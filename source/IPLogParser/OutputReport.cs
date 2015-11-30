using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLogParser
{
    public class OutputReport
    {
        public static class KnownTokens
        {
            public const string AccessList = "ACCESS-LIST";
            public const string Nat = "NAT";
        }

        public OutputReport()
        {
        }

        //static OutputReport()
        //{
        //    //Not sure if I need/want this anymore.
        //    LineTypeDictionary = new Dictionary<string, string>();
        //    LineTypeDictionary.Add("nat (transport", "NAT");
        //    LineTypeDictionary.Add("IP LOCAL POOL", "IP Local Pool");
        //    LineTypeDictionary.Add("IP ADDRESS", "General IP Listing");
        //    LineTypeDictionary.Add("OBJECT NETWORK", "Network Object");
        //    LineTypeDictionary.Add("NETWORK-OBJECT", "Network Object");
        //    LineTypeDictionary.Add("SUBNET", "Subnet");
        //    LineTypeDictionary.Add("HOST", "Host");
        //    LineTypeDictionary.Add(KnownTokens.AccessList, "Access List");
        //    LineTypeDictionary.Add("LOGGING HOST", "Logging Host");
        //    LineTypeDictionary.Add("ROUTE TRANSPORT", "Route Transport");
        //    LineTypeDictionary.Add("AAA-SERVER", "aaa-server");
        //    LineTypeDictionary.Add("SSH", "Ssh");
        //    LineTypeDictionary.Add("WINS-SERVER", "wins-server");
        //    LineTypeDictionary.Add("DNS-SERVER", "dns-server");
        //}

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
            StringBuilder result = new StringBuilder();
            //IPAddresses.RemoveAll(null);
            result.AppendFormat("{0},{1}", LineType, Port);

            IPAddresses.ForEach(x => result.AppendFormat(",{0}", x));

            var returnValue = result.ToString();
            return returnValue;
        }

        //Dictionary<string, string> _lineTypeDictionary = new Dictionary<string, string>();

        //static public Dictionary<string, string> LineTypeDictionary { get; set; }

        List<string> HeaderLine { get; set; }

        public static string GenerateHeaderLine(int ipAddressCount)
        {
            var result = "LineType,Port";

            if (ipAddressCount <= 1)
            {
                string.Join(",", new[] { result, "IpAddress" });
                return result;
            }

            string ipAddressColumns = null;

            for (int i = 1; i <= ipAddressCount; i++)
                ipAddressColumns = string.Join(",", new[] { ipAddressColumns, string.Format("IpAddress{0}", i) });

            result = string.Format("{0}{1}", result, ipAddressColumns);

            return result;
        }
    }
}
