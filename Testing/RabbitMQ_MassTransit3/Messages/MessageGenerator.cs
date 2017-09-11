using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAssets.AMS.Common;

namespace RabbitMQ_MassTransit3.Messages
{
	public class MessageGenerator
	{
		private static long _messageControlId;
		public IAdtQueueMessage ValidAdtQueueMessage()
		{
			return new AdtQueueMessage()
			{
				AccountNumber = "12345",
				ClientId = 55555,
				FacilityId = "FacilityIdTest",
				Message = GetValidMessage(),
				MessageControlId = 123255
			};
		}

		public string GetValidMessage()
		{
			var msg =
				"<ADTMessage><Header><MessageControlId>589962691</MessageControlId><MessageTimestamp>201603311232</MessageTimestamp><MessageType>ADT</MessageType><SendingApplication>ADM</SendingApplication><SendingFacility>ST</SendingFacility><SendingFacilityId>986B8285-662B-4576-ADFD-A2F87D1A0338</SendingFacilityId><ReceivingApplication/><ReceivingFacility/><RecordedTimestamp>201603311232</RecordedTimestamp><EventType>A08</EventType><OperatorId/><ClientId>52060</ClientId></Header><Patient><FamilyName>WELLS</FamilyName><GivenName>RENE</GivenName><MiddleName/><Prefix/><Suffix/><SSN/><DriversLicense/><DateOfBirth>19820824</DateOfBirth><DateOfDeath/><Gender>M</Gender><ContactInfo><AddressLine1>POBOX1010</AddressLine1><AddressLine2/><City>CANONCITY</City><State>CO</State><PostalCode>81212</PostalCode><County/><Country/><HomePhone>(719)275-4181</HomePhone><BusinessPhone/><MobilePhone/><Fax/><Email/></ContactInfo><AccountNumber>TA0000404234</AccountNumber><MRN>TM00178363</MRN><Race/><Religion/><EthnicGroup/></Patient><PatientVisit><AdmitDateTime>201603011043</AdmitDateTime><PatientType>CLI</PatientType><PatientClass>O</PatientClass><HospitalService/><PatientLocation/><PointOfCare/><Room/><Bed/><FinancialClass/><AdmissionType/><DischargeDateTime/><ServicingFacility/><AdmitSource/><VIPIndicator/><AdmitReason/><AdmitDateTimeExpected/><PreAdmitTestDate/><AccidentDateTime/><AccidentCode/><VisitNumber/><AdmittingDoctor><FamilyName/><GivenName/><MiddleName/><Prefix/><Suffix/><ContactInfo/><PhysicianId/></AdmittingDoctor><AttendingDoctor><FamilyName>Rife</FamilyName><GivenName>Celia</GivenName><MiddleName>Elizabeth</MiddleName><Prefix/><Suffix/><ContactInfo/><PhysicianId>RIFECE</PhysicianId></AttendingDoctor><ReferringDoctor><FamilyName/><GivenName/><MiddleName/><Prefix/><Suffix/><ContactInfo/><PhysicianId/></ReferringDoctor><ConsultingDoctor><FamilyName/><GivenName/><MiddleName/><Prefix/><Suffix/><ContactInfo/><PhysicianId/></ConsultingDoctor></PatientVisit><InsuranceList><Insurance><Ordinality>1</Ordinality><PlanId>DOC</PlanId><GroupNumber>CDOC</GroupNumber><GroupName/><PolicyNumber>95519</PolicyNumber><PolicyDeductible/><PolicyLimit/><PolicyLimitDays/><PlanEffectiveDate/><PlanExpirationDate/><PreCertNumber/><CoordinationBenefitsPriority/><Payor><ContactInfo><AddressLine1/><AddressLine2/><City/><State/><PostalCode/><County/><Country/><HomePhone/><BusinessPhone/><MobilePhone/><Fax/><Email/></ContactInfo><CompanyId/><Name/><Description/><WebsiteUrl/></Payor><Insured><FamilyName>WELLS</FamilyName><GivenName>RENE</GivenName><MiddleName/><Prefix/><Suffix/><SSN/><DateOfBirth/><Gender/><ContactInfo><AddressLine1/><AddressLine2/><City/><State/><PostalCode/><County/><Country/><HomePhone/><BusinessPhone/><MobilePhone/><Fax/><Email/></ContactInfo><RelationshipToPatient>SLF</RelationshipToPatient><EmployerName/></Insured><CoPayment/><CoInsurance/><RemDeductible/><RemMaxOutOfPocket/></Insurance></InsuranceList><InsuranceVerificationList><InsuranceVerification><AuthorizationObtained/><VerificationStatus/><ReportOfEligibility/><ReportOfEligibilityDate/><PreAdmitCert/><VerificationDateTime/><VerificationBy/></InsuranceVerification></InsuranceVerificationList><Guarantor/><NextOfKinList/><DiagnosisList/><ProcedureList/><RawMessage>MSH|^~\\&amp;|ADM|ST|||201603311232||ADT^A08|589962691|P|2.2EVN|A08|201603311232PID|1||TM00178363|CM467529|WELLS^RENE||19820824|M|||POBOX1010^^CANONCITY^CO^81212||(719)275-4181|||||TA0000404234PD1||||CALDAP^Caldwell^April^Daylene^^^PAPV1|1|O|||||RIFECE^Rife^Celia^Elizabeth^^^NP|||||||||||CLI||||||||||||||||||||||||||201603011043IN1|1|DOC||DOCCORRECTIONALHLTHPARTNERS||||CDOC||||||||WELLS^RENE|SLF|||||||||||||||||||95519</RawMessage><CreatedOn>0001-01-01T00:00:00</CreatedOn></ADTMessage>";
			return msg;
		}

		public IAdtQueueMessage GetAdtQueueMessage()
		{
			Random random = new Random();
			int randomNumber = random.Next(00000000, 99999999);
			_messageControlId = _messageControlId + 1;
			string accountNumber = "TA" + randomNumber;

			var message = new AdtQueueMessage
			{
				AccountNumber = accountNumber,
				Message = GetValidMessage(),
				MessageControlId = _messageControlId,
				Timestamp = DateTimeOffset.Now,
				ClientId = 3503
			};

			message.FacilityId = message.ClientId == 3503 ? "AA33D4E9-A389-41B4-BE85-A1F7EFF4B37B" : "32430F0F-82AA-4EDB-BD37-6886D1956CAB";

			return message;
		}
	}
}
