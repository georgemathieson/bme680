namespace Bme680
{
    /// <summary>
    /// Calibration data for the <see cref="Bme680"/>.
    /// </summary>
    public class CalibrationData
    {
        /// <summary>
        /// Gets a temperature calibration value from <see cref="Register.temp_cal_1"/>.
        /// </summary>
        public ushort TCal1 { get; private set; }

        /// <summary>
        /// Gets a temperature calibration value from <see cref="Register.temp_cal_2"/>.
        /// </summary>
        public ushort TCal2 { get; private set; }

        /// <summary>
        /// Gets a temperature calibration value from <see cref="Register.temp_cal_3"/>
        /// </summary>
        public byte TCal3 { get; private set; }

        /// <summary>
        /// Gets a humidity calibration value from <see cref="Register.hum_cal_1_msb"/> and <see cref="Register.hum_cal_1_lsb"/>.
        /// </summary>
        public ushort HCal1 { get; private set; }

        /// <summary>
        /// Gets a humidity calibration value from <see cref="Register.hum_cal_2_msb"/> and <see cref="Register.hum_cal_2_lsb"/>.
        /// </summary>
        public ushort HCal2 { get; private set; }

        /// <summary>
        /// Gets a humidity calibration value from <see cref="Register.hum_cal_3"/>.
        /// </summary>
        public byte HCal3 { get; private set; }

        /// <summary>
        /// Gets a humidity calibration value from <see cref="Register.hum_cal_4"/>.
        /// </summary>
        public byte HCal4 { get; private set; }

        /// <summary>
        /// Gets a humidity calibration value from <see cref="Register.hum_cal_5"/>.
        /// </summary>
        public byte HCal5 { get; private set; }

        /// <summary>
        /// Gets a humidity calibration value from <see cref="Register.hum_cal_6"/>.
        /// </summary>
        public byte HCal6 { get; private set; }

        /// <summary>
        /// Gets a humidity calibration value from <see cref="Register.hum_cal_7"/>.
        /// </summary>
        public byte HCal7 { get; private set; }

        /// <summary>
        /// Read calibration data from device.
        /// </summary>
        /// <param name="bme680">The <see cref="Bme680"/> to read calibration data from.</param>
        internal void ReadFromDevice(Bme680 bme680)
        {
            TCal1 = bme680.Read16Bits(Register.temp_cal_1);
            TCal2 = bme680.Read16Bits(Register.temp_cal_2);
            TCal3 = bme680.Read8Bits(Register.temp_cal_3);

            HCal1 = (ushort)((bme680.Read8Bits(Register.hum_cal_1_msb) << 4) | (bme680.Read8Bits(Register.hum_cal_1_lsb) & 0b_0000_1111));
            HCal2 = (ushort)((bme680.Read8Bits(Register.hum_cal_2_msb) << 4) | (bme680.Read8Bits(Register.hum_cal_2_lsb) >> 4));
            HCal3 = bme680.Read8Bits(Register.hum_cal_3);
            HCal4 = bme680.Read8Bits(Register.hum_cal_4);
            HCal5 = bme680.Read8Bits(Register.hum_cal_5);
            HCal6 = bme680.Read8Bits(Register.hum_cal_6);
            HCal7 = bme680.Read8Bits(Register.hum_cal_7);
        }
    }
}
