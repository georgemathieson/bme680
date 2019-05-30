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
    public sealed class Bme680 : IDisposable
    {
        /// <summary>
        /// Default I2C bus address.
        /// </summary>
        public const byte DefaultI2cAddress = 0x76;

        /// <summary>
        /// Secondary I2C bus address.
        /// </summary>
        public const byte SecondaryI2cAddress = 0x77;

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
                    $"Chip address 0x{deviceAddress.ToString("X2")} is out of range for a BME680. Expected 0x{DefaultI2cAddress.ToString("X2")} or 0x{SecondaryI2cAddress.ToString("X2")}");
            }

            // Ensure the device exists on the I2C bus.
            byte readChipId = Read8Bits(Register.Id);
            if (readChipId != _expectedChipId)
            {
                throw new Bme680Exception(
                    $"Chip ID 0x{readChipId.ToString("X2")} is not the same as expected 0x{_expectedChipId.ToString("X2")}. Please check you are using the right device.");
            }

            _calibrationData.ReadFromDevice(this);
        }

        /// <summary>
        /// Gets a value indicating whether or not new sensor data is available.
        /// </summary>
        /// <returns>True if new data is available.</returns>
        public bool HasNewData()
        {
            int status = Read8Bits(Register.eas_status_0);

            return status == 1;
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
        /// Read the temperature data.
        /// </summary>
        /// <returns>Temperature read from the device.</returns>
        public Temperature ReadTemperature()
        {
            // Read temperature data.
            byte msb = Read8Bits(Register.temp_msb);
            byte lsb = Read8Bits(Register.temp_lsb);
            byte xlsb = Read8Bits(Register.temp_xlsb);

            // Convert to a 32bit integer.
            int adcTemperature = (msb << 12) + (lsb << 4) + (xlsb >> 4);

            float temperature = (((adcTemperature / 16384.0f) - (_calibrationData.TCal1 / 1024.0f)) * _calibrationData.TCal2) / 5120.0f;
            float precision = (((adcTemperature / 131072.0f) - (_calibrationData.TCal1 / 8192.0f)) * _calibrationData.TCal3) / 5120.0f;

            return Temperature.FromCelsius(temperature + precision);
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
        /// Cleanup.
        /// </summary>
        public void Dispose()
        {
            _i2cDevice?.Dispose();
            _i2cDevice = null;
        }
    }
}
