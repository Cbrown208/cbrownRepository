using System;
using System.Text;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using RabbitMQ.Client;

namespace RabbitMQ_MassTransit3
{
    public class TestPublisher 
    {
        public void Publish()
        {

            Log4NetLogger.Use();
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
                x.Host(new Uri("rabbitmq://RCM41VQPASAPP10"), h =>
                {
                   h.Username("Pas");
                    h.Password("Pas");
                }));
            
            var busHandle = bus.Start();
            var text = "";

            while (text != "quit")
            {
                Console.Write("Enter a message: ");
                text = Console.ReadLine();

                var message = new SomethingHappenedMessage()
                {
                    What = text,
                    //What = GetMessage(),
                    When = DateTime.Now
                };

                bus.Publish(message);
                //bus.Publish<SomethingHappenedMessage>(message);
                SendMessage();
            }

            busHandle.Stop();
        }

        public void SendMessage() { 
            var factory = new ConnectionFactory() { HostName = "rabbitmq://RCM41VQPASAPP03" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Adt_BabyBird", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

		private static string GetMessage()
		{
			return
				@"<ADTMessage><Header><MessageControlId>589962691</MessageControlId><MessageTimestamp>201603311232</MessageTimestamp><MessageType>ADT</MessageType><SendingApplication>ADM</SendingApplication><SendingFacility>ST</SendingFacility><SendingFacilityId>986B8285-662B-4576-ADFD-A2F87D1A0338</SendingFacilityId><ReceivingApplication/><ReceivingFacility/><RecordedTimestamp>201603311232</RecordedTimestamp><EventType>A08</EventType><OperatorId/><ClientId>52060</ClientId></Header><Patient><FamilyName>WELLS</FamilyName><GivenName>RENE</GivenName><MiddleName/><Prefix/><Suffix/><SSN/><DriversLicense/><DateOfBirth>19820824</DateOfBirth><DateOfDeath/><Gender>M</Gender><ContactInfo><AddressLine1>POBOX1010</AddressLine1><AddressLine2/><City>CANONCITY</City><State>CO</State><PostalCode>81212</PostalCode><County/><Country/><HomePhone>(719)275-4181</HomePhone><BusinessPhone/><MobilePhone/><Fax/><Email/></ContactInfo><AccountNumber>TA0000404234</AccountNumber><MRN>TM00178363</MRN><Race/><Religion/><EthnicGroup/></Patient><PatientVisit><AdmitDateTime>201603011043</AdmitDateTime><PatientType>CLI</PatientType><PatientClass>O</PatientClass><HospitalService/><PatientLocation/><PointOfCare/><Room/><Bed/><FinancialClass/><AdmissionType/><DischargeDateTime/><ServicingFacility/><AdmitSource/><VIPIndicator/><AdmitReason/><AdmitDateTimeExpected/><PreAdmitTestDate/><AccidentDateTime/><AccidentCode/><VisitNumber/><AdmittingDoctor><FamilyName/><GivenName/><MiddleName/><Prefix/><Suffix/><ContactInfo/><PhysicianId/></AdmittingDoctor><AttendingDoctor><FamilyName>Rife</FamilyName><GivenName>Celia</GivenName><MiddleName>Elizabeth</MiddleName><Prefix/><Suffix/><ContactInfo/><PhysicianId>RIFECE</PhysicianId></AttendingDoctor><ReferringDoctor><FamilyName/><GivenName/><MiddleName/><Prefix/><Suffix/><ContactInfo/><PhysicianId/></ReferringDoctor><ConsultingDoctor><FamilyName/><GivenName/><MiddleName/><Prefix/><Suffix/><ContactInfo/><PhysicianId/></ConsultingDoctor></PatientVisit><InsuranceList><Insurance><Ordinality>1</Ordinality><PlanId>DOC</PlanId><GroupNumber>CDOC</GroupNumber><GroupName/><PolicyNumber>95519</PolicyNumber><PolicyDeductible/><PolicyLimit/><PolicyLimitDays/><PlanEffectiveDate/><PlanExpirationDate/><PreCertNumber/><CoordinationBenefitsPriority/><Payor><ContactInfo><AddressLine1/><AddressLine2/><City/><State/><PostalCode/><County/><Country/><HomePhone/><BusinessPhone/><MobilePhone/><Fax/><Email/></ContactInfo><CompanyId/><Name/><Description/><WebsiteUrl/></Payor><Insured><FamilyName>WELLS</FamilyName><GivenName>RENE</GivenName><MiddleName/><Prefix/><Suffix/><SSN/><DateOfBirth/><Gender/><ContactInfo><AddressLine1/><AddressLine2/><City/><State/><PostalCode/><County/><Country/><HomePhone/><BusinessPhone/><MobilePhone/><Fax/><Email/></ContactInfo><RelationshipToPatient>SLF</RelationshipToPatient><EmployerName/></Insured><CoPayment/><CoInsurance/><RemDeductible/><RemMaxOutOfPocket/></Insurance></InsuranceList><InsuranceVerificationList><InsuranceVerification><AuthorizationObtained/><VerificationStatus/><ReportOfEligibility/><ReportOfEligibilityDate/><PreAdmitCert/><VerificationDateTime/><VerificationBy/></InsuranceVerification></InsuranceVerificationList><Guarantor/><NextOfKinList/><DiagnosisList/><ProcedureList/><RawMessage>MSH|^~\\&amp;|ADM|ST|||201603311232||ADT^A08|589962691|P|2.2EVN|A08|201603311232PID|1||TM00178363|CM467529|WELLS^RENE||19820824|M|||POBOX1010^^CANONCITY^CO^81212||(719)275-4181|||||TA0000404234PD1||||CALDAP^Caldwell^April^Daylene^^^PAPV1|1|O|||||RIFECE^Rife^Celia^Elizabeth^^^NP|||||||||||CLI||||||||||||||||||||||||||201603011043IN1|1|DOC||DOCCORRECTIONALHLTHPARTNERS||||CDOC||||||||WELLS^RENE|SLF|||||||||||||||||||95519</RawMessage><CreatedOn>0001-01-01T00:00:00</CreatedOn></ADTMessage>";
		}
	}
}
