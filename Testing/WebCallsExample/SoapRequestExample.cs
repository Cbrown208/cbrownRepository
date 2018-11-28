using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace WebCallsExample
{
	public class SoapRequestExample
	{

		public string RequestMessageValues(string request)
		{
			var checkStatusRequest = new HttpRequestMessage(HttpMethod.Post, "testUrl")
			{
				Content = new StringContent(request, Encoding.UTF8, "application/soap+xml")
			};
			var temp3 = checkStatusRequest.ToString();
			var temp = new StringContent(request, Encoding.UTF8, "application/soap+xml");

			return "";
		}
		public string GeneratePreAuthCheckStatusRequestXML(string request, string username, string password)
		{
			request = GetTestRequest();
			var retString = "";
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8, Indent = true, OmitXmlDeclaration = true
			};

			using (var textWriter = new StringWriter())
			{
				using (var xmlWriter = XmlWriter.Create(textWriter, settings))
				{

					var document = new XmlDocument();
					var root = document.CreateElement("soap", "Envelope", "http://www.w3.org/2003/05/soap-envelope");
					root.SetAttribute("xmlns:sub", "http://www.w3.org/2005/08/addressing");

					document.AppendChild(root);
					var bodyHeader = document.CreateElement("soap", "Body", "test");
					root.AppendChild(bodyHeader);

					var body = document.CreateElement("sub", "SubmitSync", "test");
					bodyHeader.AppendChild(body);

					var requestElement = document.CreateElement("sub", "request", "test");
					requestElement.InnerText = request;
					body.AppendChild(requestElement);

					var requestFormat = document.CreateElement("sub", "requestFormat", "test");
					requestFormat.InnerText = "EDI";
					body.AppendChild(requestFormat);

					var responseFormat = document.CreateElement("sub", "responseFormat", "test");
					responseFormat.InnerText = "EDI";
					body.AppendChild(responseFormat);

					var synchronousTimeout = document.CreateElement("sub", "synchronousTimeout", "test");
					synchronousTimeout.InnerText = "00:01:00";
					body.AppendChild(synchronousTimeout);

					var submissionTimeout = document.CreateElement("sub", "submissionTimeout", "test");
					submissionTimeout.InnerText = "00:01:00";
					body.AppendChild(submissionTimeout);

					document.WriteTo(xmlWriter);
				}
				retString = textWriter.ToString();
				retString = formatCheckStatusRequest(retString,username,password);
			}
			return retString;
		}

		private string formatCheckStatusRequest(string retString, string username, string password)
		{
			var headerString = "<soap:Header ><sub:SecurityHeader ><sub:UserName >@username@</sub:UserName ><sub:Password > @password@ </sub:Password ></sub:SecurityHeader ></soap:Header>";
			retString = retString.Replace("nil", "i:nil");
			headerString = headerString.Replace("@username@", username);
			headerString = headerString.Replace("@password@", password);
			retString = retString.Replace(" xmlns:soap=\"test\"", "").Replace(" xmlns:sub=\"test\"", "");
			retString = retString.Replace("<soap:Body>", headerString + "<soap:Body>");
			return retString;
		}

		private string GetTestRequest()
		{
			var x15RawMessage = @"ISA*00* *00* *ZZ*GATE0115 *ZZ*841162764 *150226*1656*^*00501*390210009*0*T*:~
GS*HI*GATE0115*841162764*20150226*165605*1*X*005010X215~
ST*278*0001*005010X215~
BHT*0007*28*3285370014*20150209*1853~
HL*1**20*1~
NM1*X3*2*UNITED HEALTH CARE*****PI*10002~
HL*2*1*21*1~
NM1*1P*2*TEST PROVIDER*****XX*1111111111111~
HL*3*2*22*1~
NM1*IL*1*SMITH*JOHN****MI*99999999999~
DMG*D8*195xxxxx~
HL*4*3*23*1~
NM1*QC*1*SMITH*JANE~
DMG*D8*195xxxxx~
HL*5*4*EV*0~
UM*AR*I*18*12:B~
REF*NT*8075762301~
DTP*AAH*RD8*20141207 -20151206~
NM1*SJ*2*TEST SVC PROVIDER*****XX*1111111111111~
SE*18*0001~
GE*1*1~
IEA*1*390210009~";
			return x15RawMessage;
		}
	}
}
