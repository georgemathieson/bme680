using System;
using System.Device.I2c;
using System.Linq;
using System.Reflection;
using Moq;
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
        private readonly Mock<I2cDevice> _mockI2cDevice;

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
            _mockI2cDevice = new Mock<I2cDevice>();
            _mockI2cDevice
                .Setup(i2cDevice => i2cDevice.ConnectionSettings)
                .Returns(new I2cConnectionSettings(default, Bme680.I2cAddressPrimary));
            _mockI2cDevice
                .Setup(i2cDevice => i2cDevice.ReadByte())
                .Returns(_expectedChipId);

            _bme680 = new Bme680(_mockI2cDevice.Object);
        }

        /// <summary>
        /// Ensure that the primary I2C address is 0x76.
        /// </summary>
        [Fact]
        public void I2cAddressPrimary_HasValue_0x76()
        {
            // Arrange.
            byte expected = 0x76;

            // Act.
            var actual = Bme680.I2cAddressPrimary;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that the secondary I2C address is 0x77.
        /// </summary>
        [Fact]
        public void I2cAddressSecondary_HasValue_0x77()
        {
            // Arrange.
            byte expected = 0x77;

            // Act.
            var actual = Bme680.I2cAddressSecondary;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// On construction, ensure that an <see cref="ArgumentNullException"/> is thrown if 
        /// <see cref="Bme680.Bme680(I2cDevice)"/> is called with a null <see cref="I2cDevice"/>.
        /// </summary>
        [Fact]
        public void Bme680_NullI2cDevice_ThrowsArgumentNullException()
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
                    address != Bme680.I2cAddressPrimary && 
                    address != Bme680.I2cAddressSecondary);

            foreach (var invalidAddress in invalidAddresses)
            {
                _mockI2cDevice
                    .Setup(i2cDevice => i2cDevice.ConnectionSettings)
                    .Returns(new I2cConnectionSettings(default, invalidAddress));

                // Act and Assert.
                Assert.Throws<ArgumentOutOfRangeException>(() => new Bme680(_mockI2cDevice.Object));
            }
        }

        /// <summary>
        /// On construction, ensure that <see cref="I2cDevice.WriteByte(byte)"/> is called with the <see cref="Register.Id"/>.
        /// </summary>
        [Fact]
        public void Bme680_Writes_RegisterId()
        {
            // Assert (Arrange and Act done in the test's constructor).
            _mockI2cDevice.Verify(i2cDevice => i2cDevice.WriteByte((byte)Register.Id), Times.Once);
        }

        /// <summary>
        /// On construction, ensure that the <see cref="I2cDevice.ReadByte()"/> is called to get the device id.
        /// </summary>
        [Fact]
        public void Bme680_Calls_ReadByte()
        {
            // Assert (Arrange and Act done in the test's constructor).
            _mockI2cDevice.Verify(i2cDevice => i2cDevice.ReadByte(), Times.Once);
        }

        /// <summary>
        /// On construction, if the chip ID does not match what is expected (0x61), then a <see cref="Bme680Exception"/> is thrown.
        /// </summary>
        [Fact]
        public void Bme680_WrongChipId_ThrowsBme680Exception()
        {
            // Arrange.
            _mockI2cDevice
                .Setup(i2cDevice => i2cDevice.ReadByte())
                .Returns(default(byte));

            // Act and Assert.
            Assert.Throws<Bme680Exception>(() => new Bme680(_mockI2cDevice.Object));
        }

        /// <summary>
        /// Ensure that <see cref="I2cDevice.Dispose(true)"/> is called when <see cref="Bme680.Dispose()"/> is called.
        /// </summary>
        /// <remarks>
        /// By calling <see cref="I2cDevice.Dispose()"/> directly, we can safely assume <see cref="I2cDevice.Dispose(true)"/> will be called.
        /// </remarks>
        [Fact]
        public void Dispose_Calls_I2cDeviceDispose()
        {
            // Act.
            _bme680.Dispose();

            // Assert.
            _mockI2cDevice.Verify(i2cDevice => i2cDevice.Dispose(true), Times.Once);
        }

        /// <summary>
        /// Ensure that on calling <see cref="Bme680.Dispose()"/> that the <see cref="I2cDevice"/> stored internally is set to null.
        /// </summary>
        [Fact]
        public void Dispose_SetsI2cDevice_ToNull()
        {
            // Arrange.
            var bindFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var i2cDeviceFieldInfo = typeof(Bme680).GetField("_i2cDevice", bindFlags);

            // Act
            _bme680.Dispose();

            // Assert
            Assert.Null(i2cDeviceFieldInfo.GetValue(_bme680));
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
