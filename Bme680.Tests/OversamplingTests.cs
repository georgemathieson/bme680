using Xunit;

namespace Bme680.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Oversampling"/>.
    /// </summary>
    public class OversamplingTests
    {
        /// <summary>
        /// Ensure that <see cref="Oversampling.Skipped"/> is set to binary 0.
        /// </summary>
        [Fact]
        public void OversamplingSkipped_HasValue_0()
        {
            // Arrange.
            var expected = 0;

            // Act.
            var actual = (byte)Oversampling.Skipped;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Oversampling.x1"/> is set to binary 1.
        /// </summary>
        [Fact]
        public void Oversamplingx1_HasValue_1()
        {
            // Arrange.
            var expected = 1;

            // Act.
            var actual = (byte)Oversampling.x1;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Oversampling.x2"/> is set to binary 2.
        /// </summary>
        [Fact]
        public void Oversamplingx2_HasValue_2()
        {
            // Arrange.
            var expected = 2;

            // Act.
            var actual = (byte)Oversampling.x2;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Oversampling.x4"/> is set to binary 3.
        /// </summary>
        [Fact]
        public void Oversamplingx4_HasValue_3()
        {
            // Arrange.
            var expected = 3;

            // Act.
            var actual = (byte)Oversampling.x4;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Oversampling.x8"/> is set to binary 4.
        /// </summary>
        [Fact]
        public void Oversamplingx8_HasValue_4()
        {
            // Arrange.
            var expected = 4;

            // Act.
            var actual = (byte)Oversampling.x8;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Oversampling.x16"/> is greater than or equal to binary 5.
        /// </summary>
        [Fact]
        public void Oversamplingx16_HasValue_GreaterThanOrEqualTo5()
        {
            // Arrange.
            var expected = 5;

            // Act.
            var actual = (byte)Oversampling.x16;

            // Assert.
            Assert.True(actual >= expected);
        }
    }
}
