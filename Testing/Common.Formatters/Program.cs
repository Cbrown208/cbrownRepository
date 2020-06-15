using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Common.Formatters
{
	public class Program
	{
		private static readonly FormatManager Manager = new FormatManager();
		private static readonly DateFormatter DateManager = new DateFormatter();

		[STAThread]
		private static void Main()
		{
			DateManager.GetCentralTime();

			//RunFormatDisplay();

			//RunSqlScrubberDisplay();

			//RunJsonFormatDisplay();

			//RunByteFormatTest();

			RunCsvTest();

			Console.ReadLine();
		}

		private static void RunByteFormatTest()
		{
			long byteSize = 511464960000;
			var result = Manager.FormatByteSize(byteSize);
			Console.WriteLine(result);
		}

		private static void RunCsvTest()
		{
			var manager = new FormatManager();
			var testList = new List<string> {"Hello", "Who"};
			var result = manager.CreateCommaSeperatedString("Test Message: ", testList);
			Console.WriteLine(result);

			testList = new List<string> { "Hello" };
			result = manager.CreateCommaSeperatedString("Test Message: ", testList);
			Console.WriteLine(result);
		}

		private static void RunJsonFormatDisplay()
		{
			var testPerson = new Person{Id = 1, FirstName = "John", LastName = "Snow",Email = "jhonnySnow@theWall.com"};

			var lowerCaseJson = JsonFormatter.ToJson(testPerson);
			DisplayResult("Lower Case Json Format", lowerCaseJson);

			var camelCaseJson = JsonFormatter.ToCamelCaseJson(testPerson);
			DisplayResult("Camel Case Json Format", camelCaseJson);

			var testPersonJsonProperties = new PersonJsonAttributes { Id = 1, FirstName = "John", LastName = "Snow", Email = "jhonnySnow@theWall.com" };
			var jsonPropertiesObject = JsonConvert.SerializeObject(testPersonJsonProperties, Formatting.Indented);
			DisplayResult("Object Json Properties", jsonPropertiesObject);

			Clipboard.SetText(jsonPropertiesObject);
		}

		public static void DisplayResult(string title, object obj)
		{
			Console.WriteLine("****** "+title+" ******");
			Console.WriteLine(obj + Environment.NewLine);
		}

		private static void RunSqlScrubberDisplay()
		{
			// String to Format
			var stringToFormat = @"CREATE PROCEDURE [dbo].[isp_ReconcileChargeRecords]
	(@FacilityId INT = 0 )
AS
BEGIN
  
IF OBJECT_ID(N'tempdb..#tmpBillData') IS NOT NULL
	  BEGIN DROP TABLE #tmpBillData END

IF OBJECT_ID(N'tempdb..#tmpChargeData') IS NOT NULL
	  BEGIN DROP TABLE #tmpChargeData END

IF OBJECT_ID(N'tempdb..#tmpDetailData') IS NOT NULL
	  BEGIN DROP TABLE #tmpDetailData END

IF OBJECT_ID(N'tempdb..#tmpValuesToUpdate') IS NOT NULL
	  BEGIN DROP TABLE #tmpValuesToUpdate END

IF  ISNULL(@FacilityId, 0) = 0
  SET @FacilityId = 0

-- If no data is in the ChargeData table set all Billmast Records to IsReconciled = 1
IF NOT EXISTS(SELECT 1 FROM ChargeData)
	BEGIN 
		PRINT 'No Charge Data Found'
		UPDATE BillMast SET IsReconciled = 1
		WHERE IsReconciled = 0
	END 
	ELSE -- Reconcile Records
	BEGIN 
		PRINT 'Charge Data Found! Reconciling Data... '
		-- Build tmp Bill Data table
		SELECT FacilityId, PatientAccountNumber, SUM(GrossCharges) AS TotalBillAmount  
		INTO #tmpBillData 
		FROM BillMast (nolock)
		WHERE IsReconciled = 0
		GROUP BY FacilityId, PatientAccountNumber
		
		CREATE TABLE #tmpDetailData (BM_FacilityId INT , BM_AccountNumber VARCHAR(80) ,TotalBillAmount MONEY ,TotalChargeAmount MONEY,Result VARCHAR(10))

		IF (@FacilityId != 0)
			BEGIN 
			-- Calculate sum for charge data WITH Facility
			SELECT FacilityId, PatientAccountNumber AS AccountNumber, SUM(ExtendedChargeAmount) AS TotalChargeAmount 
			INTO #tmpChargeData
			FROM ChargeData (NOLOCK)
			WHERE FacilityId = @FacilityId
			GROUP BY FacilityId, PatientAccountNumber

			-- Bring in charge data and Reconcile accounts
			INSERT INTO #tmpDetailData
			SELECT bm.FacilityId AS 'BM_FacilityId', 
					bm.PatientAccountNumber AS 'BM_AccountNumber', 
					bm.TotalBillAmount AS TotalBillAmount,
					cd.TotalChargeAmount,
					CASE WHEN TotalChargeAmount = TotalBillAmount THEN 'M'
						 WHEN TotalChargeAmount > TotalBillAmount THEN 'C'
						 WHEN TotalChargeAmount < TotalBillAmount THEN 'B'
						 ELSE 'NA' END AS Result
			FROM #tmpBillData bm
			LEFT JOIN #tmpChargeData cd ON bm.FacilityId = cd.FacilityId and bm.PatientAccountNumber = cd.AccountNumber
			WHERE bm.FacilityId = @FacilityId
			END
		ELSE 
			BEGIN 
				-- Calculate sum for charge data without Facility
				SELECT FacilityId, PatientAccountNumber AS AccountNumber, SUM(ExtendedChargeAmount) AS TotalChargeAmount 
				INTO #tmpChargeData
				FROM ChargeData (NOLOCK)
				GROUP BY FacilityId, PatientAccountNumber

				CREATE TABLE #tmpDetailData (BM_FacilityId INT , BM_AccountNumber VARCHAR(80) ,TotalBillAmount MONEY ,TotalChargeAmount MONEY,Result VARCHAR(10))

				-- Bring in charge data and Reconcile accounts
				INSERT INTO #tmpDetailData
				SELECT bm.FacilityId AS 'BM_FacilityId', 
						bm.PatientAccountNumber AS 'BM_AccountNumber', 
						bm.TotalBillAmount AS TotalBillAmount,
						cd.TotalChargeAmount,
						CASE WHEN TotalChargeAmount = TotalBillAmount THEN 'M'
							 WHEN TotalChargeAmount > TotalBillAmount THEN 'C'
							 WHEN TotalChargeAmount < TotalBillAmount THEN 'B'
							 ELSE 'NA' END AS Result
				FROM #tmpBillData bm
				LEFT JOIN #tmpChargeData cd ON bm.FacilityId = cd.FacilityId AND bm.PatientAccountNumber = cd.AccountNumber
			END

		select distinct FacilityId as BM_FacilityId, PatientAccountNumber as BM_AccountNumber, b.Syskey
		INTO #tmpValuesToUpdate
		FROM #tmpDetailData d
		LEFT OUTER JOIN BillMast b (nolock) ON d.[BM_FacilityId] = b.FacilityId AND d.BM_AccountNumber = b.PatientAccountNumber
		WHERE d.Result = 'M'

		-- Update Bill Mast Table with Reconciled Records 
		UPDATE BillMast SET IsReconciled = 1 
		WHERE Syskey IN (SELECT Syskey from #tmpValuesToUpdate) AND IsReconciled = 0
	END
END";

			var scrubbedString = Manager.FormatToUsqlString(stringToFormat);
			var sqlScrubbedString = Manager.FormatSqlString(stringToFormat);
			var sqlCustomScrubbedString = Manager.FormatToCustom(stringToFormat);

			Clipboard.SetText(sqlScrubbedString);
			Console.WriteLine("*** Formatted String ***" + Environment.NewLine);
			Console.WriteLine(sqlCustomScrubbedString + Environment.NewLine);
		}

		private static void RunFormatDisplay()
		{
			Console.WriteLine("Formatting Values");
			var phoneNumber = "214-777-9090";
			var phoneNumberRaw = "2147779090";
			var phoneNumberParins = "(214)777-9090";

			Console.WriteLine("Normal Phone Number: " + phoneNumber);
			Console.WriteLine("Formatted Number: " + Manager.FormatPhoneNumber(phoneNumber) + Environment.NewLine);

			Console.WriteLine("Raw Phone Number: " + phoneNumberRaw);
			Console.WriteLine("Formatted Number: " + Manager.FormatPhoneNumber(phoneNumberRaw) + Environment.NewLine);

			Console.WriteLine("Partially Formatted Phone Number: " + phoneNumberParins);
			Console.WriteLine("Formatted Number: " + Manager.FormatPhoneNumber(phoneNumberParins) + Environment.NewLine);

			var dollarDecimal = 100M;
			var thousandDecimal = 1000M;
			var millionDecimal = 1000000M;

			Console.WriteLine("Hundred Number/Currency: " + dollarDecimal);
			Console.WriteLine("Formatted Number: " + Manager.FormatNumber(dollarDecimal) + Environment.NewLine);

			Console.WriteLine("Thousand Number/Currency: " + thousandDecimal);
			Console.WriteLine("Formatted Number: " + Manager.FormatNumber(thousandDecimal) + Environment.NewLine);

			Console.WriteLine("Million Number/Currency: " + millionDecimal);
			Console.WriteLine("Formatted Number: " + Manager.FormatNumber(millionDecimal) + Environment.NewLine);

			var commaList = "0,2,4,8,9,10,100";

			Console.WriteLine("Comma Seperated List: " + commaList);
			var parsedCommaList = Manager.ParseCommaSeperatedString(commaList);
			Console.WriteLine("First Value: " + parsedCommaList.FirstOrDefault());
			Console.WriteLine("Last Value: " + parsedCommaList.LastOrDefault());

			Console.WriteLine("Capatolise the first letter of every word in a string");
			var lowerCaseString = "hello this is all in lower case";
			Console.WriteLine(lowerCaseString);
			Console.WriteLine("ToTitleCase: " + Manager.ToTitleCase(lowerCaseString));

			Console.WriteLine("Camel Case string");
			var camelCaseString = "hello this case";
			Console.WriteLine(camelCaseString);
			Console.WriteLine("ToTitleCase: " + Manager.ConvertToCamelCase(camelCaseString));
		}
	}
}
