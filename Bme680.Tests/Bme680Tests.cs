using System;
using System.Device.I2c;
using System.Linq;
using System.Reflection;
using Bme680.Com;
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
        private readonly Mock<IComDevice> _mockComDevice;

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
            _mockComDevice = new Mock<IComDevice>();
            _mockComDevice
                .Setup(device => device.DeviceAddress)
                .Returns(Bme680.DefaultI2cAddress);
            _mockComDevice
                .Setup(device => device.ReadByte())
                .Returns(_expectedChipId);

            _bme680 = new Bme680(_mockComDevice.Object);
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
                _mockComDevice
                    .Setup(device => device.DeviceAddress)
                    .Returns((byte)invalidAddress);

                // Act and Assert.
                Assert.Throws<ArgumentOutOfRangeException>(() => new Bme680(_mockComDevice.Object));
            }
        }

        /// <summary>
        /// On construction, ensure that <see cref="device.WriteByte(byte)"/> is called with the <see cref="Register.Id"/>.
        /// </summary>
        [Fact]
        public void Bme680_Writes_RegisterId()
        {
            // Assert (Arrange and Act done in the test's constructor).
            _mockComDevice.Verify(device => device.WriteByte((byte)Register.Id), Times.Once);
        }

        /// <summary>
        /// On construction, ensure that the <see cref="device.ReadByte()"/> is called to get the device id.
        /// </summary>
        [Fact]
        public void Bme680_Calls_ReadByte()
        {
            // Assert (Arrange and Act done in the test's constructor).
            _mockComDevice.Verify(device => device.ReadByte(), Times.Once);
        }

        /// <summary>
        /// On construction, if the chip ID does not match what is expected (0x61), then a <see cref="Bme680Exception"/> is thrown.
        /// </summary>
        [Fact]
        public void Bme680_WrongChipId_ThrowsBme680Exception()
        {
            // Arrange.
            _mockComDevice
                .Setup(device => device.ReadByte())
                .Returns(default(byte));

            // Act and Assert.
            Assert.Throws<Bme680Exception>(() => new Bme680(_mockComDevice.Object));
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
            _mockComDevice.Verify(device => device.Dispose(), Times.Once);
        }

        /// <summary>
        /// Ensure that on calling <see cref="Bme680.Dispose()"/> that the <see cref="IComDevice"/> stored internally is set to null.
        /// </summary>
        [Fact]
        public void Dispose_SetsComDevice_ToNull()
        {
            // Arrange.
            var bindFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var fieldInfo = typeof(Bme680).GetField("_comDevice", bindFlags);

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
