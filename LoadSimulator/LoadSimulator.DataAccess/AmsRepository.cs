using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PAS.Worklist.Contract.Models;
using PAS.WorkList.Contract.Implementation;

namespace LoadSimulator.DataAccess
{
	public class AmsRepository
	{
		private readonly IDbConnection _db;

		public AmsRepository()
		{
			_db = new SqlConnection(@"Server=RCM41VQPASDB01.medassets.com\PASDEVIV;Database=AMS_QA_Current_STAH;Trusted_Connection=true;");
		}
		public dynamic GetAccountByAccountNumber(string accountNumber)
		{
			using (IDbConnection dbConnection = _db)
			{

				var sQuery = "SELECT TOP(100) FROM Account WHERE Id = @Id";
				dbConnection.Open();
				var result = dbConnection.Query(sQuery, new { AccountNumber = accountNumber }).FirstOrDefault();

				var resultName = "";
				if (result != null)
				{
					resultName = result.Name;
				}
				return resultName;
			}
		}

		public List<WorklistSyncOne> GetAccountFacilityList()
		{
			using (IDbConnection dbConnection = _db)
			{
				var sQuery = "SELECT TOP(100) * FROM Account with (nolock) ORDER BY CreatedOn desc";
				dbConnection.Open();
				var result = dbConnection.Query(sQuery);
				var accountList = new List<WorklistSyncOne>();
				if (result != null)
				{
					foreach (var account in result)
					{
						accountList.Add(new WorklistSyncOne { ClientId = account.ClientId, CreatedBy = "Local Test", Account = new Account { AccountNumber = account.AccountNumber, FacilityId = account.FacilityId, AccountSequence = account.AccountSequence } });
					}
				}
				return accountList;
			}
		}
	}
}
