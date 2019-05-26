using System;
using System.Collections.Generic;
using System.Text;

namespace Bme680.Com
{
    /// <summary>
    /// Represents the methods required for communication.
    /// </summary>
    public interface IComDevice : IDisposable
    {
        /// <summary>
        /// Get the device address that the device was initialized with.
        /// </summary>
        byte DeviceAddress { get; }

        /// <summary>
        /// Reads data from the device.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to read the data from the device.
        /// The length of the buffer determines how much data to read from the device.
        /// </param>
        void Read(byte[] buffer);

        /// <summary>
        /// Reads a byte from the device.
        /// </summary>
        /// <returns>A byte read from the device.</returns>
        byte ReadByte();

        /// <summary>
        /// Writes data to the device.
        /// </summary>
        /// <param name="buffer">
        /// The buffer that contains the data to be written to the device.
        /// The data should not include the device address.
        /// </param>
        void Write(byte[] buffer);

        /// <summary>
        /// Writes a byte to the device.
        /// </summary>
        /// <param name="value">The byte to be written to the device.</param>
        void WriteByte(byte value);
    }
}
