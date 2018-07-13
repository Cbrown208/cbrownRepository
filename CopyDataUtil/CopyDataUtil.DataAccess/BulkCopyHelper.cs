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

        public void Copy(string sourceConnectionString, string destinationConnectionString, string sourceTable, string destinationTable, List<SourceDestinationColumnMapping> columnMapping, int facilityId, string fromDate)
        {
            try
            {
                var batchSize = 10000;
                var notifyAfterCount = 500;

                var sqlString = GetSelectQuery(sourceTable, columnMapping, fromDate, facilityId);
                var totalRowCount = GetTotalRowCount(sourceConnectionString, sourceTable, fromDate, facilityId);

                //Logger.Info("Bulk Insert Started for Table " + destinationTable + " at {0}", DateTime.Now);

                using (SqlConnection sourceConnection = new SqlConnection(sourceConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sqlString.ToString(), sourceConnection);
                    sourceConnection.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        using (SqlConnection destinationConnection = new SqlConnection(destinationConnectionString))
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                            {
                                bulkCopy.BatchSize = batchSize;
                                bulkCopy.NotifyAfter = notifyAfterCount;

                                bulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(BulkCopy_SqlRowsCopied);
                                bulkCopy.DestinationTableName = destinationTable;

                                foreach (var mapping in columnMapping)
                                {
                                    bulkCopy.ColumnMappings.Add(mapping.SourceColumn, mapping.DestinationColumn);
                                }
                                bulkCopy.ColumnMappings.Add("FacilityId", "FacilityId");

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

                //Logger.Info("Bulk Insert End for Table " + destinationTable + " at {0}", DateTime.Now);
            }
            catch (Exception e)
            {
				Console.WriteLine(e.Message);
                //Logger.Error(e.Message);
                //Logger.Error(e.InnerException);
                throw;
            }
        }

        public void BulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            //_etlProcessorRepository.UpdateServiceCategoryETLAuditLogEntry(auditLogId, -1, "", e.RowsCopied.ToString(), null);
        }

        private int GetTotalRowCount(string sourceConnectionString, string sourceTable, string fromDate, int facilityId)
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

        private StringBuilder GetSelectQuery(string sourceTable, List<SourceDestinationColumnMapping> columnMapping, string fromDate, int facilityId)
        {
            var query = new StringBuilder("Select ");

            foreach (var mapping in columnMapping)
            {
                query.Append(mapping.SourceColumn);
                query.Append(",");
            }

            query.Append(facilityId).Append(" as FacilityId");
            query.Append(" from ").Append(sourceTable);

            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                if (string.Equals(sourceTable, "dbo.BillPayrClassSmry", StringComparison.InvariantCultureIgnoreCase))
                    query.Append(" where MDATE_TIME > ").Append("'" + fromDate + "'");
                else
                    query.Append(" where CAST(MDATE as datetime) > ").Append("'" + fromDate + "'");
            }

            return query;
        }
    }
}
