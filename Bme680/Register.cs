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
    }
}
