using System;
using System.Device.I2c;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Bme680.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Bme680"/>.
    /// </summary>
    public class Bme680Tests
    {
        /// <summary>
        /// A mock communications channel to a device on an I2C bus.
        /// </summary>
        private readonly MockI2cDevice _mockI2cDevice;

        /// <summary>
        /// The <see cref="Bme680"/> to test with.
        /// </summary>
        private readonly Bme680 _bme680;

        /// <summary>
        /// The expected chip ID of the BME68x product family.
        /// </summary>
        private const byte _expectedChipId = 0x61;

        /// <summary>
        /// Initialize a new instance of the <see cref="Bme680"/> class.
        /// </summary>
        public Bme680Tests()
        {
            // Arrange.
            var settings = new I2cConnectionSettings(default, Bme680.DefaultI2cAddress);
            _mockI2cDevice = new MockI2cDevice(settings);

            // By default, return the expected chip ID.
            _mockI2cDevice.ReadByteSetupReturns = _expectedChipId;

            _bme680 = new Bme680(_mockI2cDevice);
        }

        /// <summary>
        /// Ensure that the default I2C address is 0x76.
        /// </summary>
        [Fact]
        public void DefaultI2cAddress_HasValue_0x76()
        {
            // Arrange.
            byte expected = 0x76;

            // Act.
            var actual = Bme680.DefaultI2cAddress;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that the secondary I2C address is 0x77.
        /// </summary>
        [Fact]
        public void SecondaryI2cAddress_HasValue_0x77()
        {
            // Arrange.
            byte expected = 0x77;

            // Act.
            var actual = Bme680.SecondaryI2cAddress;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// On construction, ensure that an <see cref="ArgumentNullException"/> is thrown if 
        /// <see cref="Bme680.Bme680(IComDevice)"/> is called with a null <see cref="IComDevice"/>.
        /// </summary>
        [Fact]
        public void Bme680_NullComDevice_ThrowsArgumentNullException()
        {
            // Arrange, Act and Assert.
            Assert.Throws<ArgumentNullException>(() => new Bme680(null));
        }

        /// <summary>
        /// On construction, ensure that all invalid addresses throw an <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        [Fact]
        public void Bme680_AddressOutOfRange_ThrowsArgumentOutOfRangeException()
        {
            // Arrange.
            var invalidAddresses = Enumerable.Range(byte.MinValue, byte.MaxValue)
                .Where(address =>
                    address != Bme680.DefaultI2cAddress &&
                    address != Bme680.SecondaryI2cAddress);

            foreach (var invalidAddress in invalidAddresses)
            {
                var settings = new I2cConnectionSettings(default, invalidAddress);
                var mockI2cDevice = new MockI2cDevice(settings);

                // Act and Assert.
                Assert.Throws<ArgumentOutOfRangeException>(() => new Bme680(mockI2cDevice));
            }
        }

        /// <summary>
        /// On construction, ensure that <see cref="device.WriteByte(byte)"/> is called with the <see cref="Register.Id"/>.
        /// </summary>
        /// <remarks>
        /// Act done in the test's constructor
        /// </remarks>
        [Fact]
        public void Bme680_CallsWriteByte_WithRegisterId()
        {
            // Arrange.
            var expected = (byte)Register.Id;
            var actual = _mockI2cDevice.WriteByteCalledWithValue;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// On construction, if the chip ID does not match what is expected (0x61), then a <see cref="Bme680Exception"/> is thrown.
        /// </summary>
        [Fact]
        public void Bme680_WrongChipId_ThrowsBme680Exception()
        {
            // Arrange.
            _mockI2cDevice.ReadByteSetupReturns = default;

            // Act and Assert.
            Assert.Throws<Bme680Exception>(() => new Bme680(_mockI2cDevice));
        }

        [Fact]
        public void SetPowerMode_CallsWriteByte_WithControlMeasurementRegister()
        {
            // Arrange.
            var expected = (byte)Register.Ctrl_meas;

            // Act.
            _bme680.SetPowerMode(PowerMode.Forced);

            // Assert.
            Assert.Equal(expected, _mockI2cDevice.WriteByteCalledWithValue);
        }

        [Theory]
        [InlineData(0b_0000_0000)]
        [InlineData(0b_1111_1111)]
        public void SetPowerMode_CallsWrite_WithCorrectValue(byte readBits)
        {
            // Arrange.
            _mockI2cDevice.ReadByteSetupReturns = readBits;
            var powerMode = PowerMode.Forced;
            var cleared = (byte)(readBits & 0b_1111_1100);
            byte expectedBits = (byte)(cleared | (byte)powerMode);
            byte[] expected = new[] { (byte)Register.Ctrl_meas, expectedBits };

            // Act.
            _bme680.SetPowerMode(powerMode);

            // Assert.
            Assert.Equal(expected, _mockI2cDevice.WriteCalledWithValue);
        }

        /// <summary>
        /// It should write the <see cref="Register.Ctrl_meas"/> register so the register value can be read from.
        /// </summary>
        [Fact]
        public void SetTemperatureOversampling_CallsWriteByte_WithControlMeasurementRegister()
        {
            // Arrange.
            var expected = (byte)Register.Ctrl_meas;

            // Act.
            _bme680.SetTemperatureOversampling(Oversampling.Skipped);

            // Assert.
            Assert.Equal(expected, _mockI2cDevice.WriteByteCalledWithValue);
        }

        /// <summary>
        /// Given any state of read bits, the correct value the correct value should be written.
        /// </summary>
        /// <param name="readBits">The read bits to test with.</param>
        [Theory]
        [InlineData(0b_0000_0000)]
        [InlineData(0b_1111_1111)]
        public void SetTemperatureOversampling_CallsWrite_WithCorrectValue(byte readBits)
        {
            // Arrange.
            _mockI2cDevice.ReadByteSetupReturns = readBits;
            var oversampling = Oversampling.x2;
            byte cleared = (byte)(readBits & 0b_0001_1111);
            byte expectedBits = (byte)(cleared | (byte)oversampling << 5);
            byte[] expected = new[] { (byte)Register.Ctrl_meas, expectedBits };

            // Act.
            _bme680.SetTemperatureOversampling(oversampling);

            // Assert.
            Assert.Equal(expected, _mockI2cDevice.WriteCalledWithValue);
        }

        /// <summary>
        /// Ensure that <see cref="IComDevice.Dispose()"/> is called when <see cref="Bme680.Dispose()"/> is called.
        /// </summary>
        [Fact]
        public void Dispose_Calls_deviceDispose()
        {
            // Act.
            _bme680.Dispose();

            // Assert.
            Assert.True(_mockI2cDevice.Disposing);
        }

        /// <summary>
        /// Ensure that on calling <see cref="Bme680.Dispose()"/> that the <see cref="IComDevice"/> stored internally is set to null.
        /// </summary>
        [Fact]
        public void Dispose_SetsComDevice_ToNull()
        {
            // Arrange.
            var bindFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var fieldInfo = typeof(Bme680).GetField("_i2cDevice", bindFlags);

            // Act
            _bme680.Dispose();

            // Assert
            Assert.Null(fieldInfo.GetValue(_bme680));
        }

        /// <summary>
        /// A Dispose method should be callable multiple times without throwing an exception.
        /// </summary>
        [Fact]
        public void Dispose_CalledMultipleTimes_ShouldNotThrow()
        {
            // Arrange by calling once.
            _bme680.Dispose();

            // Act by calling it again.
            var result = Record.Exception(() => _bme680.Dispose());

            // Assert.
            Assert.Null(result);
        }
    }
}
