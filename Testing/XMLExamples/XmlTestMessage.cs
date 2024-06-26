﻿namespace XMLExamples
{
	public class XmlTestMessage
	{
		public string GetXmlMessage()
		{
			return @"<ADTMessage><CreatedOn>0001-01-01T00:00:00</CreatedOn><DiagnosisList /><Guarantor /><Header><ClientId>3503</ClientId><EventType>S14</EventType><MessageControlId>254173415</MessageControlId><MessageTimestamp>201707170855</MessageTimestamp><MessageType>SIU</MessageType><OperatorId /><ReceivingApplication /><ReceivingFacility /><RecordedTimestamp /><SendingApplication /><SendingFacility>TSH</SendingFacility><SendingFacilityId>AA33D4E9-A389-41B4-BE85-A1F7EFF4B37B</SendingFacilityId></Header><InsuranceList /><InsuranceVerificationList /><NextOfKinList /><Patient><ContactInfo><AddressLine1>25 OLD KINGS HWY N</AddressLine1><AddressLine2>APT 13 171</AddressLine2><BusinessPhone>(203)965-6000</BusinessPhone><City>DARIEN</City><Country /><County /><Email /><Fax /><HomePhone>(518)779-0090</HomePhone><MobilePhone /><PostalCode>06820</PostalCode><State>CT</State></ContactInfo><DateOfBirth>19720904</DateOfBirth><DateOfDeath /><DriversLicense /><FamilyName>WHITE</FamilyName><Gender>F</Gender><GivenName>NAOMI</GivenName><MiddleName /><Prefix /><SSN>999999999</SSN><Suffix /><AccountNumber>V00046246005</AccountNumber><EthnicGroup /><MRN>M000848892</MRN><Race /><Religion>CHR</Religion></Patient><PatientVisit><AccidentCode /><AccidentDateTime /><AdmissionType>EL</AdmissionType><AdmitDateTime>201707181000</AdmitDateTime><AdmitDateTimeExpected /><AdmitReason /><AdmitSource>PHY</AdmitSource><AdmittingDoctor><ContactInfo /><FamilyName /><GivenName /><MiddleName /><Prefix /><Suffix /><PhysicianId /></AdmittingDoctor><AttendingDoctor><ContactInfo /><FamilyName>GOLDPIN</FamilyName><GivenName>STEPHANIE</GivenName><MiddleName>I</MiddleName><Prefix /><Suffix /><PhysicianId>GOLDST</PhysicianId></AttendingDoctor><Bed /><ConsultingDoctor><ContactInfo /><FamilyName /><GivenName /><MiddleName /><Prefix /><Suffix /><PhysicianId /></ConsultingDoctor><DischargeDateTime /><FinancialClass>31</FinancialClass><HospitalService /><PatientClass>P</PatientClass><PatientLocation>RADH</PatientLocation><PatientType>CLI</PatientType><PointOfCare>RADH</PointOfCare><PreAdmitTestDate /><ReferringDoctor><ContactInfo /><FamilyName /><GivenName /><MiddleName /><Prefix /><Suffix /><PhysicianId /></ReferringDoctor><Room /><ServicingFacility>TSH</ServicingFacility><VIPIndicator /><VisitNumber /></PatientVisit><PriorBalance><SetId>0</SetId><UnpaidBalance /><UnpaidBalanceType /></PriorBalance><ProcedureList /><RawMessage>MSH|^~\&amp;||TSH|||201707170855||SIU^S14|254173415|P|2.3|||AL|NE|SCH|V10005290943|3221204||||EDIT||INTCONSULT|30|min|201707181000|||||||||||EDIT|PID|1||M000848892^^^^MR^TSH~M858147^^^^PI^TSH~HUBLIVE0551914^^^^HUB^TSH||WHITE^NAOMI^^^^^L||19720904|F||3|25 OLD KINGS HWY N^APT 13 171^DARIEN^CT^06820||(518)779-0090|(203)965-6000||S|CHR|V00046246005|999-99-9999|PV1|1|P|RADH|EL|||GOLDST^GOLDPIN^STEPHANIE^I^^^MD|||||||PHY||||CLI||31|||||||||||||||||||TSH||PRE|||201707181000|RGS|1||INTERVENT|RGS|2||IR|RGS|3||RADH|AIS|1||INTCONS|201707181000|||30|min|EDIT|AIS|2||IR CONSULT RM|201707181000|||30|min|EDIT|AIS|3||LAZZBR|201707181000|||30|min|EDIT|AIL|1||CATHH|AIL|2||RADH|AIL|3||RADH|AIP|1||GOLDST^GOLDPIN^STEPHANIE^I^^^MD|ATT|||||30|min| </RawMessage></ADTMessage>";
		}

		public string GetXmlSerializeTestMessage()
		{
			return @"<?xml version=""1.0"" encoding=""utf-16"" ?>
		<Employee xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
		<FirstName>Code</FirstName> 
		<LastName>Handbook</LastName> 
		</Employee>";
		}
	}
}
