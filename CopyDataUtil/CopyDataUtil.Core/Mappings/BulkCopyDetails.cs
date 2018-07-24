namespace CopyDataUtil.Core.Mappings
{
	public class BulkCopyDetails
	{
		public string SourceConnectionString { get; set; }
		public string DestinationConnectionString { get; set; }
		public Configuration Config { get; set; }
	}
}
