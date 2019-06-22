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
    }
}
