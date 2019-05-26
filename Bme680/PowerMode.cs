namespace Bme680
{
    /// <summary>
    /// Sensor power mode.
    /// </summary>
    /// <remarks>
    /// Section 3.1 in the datasheet.
    /// </remarks>
    public enum PowerMode : byte
    {
        /// <summary>
        /// No measurements are performed.
        /// </summary>
        /// <remarks>
        /// Minimal power consumption.
        /// </remarks>
        Sleep = 0b00,

        /// <summary>
        /// Single TPHG cycle is performed.
        /// </summary>
        /// <remarks>
        /// Sensor automatically returns to sleep mode afterwards.
        /// Gas sensor heater only operates during gas measurement.
        /// </remarks>
        Forced = 0b01
    }
}
