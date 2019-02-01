namespace DataAccessTesting
{
	public class BulkCopyDetails
	{
		public string DestinationConnectionString { get; set; }
		public int BatchSize { get; set; }
		public string TableName { get; set; }
	}
}
