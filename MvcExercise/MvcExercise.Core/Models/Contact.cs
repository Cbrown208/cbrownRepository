using System;

namespace MvcExercise.Core.Models
{
	public class Contact
	{
		public Guid ContactID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Telephone { get; set; }
		public string EmailAddress { get; set; }
		public DateTime BestTimeToCall { get; set; }
	}
}
