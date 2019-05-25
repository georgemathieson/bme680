using System;
using System.Device.I2c;

namespace Bme680
{
    /// <summary>
    /// Represents a BME680 gas, temperature, humidity and pressure sensor over the I2C serial protocol.
    /// </summary>
    public class Bme680 : IDisposable
    {
        /// <summary>
        /// Pipeline to the <see cref="I2cDevice"/>.
        /// </summary>
        internal I2cDevice _i2cDevice;

        /// <summary>
        /// Initialise a new instance of the <see cref="Bme680"/> class.
        /// </summary>
        /// <param name="i2cDevice">The <see cref="I2cDevice"/> to create with.</param>
        public Bme680(I2cDevice i2cDevice)
        {
            _i2cDevice = i2cDevice;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
