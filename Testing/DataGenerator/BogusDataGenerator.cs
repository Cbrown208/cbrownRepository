using System;
using Bogus;

namespace DataGenerator
{
	public class BogusDataGenerator
	{
		public void Using_The_Faker_Facade()
		{
			var faker = new Faker("en");
			var o = new Order()
			{
				OrderId = faker.Random.Number(1, 100),
				Item = faker.Lorem.Sentence(),
				Quantity = faker.Random.Number(1, 10)
			};
			Console.WriteLine("Id: " + o.OrderId + Environment.NewLine + "Item: " + o.Item + Environment.NewLine +
							  "Quantity: " + o.Quantity);
		}

		public void Using_DataSets_Directly()
		{
			var random = new Bogus.Randomizer();
			var lorem = new Bogus.DataSets.Lorem("en");
			var o = new Order()
			{
				OrderId = random.Number(1, 100),
				Item = lorem.Text(),
				Quantity = random.Number(1, 10)
			};
			Console.WriteLine("Id: " + o.OrderId + Environment.NewLine + "Item: " + o.Item + Environment.NewLine + "Quantity: "
			                  + o.Quantity);
		}

		public void Using_FakerT_Inheritance()
		{
			var orderFaker = new OrderFaker();
			var o = orderFaker.Generate();
			Console.WriteLine("Id: " + o.OrderId + Environment.NewLine + "Item: " + o.Item + Environment.NewLine + "Quantity: "
			+ o.Quantity);
		}

	}
}
