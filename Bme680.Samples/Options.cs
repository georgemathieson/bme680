using CommandLine;

namespace Bme680.Samples
{
    /// <summary>
    /// Defines options that can be passed to the program from the command line.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets whether output should be formatted as JSON.
        /// </summary>
        [Option('j', "json", Default = false, HelpText = "Output readings as JSON")]
        public bool JsonOutput { get; set; }

        /// <summary>
        /// Gets or sets whether any non-reading output should be suppressed.
        /// </summary>
        [Option('q', "quiet", Default = false, HelpText = "Suppresses non-reading output")]
        public bool Quiet { get; set; }
    }
}
