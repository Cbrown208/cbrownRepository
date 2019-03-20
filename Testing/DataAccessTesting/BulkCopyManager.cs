using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using Common.Formatters.Converters;
using DataAccessTesting.Models;

namespace DataAccessTesting
{
	public class BulkCopyManager
	{
		private readonly DataTableConverter _dtConverter = new DataTableConverter();

		public bool StartCopy()
		{
			var sw = new Stopwatch();
			try
			{
				var testData = GetTestData();
				var config = new BulkCopyDetails()
				{
					BatchSize = 100,
					DestinationConnectionString = "Data Source=localhost;Initial catalog=TestingDb;Integrated Security=true;",
					TableName = "CMSPricing"
				};
				sw.Start();
				BulkInsert(config, testData);
				sw.Stop();

				Console.WriteLine("Batch Copy Completed in: "+ sw.Elapsed);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		public void BulkInsert<T>(BulkCopyDetails config, List<T> insertData)
		{
			var dataTable = _dtConverter.ConvertToDatatable(insertData);

			using (var connection = new SqlConnection(config.DestinationConnectionString))
			{
				using (var bulkCopy = new SqlBulkCopy(connection))
				{
					bulkCopy.BatchSize = config.BatchSize;
					bulkCopy.NotifyAfter = 1000;

					bulkCopy.SqlRowsCopied += BulkCopyNotification;
					bulkCopy.DestinationTableName = config.TableName;

					connection.Open();
					bulkCopy.WriteToServer(dataTable);
				}
			}
			dataTable.Clear();
		}

		public List<CmsPricing> GetTestData()
		{
			var testData = new List<CmsPricing>();
			var tempAdd = new CmsPricing
			{
				Description = "chBrownTest",
				FacilityId = new Guid(),
				LastTouchedDate = DateTime.Now,
				Notes = "chBrownTest",
				Price = 100.00M
			};

			for (var i = 1; i < 10001; i++)
			{
				tempAdd.Code = i.ToString();
				testData.Add(tempAdd);
			}

			return testData;
		}

		public void BulkCopyNotification(object sender, SqlRowsCopiedEventArgs e)
		{
			Console.WriteLine("Rows Copied: "+e.RowsCopied);
			//_etlProcessorRepository.UpdateServiceCategoryETLAuditLogEntry(auditLogId, -1, "", e.RowsCopied.ToString(), null);
		}
	}
}
