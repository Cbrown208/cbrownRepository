using System.Data.Entity;
using MvcExercise.Core.Models;
using MvcExercise.DataAccess.Maps;

namespace MvcExercise.DataAccess.Context
{
	public class MvcExerciseContext : DbContext
	{
		public MvcExerciseContext()
			: base("Name = MvcExerciseDb")
		{ }
		//public IDbSet<Contact> Contact { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new ContactMap());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Contact> Contacts { get; set; }
	}
}

