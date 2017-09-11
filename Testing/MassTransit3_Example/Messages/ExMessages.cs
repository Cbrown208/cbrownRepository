using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAssets.AMS.Common;

namespace MassTransit3_Example.Messages
{
	public class ExMessages
	{
		private static long _messageControlId;
		public IAdtQueueMessage GetAdtQueueMessageWithAccountNumber(string accountNumber)
		{
			//string accountNumber = "TA" + randomNumber;
			_messageControlId = _messageControlId + 4;
			var message = new AdtQueueMessage
			{
				AccountNumber = accountNumber,
				Message = GetValidMessage(accountNumber),
				SocketTimestamp = DateTimeOffset.Now,
				Timestamp = DateTimeOffset.Now,
				ClientId = 15004,
				FacilityId = "CF700803-F63C-4DB2-BFAF-904D2CA1A384"
			};
			
			message.FacilityId = "CF700803-F63C-4DB2-BFAF-904D2CA1A384";
			//message.FacilityId = message.ClientId == 3503 ? "AA33D4E9-A389-41B4-BE85-A1F7EFF4B37B";

			return message;
		}

		public string GetValidMessage(string accountNumber)
		{
			var msg =
				"<ADTMessage><CreatedOn>0001-01-01T00:00:00</CreatedOn><DiagnosisList /><Guarantor><ContactInfo><AddressLine1>107 RICHMOND HILL AVE</AddressLine1><AddressLine2 /><BusinessPhone /><City>STAMFORD</City><Country /><County /><Email /><Fax /><HomePhone>(203)809-6781</HomePhone><MobilePhone /><PostalCode>06902</PostalCode><State>CT</State></ContactInfo><DateOfBirth /><FamilyName>HENRY</FamilyName><Gender /><GivenName>NATASHA</GivenName><MiddleName /><Prefix /><SSN>999999999</SSN><Suffix /><RelationshipToPatient>MO</RelationshipToPatient><Priority /></Guarantor><Header><ClientId>15004</ClientId><EventType>A08</EventType><MessageControlId>218341805</MessageControlId><MessageTimestamp>201605100909</MessageTimestamp><MessageType>ADT</MessageType><OperatorId>EDMBKGJOB</OperatorId><ReceivingApplication /><ReceivingFacility /><RecordedTimestamp>201605100909</RecordedTimestamp><SendingApplication>ADM</SendingApplication><SendingFacility /><SendingFacilityId>CF700803-F63C-4DB2-BFAF-904D2CA1A384</SendingFacilityId></Header><InsuranceList><Insurance><CoInsurance /><CoPayment /><CoordinationBenefitsPriority /><GroupName /><GroupNumber>227079</GroupNumber><Insured><ContactInfo><AddressLine1 /><AddressLine2 /><BusinessPhone /><City /><Country /><County /><Email /><Fax /><HomePhone /><MobilePhone /><PostalCode /><State /></ContactInfo><DateOfBirth /><FamilyName>BROWN</FamilyName><Gender /><GivenName>TYLER</GivenName><MiddleName /><Prefix /><SSN /><Suffix /><RelationshipToPatient>MO</RelationshipToPatient><EmployerName>WHOLE FOODS</EmployerName></Insured><Ordinality>1</Ordinality><Payor><CompanyId /><ContactInfo><AddressLine1 /><AddressLine2 /><BusinessPhone /><City /><Country /><County /><Email /><Fax /><HomePhone /><MobilePhone /><PostalCode /><State /></ContactInfo><Description /><Name /><WebsiteUrl /></Payor><PlanEffectiveDate /><PlanExpirationDate /><PlanId>UNITCCP</PlanId><PolicyDeductible /><PolicyLimit /><PolicyLimitDays /><PolicyNumber>957978865</PolicyNumber><PreCertNumber /><RemDeductible /><RemMaxOutOfPocket /></Insurance></InsuranceList><InsuranceVerificationList><InsuranceVerification><AuthorizationObtained /><PreAdmitCert /><ReportOfEligibility /><ReportOfEligibilityDate /><VerificationBy>SLOWE</VerificationBy><VerificationDateTime>20160510</VerificationDateTime><VerificationStatus>PENDING</VerificationStatus></InsuranceVerification></InsuranceVerificationList><NextOfKinList><NextOfKin><ContactInfo><AddressLine1>107 RICHMOND HILL AVE</AddressLine1><AddressLine2 /><BusinessPhone /><City>STAMFORD</City><Country /><County /><Email /><Fax /><HomePhone>(203)809-6781</HomePhone><MobilePhone /><PostalCode>06902</PostalCode><State>CT</State></ContactInfo><DateOfBirth /><FamilyName>HENRY</FamilyName><GivenName>NATASHA</GivenName><MiddleName /><Prefix /><SSN /><Suffix /><RelationshipToPatient>MO</RelationshipToPatient><ContactRole>NOK</ContactRole></NextOfKin><NextOfKin><ContactInfo><AddressLine1>107 RICHMOND HILL AVE</AddressLine1><AddressLine2 /><BusinessPhone /><City>STAMFORD</City><Country /><County /><Email /><Fax /><HomePhone>(203)809-6781</HomePhone><MobilePhone /><PostalCode>06902</PostalCode><State>CT</State></ContactInfo><DateOfBirth /><FamilyName>HENRY</FamilyName><GivenName>NATASHA</GivenName><MiddleName /><Prefix /><SSN /><Suffix /><RelationshipToPatient>MO</RelationshipToPatient><ContactRole>NOT</ContactRole></NextOfKin></NextOfKinList><Patient><ContactInfo><AddressLine1>107 RICHMOND HILL AVE</AddressLine1><AddressLine2 /><BusinessPhone /><City>STAMFORD</City><Country /><County /><Email /><Fax /><HomePhone>(203)809-6781</HomePhone><MobilePhone /><PostalCode>06902</PostalCode><State>CT</State></ContactInfo><DateOfBirth>20100903</DateOfBirth><DateOfDeath /><DriversLicense /><FamilyName>MIDDLETON</FamilyName><Gender>F</Gender><GivenName>JAMYLA</GivenName><MiddleName>R</MiddleName><Prefix /><SSN>999999999</SSN><Suffix /><AccountNumber>" +accountNumber +"</AccountNumber><EthnicGroup /><MRN>M000814543</MRN><Race /><Religion>NON</Religion></Patient><PatientVisit><AccidentCode /><AccidentDateTime /><AdmissionType>UR</AdmissionType><AdmitDateTime>201605100848</AdmitDateTime><AdmitDateTimeExpected /><AdmitReason>COUGH</AdmitReason><AdmitSource>PHY</AdmitSource><AdmittingDoctor><ContactInfo /><FamilyName /><GivenName /><MiddleName /><Prefix /><Suffix /><PhysicianId /></AdmittingDoctor><AttendingDoctor><ContactInfo /><FamilyName>ZEH</FamilyName><GivenName>KIM</GivenName><MiddleName /><Prefix /><Suffix /><PhysicianId>ZEHKIM</PhysicianId></AttendingDoctor><Bed /><ConsultingDoctor><ContactInfo /><FamilyName /><GivenName /><MiddleName /><Prefix /><Suffix /><PhysicianId /></ConsultingDoctor><DischargeDateTime /><FinancialClass>21</FinancialClass><HospitalService /><PatientClass>E</PatientClass><PatientLocation>ICCT</PatientLocation><PatientType>ER</PatientType><PointOfCare>ICCT</PointOfCare><PreAdmitTestDate /><ReferringDoctor><ContactInfo /><FamilyName /><GivenName /><MiddleName /><Prefix /><Suffix /><PhysicianId /></ReferringDoctor><Room /><ServicingFacility>TSH</ServicingFacility><VIPIndicator /><VisitNumber /></PatientVisit><ProcedureList /><RawMessage>MSH|^~\\&amp;|ADM||||201605100909||ADT^A08|218341805|P|2.2|||AL|NEEVN|A08|201605100909|||EDMBKGJOB^^^^^^|PID|1|HUBLIVE0509040|M000814543|M818758|MIDDLETON^JAMYLA^R^^^||20100903|F|^^^^^|2|107 RICHMOND HILL AVE^^STAMFORD^CT^06902||(203)809-6781|||S|NON|" +accountNumber +"|999-99-9999||PD1|1|||ADADAN^ADADE^ANDREWS^A^^^MD|NK1|1|HENRY^NATASHA^^^^|MO^G8 MOTHER|107 RICHMOND HILL AVE^^STAMFORD^CT^06902|(203)809-6781||NOKNK1|2|HENRY^NATASHA^^^^|MO^G8 MOTHER|107 RICHMOND HILL AVE^^STAMFORD^CT^06902|(203)809-6781||NOTPV1|1|E|ICCT^^|UR|||ZEHKIM^ZEH^KIM^^^^MD|||||||PHY||||ER||21|||||||||||||||||||TSH||REG|||201605100848||||||PV2|||COUGHOBX|1|ST|1010.1^^CPT4||20.91|kg|||||FAL1|1|DA^^^Allergy|NKDA^NO KNOWN DRUG ALLERGIES^CODED|||20141230GT1|1||HENRY^NATASHA^^^^||107 RICHMOND HILL AVE^^STAMFORD^CT^06902|(203)809-6781|||||MO|999-99-9999||||WHOLE FOODS|350 GRASMERE AVE^^FAIRFIELD^CT^06824|(203)319-9544||FT|IN1|1|UNITCCP||UNITED HEALTHCARE CHOICE PLUS|UNITED HEALTHCARE CHOICE PLUS^PO BOX 7408000^ATLANTA^GA^30374-0800||(800)521-5505|227079|||WHOLE FOODS|||||BROWN^TYLER^^^^|MO||||||||||||20160510|SLOWE||||||957978865||||||FT^1 FULL-TIME EMPLOYED|||PENDINGUB2||||||| </RawMessage></ADTMessage>";
			return msg;
		}

	}
}
