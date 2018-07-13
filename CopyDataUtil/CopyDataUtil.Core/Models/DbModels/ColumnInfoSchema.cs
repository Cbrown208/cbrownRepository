namespace CopyDataUtil.Core.Models.DbModels
{
	public class ColumnInfoSchema
	{
		public string Table_Catalog { get; set; }
		public string Table_Schema { get; set; }
		public string Table_Name { get; set; }
		public string Column_Name { get; set; }
		public int Ordinal_Position { get; set; }
		public string Column_Default { get; set; }
		public string Is_Nullable { get; set; }
		public string Data_Type { get; set; }
		public int Character_Maximum_Length { get; set; }
	}
}
