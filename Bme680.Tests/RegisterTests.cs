using Xunit;

namespace Bme680.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Register"/>.
    /// </summary>
    public class RegisterTests
    {
        /// <summary>
        /// Ensure that <see cref="Register.Id"/> is set to 0xD0.
        /// </summary>
        /// <remarks>
        /// 0xD0 for I2C.
        /// </remarks>
        [Fact]
        public void Id_HasValue_0xD0()
        {
            // Arrange.
            byte expected = 0xD0;

            // Act.
            var actual = (byte)Register.Id;

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
