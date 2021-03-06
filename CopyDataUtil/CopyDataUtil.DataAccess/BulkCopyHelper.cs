﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using CopyDataUtil.Core.Mappings;
using NLog;

namespace CopyDataUtil.DataAccess
{
    public class BulkCopyHelper
    {
	    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		public void Copy(BulkCopyDetails copyDetails, int? facilityId)
        {
            try
            {
                var batchSize = 10000;
                var notifyAfterCount = 5000;
				var sqlString = GetSelectQuery(copyDetails.Config.SourceTable, copyDetails.Config.SourceDestinationColumnMapping, facilityId);
                var totalRowCount = GetTotalRowCount(copyDetails.SourceConnectionString, copyDetails.Config.SourceTable, null);

	            var formattedTotalCount = string.Format("{0:n0}", totalRowCount);

				if (totalRowCount == 0)
	            {
					Console.WriteLine("No Rows to Copy, Skipping Configuration.");
					return;
	            }
	            var totalInfoRowCount = $"Number of Rows: {formattedTotalCount}";
				Console.WriteLine(totalInfoRowCount);

	            Logger.Info(totalInfoRowCount);


				using (SqlConnection sourceConnection = new SqlConnection(copyDetails.SourceConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sqlString.ToString(), sourceConnection);
                    sourceConnection.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        using (SqlConnection destinationConnection = new SqlConnection(copyDetails.DestinationConnectionString))
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                            {
                                bulkCopy.BatchSize = batchSize;
                                bulkCopy.NotifyAfter = notifyAfterCount;

                                bulkCopy.SqlRowsCopied += BulkCopy_SqlRowsCopied;
								bulkCopy.DestinationTableName = copyDetails.Config.DestinationTable;

                                foreach (var mapping in copyDetails.Config.SourceDestinationColumnMapping)
                                {
                                    bulkCopy.ColumnMappings.Add(mapping.SourceColumn, mapping.DestinationColumn);
                                }
	                            if (facilityId != null && facilityId != 0)
	                            {
		                            bulkCopy.ColumnMappings.Add("FacilityId", "FacilityId");
	                            }
	                            destinationConnection.Open();
                                bulkCopy.WriteToServer(dataReader);
                            }
                        }
                    }
                }
			}
            catch (Exception e)
            {
				Console.WriteLine(e.Message);
				Logger.Error(e);
                throw;
            }
        }

        public void BulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            //_etlProcessorRepository.UpdateServiceCategoryETLAuditLogEntry(auditLogId, -1, "", e.RowsCopied.ToString(), null);
        }

        private int GetTotalRowCount(string sourceConnectionString, string sourceTable, string fromDate)
        {
            var query = new StringBuilder("Select Count(1)");

            query.Append(" from ").Append(sourceTable);

            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                if (string.Equals(sourceTable, "dbo.BillPayrClassSmry", StringComparison.InvariantCultureIgnoreCase))
                    query.Append(" where MDATE_TIME > ").Append("'" + fromDate + "'");
                else
                    query.Append(" where CAST(MDATE as datetime) > ").Append("'" + fromDate + "'");
            }

            try
            {
                using (SqlConnection sourceConnection = new SqlConnection(sourceConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query.ToString(), sourceConnection);
                    sourceConnection.Open();
                    var count = (Int32)cmd.ExecuteScalar();
                    Debug.WriteLine(count + " - " + query);
                    return count;
                }
            }
            catch(Exception ex)
            {
				Console.WriteLine(ex.Message);
                throw;
            }

        }

        private StringBuilder GetSelectQuery(string sourceTable, List<SourceDestinationColumnMapping> columnMapping, int? facilityId)
        {
            var query = new StringBuilder("Select ");
	        var count = columnMapping.Count;
	        var i = 0;
            foreach (var mapping in columnMapping)
            {
                query.Append(mapping.SourceColumn);
				if (i < count - 1)
				{
					query.Append(",");
				}
				i = i + 1;
			}

	        if (facilityId != null && facilityId != 0)
	        {
				query.Append(","+facilityId).Append(" as FacilityId");
	        }
	        query.Append(" from ").Append(sourceTable);
            return query;
        }
    }
}
