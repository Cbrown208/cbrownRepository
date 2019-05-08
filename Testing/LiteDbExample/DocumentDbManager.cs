using System;
using LiteDB;

namespace LiteDbExample
{
	public class DocumentDbManager
	{

		public void QueryDocumentDb()
		{
			// Open database (or create if not exits)
			using (var db = new LiteDatabase(@"MyData.db"))
			{
				// Get customer collection
				var customers = db.GetCollection<Customer>("customers");

				Console.WriteLine(customers.ToString());

				// Create your new customer instance
				var customer = new Customer
				{
					Name = "John Doe",
					Phones = new string[] {"8000-0000", "9000-0000"},
					IsActive = true
				};

				// Insert new customer document (Id will be auto-incremented)
				customers.Insert(customer);

				// Update a document inside a collection
				customer.Name = "Joana Doe";

				customers.Update(customer);

				Console.WriteLine(customers.ToString());
				// Index document using a document property
				customers.EnsureIndex(x => x.Name);

				// Use Linq to query documents
				var results = customers.Find(x => x.Name.StartsWith("Jo"));
				Console.WriteLine(results.ToString());

			}
		}

		public void StoreDocument(string path)
		{
			using (var db = new LiteDatabase(@"MyData.db"))
			{
				// Upload a file from file system to database
				db.FileStorage.Upload("my-photo-id", @"picture-01.jpg");

				
				// And download later
				//var image = db.FileStorage.Download("my-photo-id", @"C:\myscripts\temp\copy-of-picture-01.jpg");
			}
		}

		// Basic example
		public class Customer
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string[] Phones { get; set; }
			public bool IsActive { get; set; }
		}

	}
}
