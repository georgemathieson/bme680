using System;
using System.Device.I2c;
using Iot.Units;

namespace Bme680
{
    /// <summary>
    /// Represents a BME680 gas, temperature, humidity and pressure sensor.
    /// </summary>
    public sealed class Bme680 : IDisposable
    {
        /// <summary>
        /// Primary I2C address.
        /// </summary>
        public const byte I2cAddressPrimary = 0x76;

        /// <summary>
        /// Secondary I2C address.
        /// </summary>
        public const byte I2cAddressSecondary = 0x77;

        /// <summary>
        /// The expected chip ID of the BME68x product family.
        /// </summary>
        private const byte _expectedChipId = 0x61;

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
            var deviceAddress = i2cDevice.ConnectionSettings.DeviceAddress;
            if (deviceAddress < I2cAddressPrimary || deviceAddress > I2cAddressSecondary)
            {
                throw new ArgumentOutOfRangeException(nameof(i2cDevice),
                    $"Chip address 0x{deviceAddress.ToString("X2")} is out of range for a BME680. Expected 0x{I2cAddressPrimary.ToString("X2")} or 0x{I2cAddressSecondary.ToString("X2")}");
            }

            // Ensure the device exists on the bus.
            _i2cDevice.WriteByte((byte)Register.Id);
            var readChipId = _i2cDevice.ReadByte();
            if (readChipId != _expectedChipId)
            {
                throw new Bme680Exception(
                    $"Chip ID 0x{readChipId.ToString("X2")} is not the same as expected 0x{_expectedChipId.ToString("X2")}. Please check you are using the right device.");
            }
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
