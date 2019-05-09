namespace Tools.SendingEmail.Models
{
	public class CopyDetails
	{
		public string Total { get; set; }
		public string Copied { get; set; }
		public string Skipped { get; set; }
		public string Mismatched { get; set; }
		public string Failed { get; set; }
		public string Extras { get; set; }
	}
}
