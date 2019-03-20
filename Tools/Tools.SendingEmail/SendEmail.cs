using System.Net;
using System.Net.Mail;

namespace Tools.SendingEmail
{
	public class SendEmail
	{
		public int SendMail()
		{
			var fromAddress = new MailAddress("cb83050@gmail.com", "cb83050@gmail.com");
			var toAddress = new MailAddress("cr.brown208@gmail.com", "To Name");
			const string fromPassword = "J7Y29FAzwaxs";
			const string subject = "SendMail Function";
			const string body = "Hey now!!";

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
				Timeout = 20000
			};
			using (var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
				Body = body
			})
			{
				smtp.Send(message);
			}
			return 1;
		}

		public int SendMailWithBody(string stats)
		{
			var fromAddress = new MailAddress("cb83050@gmail.com", "cb83050@gmail.com");
			var toAddress = new MailAddress("cr.brown208@gmail.com", "To Name");
			const string fromPassword = "J7Y29FAzwaxs";
			const string subject = "Manual Backup Update";
			string body = stats;

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
				Timeout = 20000
			};
			using (var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
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
