using System;
using System.Device.I2c;

namespace Bme680.Com
{
    /// <summary>
    /// Wrapper for talking to a device on an I2C bus.
    /// </summary>
    /// <remarks>
    /// Decouples the <see cref="I2cDevice"/> from the sensor implementation.
    /// </remarks>
    public sealed class I2cComDevice : IComDevice
    {
        /// <summary>
        /// The communications channel to a device on an I2C bus.
        /// </summary>
        private I2cDevice _i2cDevice;

        /// <summary>
        /// Get the device address that the <see cref="I2cDevice"/> was initialized with.
        /// </summary>
        public byte DeviceAddress => (byte)_i2cDevice.ConnectionSettings.DeviceAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="I2cComDevice"/> class.
        /// </summary>
        /// <param name="i2cDevice">The <see cref="I2cDevice"/> to create with.</param>
        public I2cComDevice(I2cDevice i2cDevice)
        {
            _i2cDevice = i2cDevice ?? throw new ArgumentNullException(nameof(i2cDevice));
        }

        /// <summary>
        /// Reads data from the I2C device.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to read the data from the I2C device.
        /// The length of the buffer determines how much data to read from the I2C device.
        /// </param>
        public void Read(byte[] buffer)
        {
            _i2cDevice.Read(buffer);
        }

        /// <summary>
        /// Reads a byte from the I2C device.
        /// </summary>
        /// <returns>A byte read from the I2C device.</returns>
        public byte ReadByte()
        {
            return _i2cDevice.ReadByte();
        }

        /// <summary>
        /// Writes data to the I2C device.
        /// </summary>
        /// <param name="buffer">
        /// The buffer that contains the data to be written to the I2C device.
        /// The data should not include the I2C device address.
        /// </param>
        public void Write(byte[] buffer)
        {
            _i2cDevice.Write(buffer);
        }

        /// <summary>
        /// Writes a byte to the I2C device.
        /// </summary>
        /// <param name="value">The byte to be written to the I2C device.</param>
        public void WriteByte(byte value)
        {
            _i2cDevice.WriteByte(value);
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
