using System;

namespace CopyDataUtil.Core.Models
{
	public class CpPackage_Params
	{
		public DateTime? EFF_DATE { get; set; }
		public DateTime? TRM_DATE { get; set; }
		public int LOSMAX { get; set; }
		public int LOSMIN { get; set; }
		public string CDATE { get; set; }
		public string CTIME { get; set; }
		public string CUSER { get; set; }
		public string DATEFROM { get; set; }
		public string DATETO { get; set; }
		public string DOMDEF { get; set; }
		public string MDATE { get; set; }
		public string MTIME { get; set; }
		public string MUSER { get; set; }
		public string NPI { get; set; }
		public string PATTYPE { get; set; }
		public string SYSKEY { get; set; }
	}
}
