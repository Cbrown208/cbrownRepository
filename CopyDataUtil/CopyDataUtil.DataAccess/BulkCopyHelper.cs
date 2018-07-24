using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using CopyDataUtil.Core.Mappings;
using CopyDataUtil.Core.Models;

namespace CopyDataUtil.DataAccess
{
    public class BulkCopyHelper
    {
        //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        //private readonly IETLProcessorRepository _etlProcessorRepository;
        int auditLogId;

        public BulkCopyHelper()
        {
            //_etlProcessorRepository = etlProcessorRepository;
            this.auditLogId = auditLogId;
        }

        public void Copy(BulkCopyDetails copyDetails)
        {
            try
            {
                var batchSize = 10000;
                var notifyAfterCount = 500;
				var sqlString = GetSelectQuery(copyDetails.Config.SourceTable, copyDetails.Config.SourceDestinationColumnMapping);
                var totalRowCount = GetTotalRowCount(copyDetails.SourceConnectionString, copyDetails.Config.SourceTable, null);
				Console.WriteLine($"Attempting to Copy {totalRowCount:n} rows.");

                //Logger.Info("Bulk Insert Started for Table " + destinationTable + " at {0}", DateTime.Now);

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

                                bulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(BulkCopy_SqlRowsCopied);
                                bulkCopy.DestinationTableName = copyDetails.Config.DestinationTable;

                                foreach (var mapping in copyDetails.Config.SourceDestinationColumnMapping)
                                {
                                    bulkCopy.ColumnMappings.Add(mapping.SourceColumn, mapping.DestinationColumn);
                                }
                                //bulkCopy.ColumnMappings.Add("FacilityId", "FacilityId");

                                destinationConnection.Open();
                                bulkCopy.WriteToServer(dataReader);

                                if (totalRowCount > 0)
                                {
                                    //_etlProcessorRepository.UpdateServiceCategoryETLAuditLogEntry(auditLogId, -1, "", totalRowCount.ToString(), null);
                                    //Debug.WriteLine("last updated row count - " + totalRowCount);
                                }

                            }
                        }
                    }
                }
	            Console.WriteLine($"Number of Rows Successfully Copied: {totalRowCount:n}");

				//Logger.Info("Bulk Insert End for Table " + destinationTable + " at {0}", DateTime.Now);
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
                throw ex;
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
	            //query.Append(",");
				if (i < count - 1)
				{
					query.Append(",");
				}
				i = i + 1;
			}

           // query.Append(facilityId).Append(" as FacilityId");
            query.Append(" from ").Append(sourceTable);
            return query;
        }
    }
}
