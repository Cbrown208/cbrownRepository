using System.Data.Entity.ModelConfiguration;
using MvcExercise.Core.Models;

namespace MvcExercise.DataAccess.Maps
{
	public class ContactMap : EntityTypeConfiguration<Contact>
	{
		public ContactMap()
		{
			//table mappings
			HasKey(x => new {x.ContactID});
			Property(x => x.FirstName).HasColumnName("FirstName");
			Property(x => x.FirstName).HasColumnName("FirstName");
			Property(x => x.LastName).HasColumnName("LastName");
			Property(x => x.Telephone).HasColumnName("Telephone");
			Property(x => x.EmailAddress).HasColumnName("EmailAddress");
			Property(x => x.BestTimeToCall).HasColumnName("BestTimeToCall");

			ToTable("Contact");
		}
	}
}
