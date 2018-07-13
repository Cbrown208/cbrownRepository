using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CopyDataUtil.Core.Mappings;
using CopyDataUtil.Core.Models;

namespace CopyDataUtil.DataAccess
{
    public class MergeHelper
    {

        public void Merge(string connectionString, string sourceTable, string destinationTable, List<SourceDestinationColumnMapping> columnMapping, int facilityId)
        {
            try
            {
                //Logger.Info("Merge Started for Table " + destinationTable + " at {0}", DateTime.Now);

                var sqlString = GetMergeQuery(sourceTable, destinationTable, columnMapping, facilityId);

                new SqlUtil().ExecuteSql(connectionString, sqlString.ToString());

                //Logger.Info("Merge End for Table " + destinationTable + " at {0}", DateTime.Now);
            }
            catch (Exception e)
            {
                //TODO: Write errors to audit database
                //Logger.Error(e.Message);
                //Logger.Error(e.InnerException);
                throw;
            }
        }

        private StringBuilder GetMergeQuery(string sourceTable, string destinationTable, List<SourceDestinationColumnMapping> columnMapping, int facilityId)
        {
            var query = new StringBuilder();
            query.AppendFormat("MERGE INTO {0} TGT ", destinationTable);
            query.AppendFormat("USING {0} SRC ", sourceTable);
            query.Append("ON SRC.FacilityId = TGT.FacilityId and SRC.SysKey = TGT.SysKey");
            query.Append(@" WHEN MATCHED 
                        THEN 
                            UPDATE 
                            SET 
                        ");
            
            foreach (var mapping in columnMapping)
            {
                query.AppendFormat("{0} = SRC.{1}", mapping.DestinationColumn, mapping.DestinationColumn);
                query.Append(",");
            }

            query.Append("FacilityId = SRC.FacilityId");

            query.Append(@" WHEN NOT MATCHED 
                        THEN 
                            INSERT (  
                        ");

            var columns = String.Join(",", columnMapping.Select(cm => cm.DestinationColumn));

            query.Append(columns);
            query.Append(", FacilityId");

            query.Append(@" ) 
		                    VALUES ( 
                        ");

            query.Append(columns);
            query.Append(", FacilityId");

            query.Append(");");

            return query;
        }
    }
}
