using CommandLine;

namespace Bme680.Samples
{
    /// <summary>
    /// Defines options that can be passed to the program from the command line.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the format to use when writing readings to the console.
        /// </summary>
        [Option('f', "format", Default = "text", HelpText = "Set format of output (text, json)")]
        public string Format { get; set; }
    }
}
