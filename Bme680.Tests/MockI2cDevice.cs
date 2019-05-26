﻿using System;
using System.Device.I2c;

namespace Bme680.Tests
{
    /// <summary>
    /// A mock communications channel to a device on an I2C bus.
    /// </summary>
    public class MockI2cDevice : I2cDevice
    {
        /// <summary>
        /// Pipeline to the <see cref="I2cConnectionSettings"/>.
        /// </summary>
        private readonly I2cConnectionSettings _settings;

        /// <summary>
        /// The connection settings of a device on an I2C bus.
        /// </summary>
        public override I2cConnectionSettings ConnectionSettings => _settings;

        /// <summary>
        /// Gets the last value <see cref="MockI2cDevice.Read(Span{byte})"/> was called with.
        /// </summary>
        public byte[] ReadCalledWithValue { get; private set; }

        /// <summary>
        /// Gets or sets the value to return for <see cref="MockI2cDevice.ReadByte"/>.
        /// </summary>
        public byte ReadByteSetupReturns { get; set; }

        /// <summary>
        /// Gets the last value <see cref="MockI2cDevice.Write(ReadOnlySpan{byte})"/> was called with.
        /// </summary>
        public byte[] WriteCalledWithValue { get; private set; }

        /// <summary>
        /// Gets the last value <see cref="MockI2cDevice.WriteByte(byte)"/> was called with.
        /// </summary>
        public byte WriteByteCalledWithValue { get; private set; }

        /// <summary>
        /// Gets a value indicating whether or not the object has been disposed of.
        /// </summary>
        public bool Disposing { get; private set; }

        /// <summary>
        /// Initializes new instance of <see cref="MockI2cDevice"/> class for use in tests.
        /// </summary>
        /// <param name="settings">The connection settings of a device on an I2C bus.</param>
        public MockI2cDevice(I2cConnectionSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Reads data from the I2C device.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to read the data from the I2C device.
        /// The length of the buffer determines how much data to read from the I2C device.
        /// </param>
        public override void Read(Span<byte> buffer)
        {
            ReadCalledWithValue = buffer.ToArray();
        }

        /// <summary>
        /// Reads a byte from the I2C device.
        /// </summary>
        /// <returns>A byte read from the I2C device.</returns>
        public override byte ReadByte()
        {
            return ReadByteSetupReturns;
        }

        /// <summary>
        /// Writes data to the I2C device.
        /// </summary>
        /// <param name="buffer">
        /// The buffer that contains the data to be written to the I2C device.
        /// The data should not include the I2C device address.
        /// </param>
        public override void Write(ReadOnlySpan<byte> buffer)
        {
            WriteCalledWithValue = buffer.ToArray();
        }

        /// <summary>
        /// Writes a byte to the I2C device.
        /// </summary>
        /// <param name="value">The byte to be written to the I2C device.</param>
        public override void WriteByte(byte value)
        {
            WriteByteCalledWithValue = value;
        }

        public override void Dispose(bool disposing)
        {
            Disposing = disposing;
        }
    }
}
