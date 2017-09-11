using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLReader
{
	public class XMLtoRead
	{

		public static string GetTestingXML()
		{
			string xmlString2 =
				@"<pfs xsi:schemaLocation=""http://www.transunion.com/namespace pfsV4.xsd"" xmlns=""http://www.transunion.com/namespace/pfs/v4"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
	<document>response</document>
	<version>4.0</version>
	<transactionControl>
		<subscriber>
			<industryCode>Z</industryCode>
			<memberCode>05453552</memberCode>
			<inquirySubscriberPrefixCode>0622</inquirySubscriberPrefixCode>
			<password>B61F</password>
		</subscriber>
		<userKey>janeDoe123</userKey>
		<queryType>FA</queryType>
		<permissiblePurpose>
			<endUser>
				<organization/>
			</endUser>
		</permissiblePurpose>
		<outputFormat>XML</outputFormat>
		<referenceID>1223929ABCD</referenceID>
	</transactionControl>
	<product>
		<subject>
			<subjectRecord>
				<indicative source=""input"">
					<name>
						<person>
							<first>TIM</first>
							<middle>S</middle>
							<last>ZUMWALT</last>
						</person>
					</name>
					<address>
						<street>
							<unparsed>203 VIRGINIA</unparsed>
						</street>
						<location>
							<city>ROCHESTER</city>
							<state>TN</state>
							<zipCode>15704</zipCode>
							<county>
								<name/>
								<code/>
							</county>
						</location>
					</address>
					<socialSecurity>
						<number>666609251</number>
					</socialSecurity>
					<dateOfBirth/>
				</indicative>
				<indicative source=""file"">
					<name>
						<person>
							<first/>
							<middle/>
							<last/>
						</person>
					</name>
					<address>
						<street>
							<unparsed/>
							<number/>
							<preDirectional/>
							<name/>
							<postDirectional/>
							<type/>
							<unit/>
						</street>
						<location>
							<city/>
							<state/>
							<zipCode/>
							<county/>
						</location>
					</address>
					<previousAddresses/>
					<phone/>
					<socialSecurity>
						<number/>
					</socialSecurity>
					<dateOfBirth/>
					<employments/>
				</indicative>
				<calculations>
					<dti>1</dti>
					<residualIncome>420</residualIncome>
					<fpl>84</fpl>
					<availableCredit/>
					<estHouseholdIncome>1700</estHouseholdIncome>
					<estHouseholdSize>4</estHouseholdSize>
				</calculations>
				<scores>
					<scoreModel>
						<code>New Account</code>
						<score>
							<results/>
							<derogatoryAlert/>
							<fileInquiriesImpactedScore/>
							<scoreCard/>
						</score>
					</scoreModel>
					<scoreModel>
						<code>Recovery</code>
						<score>
							<results/>
							<derogatoryAlert/>
							<fileInquiriesImpactedScore/>
							<scoreCard/>
						</score>
					</scoreModel>
					<scoreModel>
						<code>FICO</code>
						<score>
							<results/>
							<derogatoryAlert/>
							<fileInquiriesImpactedScore/>
							<scoreCard/>
						</score>
					</scoreModel>
				</scores>
				<determinationStatus>
					<accuracy>Not Included</accuracy>
					<financialAid DisplayColor=""GREEN"">0 to 100 FPL</financialAid>
					<collection>Not Included</collection>
					<riskIndicator/>
					<redFlag/>
					<warnings/>
				</determinationStatus>
				<creditReport>
					<pullDate>2015-10-23 11:26:24.240</pullDate>
					<printImage>Not Included</printImage>
				</creditReport>
				<tradeAccounts/>
				<publicRecords/>
				<collectionAccounts/>
			</subjectRecord>
		</subject>
	</product>
</pfs>";
			return xmlString2;
		}
	}
}
