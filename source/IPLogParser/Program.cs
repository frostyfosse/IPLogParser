using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var parameters = CommandLine.Parser.Default.ParseArguments<CommandLineParameters>(args).Value;

                new ParseLogFile(new ParseLogFileArgs(parameters.SourceFile, parameters.DestinationFile)).Parse();
                Environment.ExitCode = 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Environment.ExitCode = 1;
            }
        }
    }
}
