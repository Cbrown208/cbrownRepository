using System.Net;
using System.Net.Mail;

namespace Tools.SendingEmail
{
	public class SendEmail
	{
		private readonly MailAddress _fromAddress = new MailAddress("cb83050@gmail.com", "cb83050@gmail.com");
		private readonly MailAddress _toAddress = new MailAddress("cr.brown208@gmail.com", "To Name");
		private const string FromPassword = "J7Y29FAzwaxs";
		private const string EmailSubject = "Manual Backup Update";

		public int SendMailWithBody(string stats)
		{
			var body = stats;
			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Credentials = new NetworkCredential(_fromAddress.Address, FromPassword),
				Timeout = 20000
			};
			using (var message = new MailMessage(_fromAddress, _toAddress)
			{
				Subject = EmailSubject,
				Body = body,
				IsBodyHtml = true
			})
			{
				smtp.Send(message);
			}
			return 1;
		}
	}
}
