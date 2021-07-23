using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCallsExample
{
	public class TestWebsiteManager
	{
		private string _websiteUrl = "";
		public TestWebsiteManager()
		{
			_websiteUrl = "https://secure.enshealth.com/stomp/servlet/com.ens.stomp.transport.adapters.inbound.http.HttpAdapter";
		}

		public string RunApiCallTest()
		{
			Console.WriteLine("Making Request ...");
			var request = @"Version: 0.2
Username: GATE0215
Password: 7tvK5DR7
Organization: GATE0215
Message Format: X12
Message Type: ANSI
Body Length: 770
Usage: T

ISA*00*          *00*          *ZZ*BPR219997000   *ZZ*87726          *210708*1439*^*00501*000000011*0*T*:~GS*HI*BPR219997000*87726*20210708*1439*1*X*005010X216~ST*278*0001*005010X216~BHT*0007*CN*ZU46NG2XSGZU*20210708*143935*RD~HL*1**20*1~NM1*FA*2*Centura Health - Avista Adventist Hospital*****XX*1891709192~PER*IC* *TE**EX~HL*2*1*21*1~NM1*PR*2*UnitedHealth Group*****PI*87726~HL*3*2*22*1~NM1*IL*1*MORGAN*JOHN****MI~REF*6P*U4509268303~DMG*D8*19900911*U~HL*4*3*EV*1~UM*AR*I*30*11:A**03~DTP*435*D8*20210708~HI*ABF:2~CL1*9~MSG*18~NM1*FA*2*Centura Health - Avista Adventist Hospital*****XX*1891709192~PER*IC**TE**FX~NM1*71*1******XX~PER*IC**TE**FX~HL*5*4*SS*0~TRN*1*QL4OQJR9ZSQD*00GATE0215~UM*HS*I*30*11:A~DTP*472*D8*20210708~SV2**HC:12345~SE*27*0001~GE*1*1~IEA*1*000000011~";
			var response = GetResponse(request);
			WriteToOutputFile(response);
			Console.WriteLine("Response : " + response);

			Console.ReadLine();
			return response;
		}

		public static void WriteToOutputFile(string response)
		{
			File.AppendAllText("output.txt", response);
		}

		protected string GetResponse(string request)
		{

			using (var client = new HttpClient {BaseAddress = new Uri(_websiteUrl) })
			{
				client.DefaultRequestHeaders.Accept.Clear();
				var notificationOfAdmissionRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_websiteUrl))
				{
					Content = new StringContent(request)
				};
				var response = client.SendAsync(notificationOfAdmissionRequest).Result;
				return HandleResponse(response);
			}
		}

		protected string HandleResponse(HttpResponseMessage response)
		{
			Console.WriteLine("Recieved Response ...");
			var responseContent = response.Content.ReadAsStringAsync().Result;

			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Successful Response ...");
				return responseContent;
			}

			Console.WriteLine("Unsuccessful Response ...");

			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new UnauthorizedAccessException("Access Denied Error");
			}

			return responseContent;
		}
	}
}
