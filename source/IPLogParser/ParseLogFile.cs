using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IPLogParser
{
    public class ParseLogFile : ParseLogFileArgs
    {
        public ParseLogFile()
        {

        }
        public ParseLogFile(ParseLogFileArgs args)
        {
            SourceFile = args.SourceFile;
            DestinationFile = args.DestinationFile;
        }

        public const string AccessListIdentifier = "access-list";

        public void Parse(ParseLogFileArgs args = null)
        {
            if (args != null)
            {
                SourceFile = string.IsNullOrEmpty(args.SourceFile) ? SourceFile : args.SourceFile;
                DestinationFile = string.IsNullOrEmpty(args.DestinationFile) ? DestinationFile : args.DestinationFile;

                var fileData = File.ReadAllLines(SourceFile).ToList();
                var result = ProcessFileData(fileData);

                File.WriteAllLines(DestinationFile, result);

                return;
            }

            var sourceDirectory = "Source";
            var outputDirectory = "Output";

            if (!Directory.Exists(sourceDirectory))
            {
                Directory.CreateDirectory(sourceDirectory);
                return;
            }
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            var sourceFiles = Directory.GetFiles(sourceDirectory);

            foreach (var file in sourceFiles)
            {
                var destinationFile = Path.Combine(outputDirectory, Path.GetFileName(file) + ".csv");
                var fileData = File.ReadAllLines(file).ToList();
                var result = ProcessFileData(fileData);

                File.WriteAllLines(destinationFile, result);
            }
        }

        List<string> ProcessFileData(List<string> fileData)
        {
            var reports = new List<OutputReport>();
            var result = new List<string>();
            //result.Add(OutputReport.GenerateHeaderLine());

            foreach (var line in fileData)
            {
                var report = new OutputReport();
                var isAccessList = line.Contains(AccessListIdentifier);
                var text = line.Replace("-", " ").Replace("_", " ");

                if (isAccessList)
                    ExtractAccessListData(report, text);
                //else
                //    ExtractIPs(report, text);

                if (string.IsNullOrEmpty(report.Port) && report.IPAddresses.Count == 0)
                    continue;

                reports.Add(report);
            }

            int ipColumnCount = reports.Select(x => x.IPAddresses.Count).Max();

            result.Add(OutputReport.GenerateHeaderLine(ipColumnCount));
            reports.ForEach(x => result.Add(x.ToString()));

            return result;
        }

        void ExtractAccessListData(OutputReport report, string text)
        {
            report.LineType = AccessListIdentifier;

            var words = text.Split(' ').ToList();
            var startPointer = words.FindIndex(x => x.Contains("eq"));
            var endPointer = words.FindIndex(x => x.Contains("(hitcnt="));

            if (endPointer > 0 && startPointer > 0)
            {
                var port = words[endPointer - 1];

                if (!port.Contains("255."))
                    report.Port = port;
            }

            ExtractIPs(report, text);
        }

        void ExtractIPs(OutputReport report, string value)
        {
            string pattern = null;
            pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"; //This one actually works

            var words = value.Split(' ');

            MatchCollection ips = Regex.Matches(value, pattern);

            for (int i = 0; i < ips.Count; i++)
            {
                var ipValue = ips[i].Value;

                if (!ipValue.Contains("255"))
                    report.IPAddresses.Add(ipValue);
            }
        }
    }
}
