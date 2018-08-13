using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using CopyDataUtil.Core.Mappings;

namespace CopyDataUtil.DataAccess
{
    public class BulkCopyHelper
    {
        public void Copy(BulkCopyDetails copyDetails)
        {
            try
            {
                var batchSize = 10000;
                var notifyAfterCount = 5000;
				var sqlString = GetSelectQuery(copyDetails.Config.SourceTable, copyDetails.Config.SourceDestinationColumnMapping);
                var totalRowCount = GetTotalRowCount(copyDetails.SourceConnectionString, copyDetails.Config.SourceTable, null);

	            var formattedTotalCount = string.Format("{0:n0}", totalRowCount);

				if (totalRowCount == 0)
	            {
					Console.WriteLine("No Rows to Copy, Skipping Configuration.");
					return;
	            }

				Console.WriteLine($"Number of Rows: {formattedTotalCount}");

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
                                //bulkCopy.ColumnMappings.Add("FacilityId", "FacilityId");
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
                throw;
            }
        }

        public void BulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            //_etlProcessorRepository.UpdateServiceCategoryETLAuditLogEntry(auditLogId, -1, "", e.RowsCopied.ToString(), null);
        }

        private int GetTotalRowCount(string sourceConnectionString, string sourceTable, string fromDate)
        {
            var query = new StringBuilder("Select Count(*)");

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

        private StringBuilder GetSelectQuery(string sourceTable, List<SourceDestinationColumnMapping> columnMapping)
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

            //query.Append(",1412").Append(" as FacilityId");
            query.Append(" from ").Append(sourceTable);
            return query;
        }
    }
}
