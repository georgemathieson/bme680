using Xunit;

namespace Bme680.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Register"/>.
    /// </summary>
    public class RegisterTests
    {
        /// <summary>
        /// Ensure that <see cref="Register.Ctrl_hum"/> is set to 0x72.
        /// </summary>
        [Fact]
        public void Ctrl_hum_HasValue_0x72()
        {
            // Arrange.
            byte expected = 0x72;

            // Act.
            var actual = (byte)Register.Ctrl_hum;

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
        /// Ensure that <see cref="Register.eas_status_0"/> is set to 0x1D.
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
        /// Ensure that <see cref="Register.hum_cal_1_lsb"/> is set to 0xE2.
        /// </summary>
        [Fact]
        public void hum_cal_1_lsb_HasValue_0xE2()
        {
            // Arrange.
            byte expected = 0xE2;

            // Act.
            var actual = (byte)Register.hum_cal_1_lsb;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_cal_1_msb"/> is set to 0xE3.
        /// </summary>
        [Fact]
        public void hum_cal_1_msb_HasValue_0xE3()
        {
            // Arrange.
            byte expected = 0xE3;

            // Act.
            var actual = (byte)Register.hum_cal_1_msb;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_cal_2_lsb"/> is set to 0xE2.
        /// </summary>
        [Fact]
        public void hum_cal_2_lsb_HasValue_0xE2()
        {
            // Arrange.
            byte expected = 0xE2;

            // Act.
            var actual = (byte)Register.hum_cal_2_lsb;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_cal_2_msb"/> is set to 0xE1.
        /// </summary>
        [Fact]
        public void hum_cal_2_msb_HasValue_0xE1()
        {
            // Arrange.
            byte expected = 0xE1;

            // Act.
            var actual = (byte)Register.hum_cal_2_msb;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_cal_3"/> is set to 0xE4.
        /// </summary>
        [Fact]
        public void hum_cal_3_HasValue_0xE4()
        {
            // Arrange.
            byte expected = 0xE4;

            // Act.
            var actual = (byte)Register.hum_cal_3;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_cal_4"/> is set to 0xE5.
        /// </summary>
        [Fact]
        public void hum_cal_4_HasValue_0xE5()
        {
            // Arrange.
            byte expected = 0xE5;

            // Act.
            var actual = (byte)Register.hum_cal_4;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_cal_5"/> is set to 0xE6.
        /// </summary>
        [Fact]
        public void hum_cal_5_HasValue_0xE6()
        {
            // Arrange.
            byte expected = 0xE6;

            // Act.
            var actual = (byte)Register.hum_cal_5;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_cal_6"/> is set to 0xE7.
        /// </summary>
        [Fact]
        public void hum_cal_5_HasValue_0xE7()
        {
            // Arrange.
            byte expected = 0xE7;

            // Act.
            var actual = (byte)Register.hum_cal_6;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_cal_7"/> is set to 0xE8.
        /// </summary>
        [Fact]
        public void hum_cal_6_HasValue_0xE8()
        {
            // Arrange.
            byte expected = 0xE8;

            // Act.
            var actual = (byte)Register.hum_cal_7;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_lsb"/> is set to 0x26.
        /// </summary>
        [Fact]
        public void hum_lsb_HasValue_0x26()
        {
            // Arrange.
            byte expected = 0x26;

            // Act.
            var actual = (byte)Register.hum_lsb;

            // Assert.
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Ensure that <see cref="Register.hum_msb"/> is set to 0x25.
        /// </summary>
        [Fact]
        public void hum_msb_HasValue_0x26()
        {
            // Arrange.
            byte expected = 0x25;

            // Act.
            var actual = (byte)Register.hum_msb;

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
