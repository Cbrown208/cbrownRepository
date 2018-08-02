using System;
using System.Collections.Generic;

namespace Common.Formaters
{
	public class DatabaseStringFormatter
	{
		public List<string> GetSqlStringFormatList()
		{
			var sqlStringList = new List<string>
			{
				" all ", " and ", " any ", " as ", " between ","Case ", " char ", " count(", " create ","Create ", " cross ","dateadd(", " datetime ","declare ","Declare ","delete ", "distinct "," else "," Else ", " end ", " exec ", " exists ", "from ", "getdate()"," group by ","group by ", " if (","if("," if(","If ","isnull(", " in ", " index ", " inner "," Inner ", " insert ", " int ", " into ", " is ", " join ", " left ", " like ","ltrim(", " not ", " nvarchar", " on ", " or ", " order by ","order by ", " outer "," Procedure "," procedure ", " right ","replace(","rtrim(", "select ","Select ", " set "," Set ", " table ", " then ", " union ", " varchar", " when ", " where ", " xml "
			};
			return sqlStringList;
		}

		public string FormatSqlString(string str)
		{
			var sqlStringList = GetSqlStringFormatList();
			sqlStringList.Sort();

			foreach (var sqlOperator in sqlStringList)
			{
				if (str.Contains(sqlOperator))
				{
					str = str.Replace(sqlOperator, sqlOperator.ToUpper());
				}
			}
			return str;
		}

		public string FormatUsqlString(string str)
		{
			var sqlStringList = GetSqlStringFormatList();

			foreach (var sqlOperator in sqlStringList)
			{
				if (str.Contains(sqlOperator))
				{
					str = str.Replace(sqlOperator, sqlOperator.ToUpper());
				}
			}
			str = str.Replace(" = ", " == ");
			str = str.Replace(" #", " @");
			str = str.Replace("'", @"""");
			str = str.Replace("null", @"""""");
			str = str.Replace("--", @"//");
			str = str.Replace("         ", " ");
			str = str.Replace("        ", " ");
			str = str.Replace("  ", "");

			// CBO Custom 
			str = str.Replace("SYSKEY", "Syskey");
			str = str.Replace("PROCCODE", "ProcCode");
			str = str.Replace("CODE_CNT", "CodeCnt");
			str = str.Replace(" CODETYPE_CNT ", " CodeTypeCnt ");
			str = str.Replace(" CODETYPE", " CodeType");
			str = str.Replace("CODETYPE", "CodeType");
			str = str.Replace(" CODETYPE,", " CodeType,");
			str = str.Replace(".REVCODE", ".RevCode ");
			str = str.Replace("REVCODE", "RevCode");
			str = str.Replace("WITH (Nolock)", "");
			str = str.Replace("BILLMAST", "BillMast");
			str = str.Replace(".ICD_VER", ".IcdVer");
			str = str.Replace("@ICD_VER", "@Icd_Ver");
			str = str.Replace("PATTYPE", "PatientType");
			str = str.Replace("PATNUM", "PatientAccountNumber");
			str = str.Replace("PATDOB", "PatientDob");
			str = str.Replace("PATSEX", "PatientSex");
			str = str.Replace(@"ISNULL(M.ICD_VER,'9') == (CASE WHEN @ICD_VER ='9' THEN '9' WHEN @ICD_VER ='0'  THEN '0' END)",
			@"(string.IsNullOrEmpty(M.IcdVer) ? ""9"" : M.IcdVer) == @Icd_Ver");
			str = str.Replace(".RPC_TYPE", ".RepriceType");

			str = str.Replace(" DRG", " Drg");
			str = str.Replace(".DRG", ".Drg");
			str = str.Replace(" HCPCS", " Hcpcs");
			str = str.Replace(" DIAG_SEQUENCE", " DiagSequence");
			str = str.Replace("SEQUENCE", "Sequence");
			str = str.Replace(" BILLPROC", " BillProc");

			str = str.Replace("tmp_sampling C", "Tmp_Sampling AS C");
			str = str.Replace(".DIAGCODE", ".DiagCode");
			str = str.Replace("DIAGCODE", "DiagCode");
			str = str.Replace("BILLDIAG", "BillDiag");

			str = str.Replace("UNITS", "Units");
			str = str.Replace("CHARGES", "Charges");
			str = str.Replace(".LOS", ".Los");
			str = str.Replace(".AGE", ".Age");
			str = str.Replace(".SEX", ".Sex");
			str = str.Replace(".HCPCS", ".Hcpcs");
			str = str.Replace("ADMDATE", "AdmDate");
			str = str.Replace("HCPCSRATES", "HcpcsRates");
			str = str.Replace("HCPCS", "Hcpcs");

			str = str.Replace("snCP_PRO_CPTEXCL", "SncpProCptexcl");
			str = str.Replace("CPT_EXCL", "CptExcl");

			str = str.Replace("GROSSCHGS", "GrossCharges");
			str = str.Replace("Payer_Class", "PayerClass");
			str = str.Replace("ZLEVEL", "ZLevel");
			str = str.Replace("ZVALUE", "ZValue");

			str = str.Replace("HcpcsRATES", "HcpcsRates");
			str = str.Replace("PHYAVGCHGS", "PhyAvgChgs");
			str = str.Replace("PHYSTDEVCHGS", "PhyStdevChgs");
			str = str.Replace(".CODE", ".Code");
			str = str.Replace("PHYSTYPE", "PhysType");
			str = str.Replace("BILLPhys", "BillPhys");
			str = str.Replace("PHYSNAMEAS", "PhysNameAs");
			str = str.Replace("PHYSNAME", "PhysName");
			str = str.Replace("PHYCNT", "PhyCnt");

			str = str.Replace("NPI", "Npi");
			str = str.Replace("PHYS", "Phys");
			str = str.Replace("PHY", "Phy");

			str = RemoveUnderscores(str);

			var today = DateTime.MinValue;


			return str;
		}

		public string RemoveUnderscores(string str)
		{
			if (str.Contains("_"))
			{
				//var foundIndexes = new List<int>();

				long t1 = DateTime.Now.Ticks;
				for (var i = str.IndexOf('_'); i > -1; i = str.IndexOf('_', i + 1))
				{

					//foundIndexes.Add(i);
					var found = str[i + 1];
					str = str.Replace("_" + found, found.ToString().ToUpper());
				}
			}
			return str;
		}
	}
}
