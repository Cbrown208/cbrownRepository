using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var facilityId = 1;
            var patientId = 1;
            var sqlpatient =
    string.Format(
        "INSERT INTO dbo.Patient(FacilityId,PatientId,FamilyName,CreatedBy,CreatedOn,Isadhoc) VALUES (CONVERT(uniqueidentifier,'" +
        facilityId + "'), CONVERT(uniqueidentifier,'" + patientId + "'),'brown','cbrown',GETDATE(),1)");
            try
            {
         //       _connection.Execute(sqlpatient);
            }
            catch (Exception ex)
            {
                throw;
            }

            var connection = new SqlConnection("Data Source=.;Initial Catalog=AMS;Integrated Security=True");
            SqlCommand command =
                new SqlCommand(
                    "SELECT AccountSequence FROM dbo.Account with (nolock) WHERE PatientId = '123';",
                    connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var value = reader.GetInt32(0);
   //                 Accountseq = value;
                }
            }
            reader.Close();
            connection.Close();
     //       var returnval = Accountseq;
       //     return returnval;
        }
    }
}
