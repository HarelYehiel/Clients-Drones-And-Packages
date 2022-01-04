using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalFacade
{
    class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackeages;
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"xml\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackeages = (from pack in dalConfig.Element("dal-packages").Elements()
                            select pack).ToDictionary(p => "" + p.Name, p => p.Value);

        }
    }
    public class DalConfigExeption : Exception
    {
        public DalConfigExeption(string str) : base(str) { }
        public DalConfigExeption(string str, Exception ex) : base(str,ex) { }

    }
}
