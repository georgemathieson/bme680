using System;
using System.Device.I2c;

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
        /// Chip ID of the BME68x product family.
        /// </summary>
        private const byte _chipId = 0x61;

        /// <summary>
        /// The communications channel to this device on an I2C bus.
        /// </summary>
        private I2cDevice _i2cDevice;

        /// <summary>
        /// Initialize a new instance of the <see cref="Bme680"/> class.
        /// </summary>
        /// <param name="i2cDevice">The <see cref="I2cDevice"/> to create with.</param>
        public Bme680(I2cDevice i2cDevice)
        {
            _i2cDevice = i2cDevice;
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
