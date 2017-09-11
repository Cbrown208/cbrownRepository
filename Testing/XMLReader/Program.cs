using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XMLReader
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder output = new StringBuilder();
            var xmlString2 = XMLtoRead.GetTestingXML();

            XmlExamples examples = new XmlExamples();

            //examples.Example1();
            //examples.Example2();

			string XmlLayout =
	@"<PnsProviderSettings>
		<Username>[1]</Username>
		<Password>[2]</Password>
		<OrganizationId>[3]</OrganizationId>
		<IsTestRequest>[4]</IsTestRequest>
	</PnsProviderSettings>";

			var xmlDoc = XDocument.Parse(XmlLayout);
			var cridentials = xmlDoc.Elements();
			var results = "";

			var temp = cridentials.Where(x => x.Name == "Username");

			foreach (var creds in cridentials)
			{
				results = creds.Element("Username").Value;
			}

			//var result = ReadXMLFunction.SearchXML(xmlString2, "dti");
			//Console.WriteLine("Results:");
			//Console.WriteLine(result);
			Console.ReadLine();



        }
    }
}
