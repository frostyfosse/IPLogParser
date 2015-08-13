using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLogParser
{
    public class OutputReport
    {
        public OutputReport()
        {

        }

        /// <summary>
        /// This is used to determine what type of information was found. In the logs provided each line could
        /// contain different information and it would be helpful to understand why not all properties were populated.
        /// </summary>
        public string LineType { get; set; }

        List<string> _ipAddresses = new List<string>();
        public List<string> IPAddresses 
        {
            get { return _ipAddresses; }
            set { _ipAddresses = value; }
        }
        public string Port { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns>
        /// Comma delimited list of all the properties listed in the following order:
        /// LineType, IPAddresses (Delimited internally by '|' if more than one), Port, and Count
        /// </returns>
        public override string ToString()
        {
            StringBuilder ipAddresses = new StringBuilder();

            if (IPAddresses.Count == 1)
                ipAddresses.Append(IPAddresses.First());
            else if (IPAddresses.Count > 1)
                IPAddresses.ForEach(x => ipAddresses.AppendFormat("{0}|", x));

            return string.Format("{0},{1},{2}", LineType, Port, ipAddresses.ToString());
        }

        Dictionary<string, string> _lineTypeDictionary = new Dictionary<string, string>();

        public Dictionary<string, string> LineTypeDictionary 
        {
            get { return _lineTypeDictionary; }
            set { _lineTypeDictionary = value; }
        }

        public static string GenerateHeaderLine()
        {
            return "LineType,Port,IPAddresses";
        }

        void GenerateLineTypeDictionary()
        {
            LineTypeDictionary.Add("nat (transport", "NAT");
            LineTypeDictionary.Add("IP LOCAL POOL", "IP Local Pool");
            LineTypeDictionary.Add("IP ADDRESS", "General IP Listing");
            LineTypeDictionary.Add("OBJECT NETWORK", "Network Object");
            LineTypeDictionary.Add("NETWORK-OBJECT", "Network Object");
            LineTypeDictionary.Add("SUBNET", "Subnet");
            LineTypeDictionary.Add("HOST", "Host");
            LineTypeDictionary.Add("ACCESS-LIST", "Access List");
            LineTypeDictionary.Add("LOGGING HOST", "Logging Host");
            LineTypeDictionary.Add("ROUTE TRANSPORT", "Route Transport");
            LineTypeDictionary.Add("AAA-SERVER", "aaa-server");
            LineTypeDictionary.Add("SSH", "Ssh");
            LineTypeDictionary.Add("WINS-SERVER", "wins-server");
            LineTypeDictionary.Add("DNS-SERVER", "dns-server");

        }
    }
}
