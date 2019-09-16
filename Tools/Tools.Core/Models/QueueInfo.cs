namespace Tools.Core.Models
{
	public class QueueInfo
	{
		public string Name { get; set; }
		public int Consumers { get; set; }
		public int Messages { get; set; }
		public bool Durable { get; set; }
	}
}
