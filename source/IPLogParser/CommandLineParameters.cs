using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace IPLogParser
{
    class CommandLineParameters
    {
        [Option(HelpText="The file containing the raw data.", DefaultValue="Source.txt", Required = false)]
        public string SourceFile { get; set; }

        [Option(HelpText = "The file to store the parsed data.", DefaultValue = "Results.csv", Required = false)]
        public string DestinationFile { get; set; }
    }
}
