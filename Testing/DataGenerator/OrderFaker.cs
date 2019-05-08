using Bogus;

namespace DataGenerator
{
	public sealed class OrderFaker : Faker<Order>
	{
		public OrderFaker()
		{
			RuleFor(o => o.OrderId, f => f.Random.Number(1, 100));
			RuleFor(o => o.Item, f => f.Lorem.Sentence());
			RuleFor(o => o.Quantity, f => f.Random.Number(1, 10));
		}
	}
}
