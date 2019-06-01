using System;
using System.Buffers.Binary;
using System.Device.I2c;
using System.Threading.Tasks;
using Iot.Units;

namespace Bme680
{
    /// <summary>
    /// Represents a BME680 gas, temperature, humidity and pressure sensor.
    /// </summary>
    public class Bme680 : IDisposable
    {
        /// <summary>
        /// Default I2C bus address.
        /// </summary>
        public const byte DefaultI2cAddress = 0x76;

        /// <summary>
        /// Gets a value indicating whether new data is available.
        /// </summary>
        public bool HasNewData => ReadHasNewData();

        /// <summary>
        /// Gets the humidity in %rH (percentage relative humidity).
        /// </summary>
        public float Humidity => ReadHumidity();

        /// <summary>
        /// Gets the <see cref="PowerMode"/>.
        /// </summary>
        public PowerMode PowerMode => ReadPowerMode();

        /// <summary>
        /// Secondary I2C bus address.
        /// </summary>
        public const byte SecondaryI2cAddress = 0x77;

        /// <summary>
        /// Gets the <see cref="Temperature"/>.
        /// </summary>
        public Temperature Temperature => ReadTemperature();

        /// <summary>
        /// Calibration data specific to the device.
        /// </summary>
        private readonly CalibrationData _calibrationData = new CalibrationData();

        /// <summary>
        /// The expected chip ID of the BME68x product family.
        /// </summary>
        private readonly byte _expectedChipId = 0x61;

        /// <summary>
        /// The communications channel to a device on an I2C bus.
        /// </summary>
        private I2cDevice _i2cDevice;

        /// <summary>
        /// Initialize a new instance of the <see cref="Bme680"/> class.
        /// </summary>
        /// <param name="i2cDevice">The <see cref="I2cDevice"/> to create with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="Bme680Exception"></exception>
        public Bme680(I2cDevice i2cDevice)
        {
            _i2cDevice = i2cDevice ?? throw new ArgumentNullException(nameof(i2cDevice));

            // Ensure a valid device address has been set.
            int deviceAddress = i2cDevice.ConnectionSettings.DeviceAddress;
            if (deviceAddress < DefaultI2cAddress || deviceAddress > SecondaryI2cAddress)
            {
                throw new ArgumentOutOfRangeException(nameof(i2cDevice),
                    $@"Chip address 0x{deviceAddress.ToString("X2")} is out of range for a BME680. 
                    Expected 0x{DefaultI2cAddress.ToString("X2")} or 0x{SecondaryI2cAddress.ToString("X2")}");
            }

            // Ensure the device exists on the I2C bus.
            byte readChipId = Read8Bits(Register.Id);
            if (readChipId != _expectedChipId)
            {
                throw new Bme680Exception(
                    $@"Chip ID 0x{readChipId.ToString("X2")} is not the same as expected 0x{_expectedChipId.ToString("X2")}. 
                    Please check you are using the right device.");
            }

            _calibrationData.ReadFromDevice(this);
        }

        /// <summary>
        /// Set the humidity oversampling.
        /// </summary>
        /// <param name="oversampling">The <see cref="Oversampling"/> to set.</param>
        public void SetHumidityOversampling(Oversampling oversampling)
        {
            var register = Register.Ctrl_hum;
            byte read = Read8Bits(register);

            // Clear first 3 bits.
            var cleared = (byte)(read & 0b_1111_1000);

            _i2cDevice.Write(new[] { (byte)register, (byte)(cleared | (byte)oversampling) });
        }

        /// <summary>
        /// Set the power mode.
        /// </summary>
        /// <param name="powerMode">The <see cref="PowerMode"/> to set.</param>
        public void SetPowerMode(PowerMode powerMode)
        {
            var register = Register.Ctrl_meas;
            byte read = Read8Bits(register);

            // Clear first 2 bits.
            var cleared = (byte)(read & 0b_1111_1100);

            _i2cDevice.Write(new[] { (byte)register, (byte)(cleared | (byte)powerMode) });
        }

        /// <summary>
        /// Set the temperature oversampling.
        /// </summary>
        /// <param name="oversampling">The <see cref="Oversampling"/> value to set.</param>
        public void SetTemperatureOversampling(Oversampling oversampling)
        {
            var register = Register.Ctrl_meas;
            byte read = Read8Bits(register);

            // Clear last 3 bits.
            var cleared = (byte)(read & 0b_0001_1111);

            _i2cDevice.Write(new[] { (byte)register, (byte)(cleared | (byte)oversampling << 5) });
        }

        /// <summary>
        /// Read 8 bits from a given <see cref="Register"/>.
        /// </summary>
        /// <param name="register">The <see cref="Register"/> to read from.</param>
        /// <returns>Value from register.</returns>
        internal byte Read8Bits(Register register)
        {
            _i2cDevice.WriteByte((byte)register);

            return _i2cDevice.ReadByte();
        }

        /// <summary>
        /// Read 16 bits from a given <see cref="Register"/>.
        /// </summary>
        /// <param name="register">The <see cref="Register"/> to read from.</param>
        /// <returns>Value from register.</returns>
        internal ushort Read16Bits(Register register)
        {
            Span<byte> bytes = stackalloc byte[2];

            _i2cDevice.WriteByte((byte)register);
            _i2cDevice.Read(bytes);

            return BinaryPrimitives.ReadUInt16LittleEndian(bytes);
        }

        /// <summary>
        /// Read a value indicating whether or not new sensor data is available.
        /// </summary>
        /// <returns>True if new data is available.</returns>
        private bool ReadHasNewData()
        {
            var register = Register.eas_status_0;
            int read = Read8Bits(register);

            // Get only the power mode bit.
            var hasNewData = (byte)(read & 0b_1000_0000);

            return (hasNewData >> 7) == 1;
        }

        /// <summary>
        /// Read the <see cref="PowerMode"/> state.
        /// </summary>
        /// <returns>The current <see cref="PowerMode"/>.</returns>
        private PowerMode ReadPowerMode()
        {
            var register = Register.Ctrl_meas;
            byte read = Read8Bits(register);

            // Get only the power mode bits.
            var powerMode = (byte)(read & 0b_0000_0011);

            return (PowerMode)powerMode;
        }

        /// <summary>
        /// Read the humidity data.
        /// </summary>
        /// <returns>Calculated humidity.</returns>
        private float ReadHumidity()
        {
            // Read humidity data.
            byte msb = Read8Bits(Register.hum_msb);
            byte lsb = Read8Bits(Register.hum_lsb);
            var temperature = (float)Temperature.Celsius;
            
            // Convert to a 32bit integer.
            var adcHumidity = (msb << 8) + lsb;

            // Calculate the humidity.
            float var1 = adcHumidity - ((_calibrationData.HCal1 * 16.0f) + ((_calibrationData.HCal3 / 2.0f) * temperature));

            float var2 = var1 * ((_calibrationData.HCal2 / 262144.0f) * (1.0f + ((_calibrationData.HCal4 / 16384.0f)
                * temperature) + ((_calibrationData.HCal5 / 1048576.0f) * temperature * temperature)));

            float var3 = _calibrationData.HCal6 / 16384.0f;

            float var4 = _calibrationData.HCal7 / 2097152.0f;

            float calculatedHumidity = var2 + ((var3 + (var4 * temperature)) * var2 * var2);

            if (calculatedHumidity > 100.0f)
            {
                calculatedHumidity = 100.0f;
            }
            else if (calculatedHumidity < 0.0f)
            {
                calculatedHumidity = 0.0f;
            }

            return calculatedHumidity;
        }

        /// <summary>
        /// Read the temperature data.
        /// </summary>
        /// <returns>Calculated temperature.</returns>
        private Temperature ReadTemperature()
        {
            // Read temperature data.
            byte msb = Read8Bits(Register.temp_msb);
            byte lsb = Read8Bits(Register.temp_lsb);
            byte xlsb = Read8Bits(Register.temp_xlsb);

            // Convert to a 32bit integer.
            var adcTemperature = (msb << 12) + (lsb << 4) + (xlsb >> 4);

            var temperature = ((adcTemperature / 16384.0f) - (_calibrationData.TCal1 / 1024.0f)) * _calibrationData.TCal2;
            var precision = ((adcTemperature / 131072.0f) - (_calibrationData.TCal1 / 8192.0f)) * _calibrationData.TCal3;

            return Temperature.FromCelsius((temperature + precision) / 5120f);
        }

        /// <summary>
        /// Cleanup.
        /// </summary>
        public void Dispose()
        {
            _i2cDevice?.Dispose();
            _i2cDevice = null;
        }
    }
}
