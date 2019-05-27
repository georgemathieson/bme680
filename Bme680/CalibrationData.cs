namespace Bme680
{
    /// <summary>
    /// Calibration data for the <see cref="Bme680"/>.
    /// </summary>
    public class CalibrationData
    {
        /// <summary>
        /// Gets or sets the temperature calibration for <see cref="Register.temp_cal_1"/>.
        /// </summary>
        public ushort TCal1 { get; private set; }

        /// <summary>
        /// Gets or sets the temperature calibration for <see cref="Register.temp_cal_2"/>.
        /// </summary>
        public ushort TCal2 { get; private set; }

        /// <summary>
        /// Gets or sets the temperature calibration for <see cref="Register.temp_cal_3"/>
        /// </summary>
        public byte TCal3 { get; private set; }

        /// <summary>
        /// Read calibration data from device.
        /// </summary>
        /// <param name="bme680">The <see cref="Bme680"/> to read calibration data from.</param>
        internal void ReadFromDevice(Bme680 bme680)
        {
            TCal1 = bme680.Read16Bits(Register.temp_cal_1);
            TCal2 = bme680.Read16Bits(Register.temp_cal_2);
            TCal3 = bme680.Read8Bits(Register.temp_cal_3);
        }
    }
}
