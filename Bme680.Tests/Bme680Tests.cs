using System;
using System.Reflection;
using System.Device.I2c;
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
        /// Initialize a new instance of the <see cref="Bme680"/> class.
        /// </summary>
        public Bme680Tests()
        {
            // Arrange.
            _mockI2cDevice = new Mock<I2cDevice>();
            _bme680 = new Bme680(_mockI2cDevice.Object);
        }

        /// <summary>
        /// Ensure that the primary I2C address is 0x76.
        /// </summary>
        [Fact]
        public void I2cAddressPrimary_ShouldBe_0x76()
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
        public void I2cAddressSecondary_ShouldBe_0x77()
        {
            // Arrange.
            byte expected = 0x77;

            // Act.
            var actual = Bme680.I2cAddressSecondary;

            // Assert.
            Assert.Equal(expected, actual);
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
