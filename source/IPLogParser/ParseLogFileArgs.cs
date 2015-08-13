using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLogParser
{
    public class ParseLogFileArgs
    {
        public ParseLogFileArgs()
        {
            
        }
        public ParseLogFileArgs(string sourceFile, string destinationFile)
        {
            SourceFile = sourceFile;
            DestinationFile = destinationFile;
        }

        public string SourceFile { get; set; }
        public string DestinationFile { get; set; }
    }
}
