namespace Bme680
{
    /// <summary>
    /// General control registers.
    /// </summary>
    /// <remarks>
    /// Register names have been kept the same as listed in the BME680 datasheet. See section 5.2 Memory map.
    /// </remarks>
    public enum Register : byte
    {
        /// <summary>
        /// Register for retrieving the chip ID of the device. 
        /// </summary>
        /// <remarks>
        /// Status register. This register is read-only.
        /// </remarks>
        Id = 0xD0,

        /// <summary>
        /// Measurement condition control register.
        /// </summary>
        /// <remarks>
        /// Temperature oversampling (bits 7 to 5).
        /// Pressure oversampling (bits 4 to 2).
        /// Power mode (bits 1 to 0).
        /// </remarks>
        Ctrl_meas = 0x74,

        /// <summary>
        /// Register for retrieving the MSB part of the raw temperature measurement output data.
        /// </summary>
        temp_msb = 0x22,

        /// <summary>
        /// Register for retrieving the LSB part of the raw temperature measurement output data.
        /// </summary>
        temp_lsb = 0x23,

        /// <summary>
        /// Register for retrieving the XLSB part of the raw temperature measurement output data.
        /// </summary>
        /// <remarks>
        /// Contents depend on temperature resolution controlled by oversampling setting.
        /// </remarks>
        temp_xlsb = 0x24,

        /// <summary>
        /// Register for retrieving temperature calibration data.
        /// </summary>
        temp_cal_1 = 0xE9,

        /// <summary>
        /// Register for retrieving temperature calibration data.
        /// </summary>
        temp_cal_2 = 0x8A,

        /// <summary>
        /// Register for retrieving temperature calibration data.
        /// </summary>
        temp_cal_3 = 0x8C,
    }
}
