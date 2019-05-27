using Xunit;

namespace Bme680.Tests
{
    /// <summary>
    /// Unit tests for <see cref="PowerMode"/>.
    /// </summary>
    /// <remarks>
    /// Section 3.1 in the datasheet.
    /// </remarks>
    public class PowerModeTests
    {
        /// <summary>
        /// Ensure that <see cref="PowerMode.Sleep"/> is set to binary 0.
        /// </summary>
        [Fact]
        public void Sleep_HasValue_0()
        {
            // Arrange.
            var expected = 0;

            // Act.
            var actual = (byte)PowerMode.Sleep;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="PowerMode.Forced"/> is set to binary 1.
        /// </summary>
        [Fact]
        public void Forced_HasValue_1()
        {
            // Arrange.
            var expected = 1;

            // Act.
            var actual = (byte)PowerMode.Forced;

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
