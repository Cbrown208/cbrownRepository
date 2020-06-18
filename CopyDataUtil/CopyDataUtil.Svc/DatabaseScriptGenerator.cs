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

			for (int i = 0; i < dataList.Count; i++)
			{
				insertQuery = insertQuery + string.Format(sqlString, dataList[i], descriptionList[i]);
			}

			

			return insertQuery;
		}

		private List<string> GetDescriptionList()
		{
			var descList = new List<string>()
			{
				"ETL of Svc Cat",
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
				"RCM40VPPASAPP22.medassets.com",
				"RCM40VPPASAPP23.medassets.com",
				"RCM40VPPASAPP24.medassets.com",
				"RCM40VPPASAPP25.medassets.com",
				"RCM40VPPASAPP26.medassets.com",
				"RCM40VPPASAPP27.medassets.com",
				"RCM40VPPASAPP29.medassets.com",
				"RCM40VPPASAPP28.medassets.com",
				"RCM40VPPASAPP30.medassets.com",

				"RCM40VPPASWEB11.medassets.com",
				"RCM40VPPASWEB12.medassets.com",
				"RCM40VPPASWEB13.medassets.com",
				"RCM40VPPASWEB14.medassets.com",
				"RCM40VPPASWEB15.npce.com",
				"RCM40VPPASWEB16.npce.com",
				"RCM40VPPASWEB17.npce.com",
				"RCM40VPPASWEB18.npce.com",
				"RCM40VPPASWEB19.npce.com",
				"RCM40VPPASWEB20.npce.com"
			};
			return data;
		}

		private string GetSqlString()
		{
			var sqlString = @"BEGIN
if NOT exists(select 1 from [Servers] where ServerName = '{0}' AND Environment = 5)
INSERT INTO [dbo].[Servers] ([ServerName],[ServerDescription],[Environment],[Status],[IsActive],[LastUpdated]) VALUES
('{0}','{1}',5,0,1,GETDATE())
END";
			sqlString = sqlString + Environment.NewLine+ Environment.NewLine;
			return sqlString;
		}
	}
}
