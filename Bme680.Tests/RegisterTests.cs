using Xunit;

namespace Bme680.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Register"/>.
    /// </summary>
    public class RegisterTests
    {
        /// <summary>
        /// Ensure that <see cref="Register.eas_status_0"/> is set to 0x1D
        /// </summary>
        [Fact]
        public void eas_status_0_HasValue_0x1D()
        {
            // Arrange.
            byte expected = 0x1D;

            // Act.
            var actual = (byte)Register.eas_status_0;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.Ctrl_meas"/> is set to 0x74.
        /// </summary>
        [Fact]
        public void Ctrl_meas_HasValue_0x74()
        {
            // Arrange.
            byte expected = 0x74;

            // Act.
            var actual = (byte)Register.Ctrl_meas;

            // Assert.
            Assert.Equal(expected, actual);
        }

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

        /// <summary>
        /// Ensure that <see cref="Register.temp_cal_1"/> is set to 0xE9.
        /// </summary>
        [Fact]
        public void temp_cal_1_HasValue_0xE9()
        {
            // Arrange.
            byte expected = 0xE9;

            // Act.
            var actual = (byte)Register.temp_cal_1;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.temp_cal_2"/> is set to 0x8A.
        /// </summary>
        [Fact]
        public void temp_cal_2_HasValue_0xE9()
        {
            // Arrange.
            byte expected = 0x8A;

            // Act.
            var actual = (byte)Register.temp_cal_2;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.temp_cal_3"/> is set to 0x8C.
        /// </summary>
        [Fact]
        public void temp_cal_3_HasValue_0xE9()
        {
            // Arrange.
            byte expected = 0x8C;

            // Act.
            var actual = (byte)Register.temp_cal_3;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.temp_lsb"/> is set to 0x23.
        /// </summary>
        [Fact]
        public void temp_lsb_HasValue_0x23()
        {
            // Arrange.
            byte expected = 0x23;

            // Act.
            var actual = (byte)Register.temp_lsb;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.temp_msb"/> is set to 0x22.
        /// </summary>
        [Fact]
        public void temp_msb_HasValue_0x22()
        {
            // Arrange.
            byte expected = 0x22;

            // Act.
            var actual = (byte)Register.temp_msb;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.temp_xlsb"/> is set to 0x24.
        /// </summary>
        [Fact]
        public void temp_xlsb_HasValue_0x24()
        {
            // Arrange.
            byte expected = 0x24;

            // Act.
            var actual = (byte)Register.temp_xlsb;

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
