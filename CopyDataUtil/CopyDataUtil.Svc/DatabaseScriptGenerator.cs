using System;
using System.Collections.Generic;

namespace CopyDataUtil.Svc
{
	public class DatabaseScriptGenerator
	{
		public string GenerateScriptFromList()
		{
			var insertQuery = "";
			var sqlString = GetSqlString();
			var dataList = GetDataList();
			var descriptionList = GetDescriptionList();
			var environment = "1";

			for (int i = 0; i < dataList.Count; i++)
			{
				insertQuery = insertQuery + string.Format(sqlString, dataList[i], descriptionList[i]);
			}

			

			return insertQuery;
		}

		private List<string> GetDescriptionList()
		{
			var descList = new List<string>
			{
				"Old CP Server",
				"ETL of Svc Cat",
				"Enrollment",
				"Enrollment",
				"People",
				"People",
				"PasWebSvc",
				"IR for SvcCat",
				"PasWebSvc",
				"RegQA Service",
				"RegQA Service",
				"RegQA Api",
				"RegQA Api",
				"RegQA",
				"RegQA",
				"CFTE",
				"CFTE",
				"PasNG",
				"PasNG",
			};
			return descList;
		}

		private List<string> GetDataList()
		{
			var data = new List<string>()
			{
				"RCM41VDCPAPP01.medassets.com",
				"RCM41VDCPAPP02.medassets.com",
				"RCM41VDCPWEB01.medassets.com"
			};
			return data;
		}

		private string GetSqlString()
		{
			var sqlString = @"IF NOT exists(select 1 from [Servers] where ServerName = '{0}' AND Environment = {2})
BEGIN
INSERT INTO [dbo].[Servers] ([ServerName],[ServerDescription],[Environment],[Status],[IsActive],[LastUpdated]) VALUES
('{0}','{1}',{2},0,1,GETDATE())
END";
			sqlString = sqlString + Environment.NewLine+ Environment.NewLine;
			return sqlString;
		}
	}
}
