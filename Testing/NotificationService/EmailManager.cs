using System.Net.Mail;

namespace NotificationService
{
	public class EmailManager
	{
		public void SendEmail(string emailList, int messageCount)
		{
			var smtp = new SmtpClient("mailrelayldc.medassets.com");
			var mailMessage = new MailMessage();

			mailMessage.To.Add(emailList);
			mailMessage.From = new MailAddress("noreply@medassets.com", "RegQaPublisherIssue");
			mailMessage.Subject = "RegQa Issue happening ";
			mailMessage.Body = "NTHR_AMS_RegQaMessagePublisher Issue " + messageCount + " messages are in the ready state.";
			mailMessage.IsBodyHtml = true;

			smtp.Send(mailMessage);
		}
	}
}
