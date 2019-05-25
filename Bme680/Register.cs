namespace Bme680
{
    /// <summary>
    /// General control registers.
    /// </summary>
    public enum Register : byte
    {
        /// <summary>
        /// Register for retrieving the chip ID of the device. 
        /// </summary>
        /// <remarks>
        /// Status register. This register is read-only.
        /// </remarks>
        Id = 0xD0
    }
}
