using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Tools.TimeCardAutomation
{
	public class TimeCardManager
	{
		public void FillOutTimeCard()
		{ 
			var pbiList = "794985,771368,790976,797301,797302,797298,790490,791233,792543";

			IWebDriver driver = new InternetExplorerDriver();
			driver.Url = "http://timex.nthrive.com/BP/Project/Project%20Center%20Pages/Time.aspx";

			driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ2");

			//driver.FindElement(By.LinkText("[Duplicate]")).Click();

			driver.FindElement(By.PartialLinkText("1000")).Click();

			driver.SwitchTo().ParentFrame();
			Thread.Sleep(1000);
			driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ3");
			driver.SwitchTo().Frame("dynamicpart");

			//Maintenance
			var cellId = "day2_hr1_5";
			ClearCellContents(driver, cellId);
			driver.FindElement(By.Id(cellId)).SendKeys("1.00");

			//Dev Work
			cellId = "day2_hr1_4";
			ClearCellContents(driver, cellId);
			driver.FindElement(By.Id(cellId)).SendKeys("7.00");

			cellId = "day3_hr1_4";
			ClearCellContents(driver, cellId);
			driver.FindElement(By.Id(cellId)).SendKeys("8.00");

			cellId = "day4_hr1_4";
			ClearCellContents(driver, cellId);
			driver.FindElement(By.Id(cellId)).SendKeys("8.00");

			cellId = "day5_hr1_4";
			ClearCellContents(driver, cellId);
			driver.FindElement(By.Id(cellId)).SendKeys("8.00");

			cellId = "day6_hr1_4";
			ClearCellContents(driver, cellId);
			driver.FindElement(By.Id(cellId)).SendKeys("8.00");


			driver.FindElement(By.Id("theSubmit")).Click();

			var notes = "(//A[starts-with(@title, 'Edit notes for this line')])[2]";
			driver.FindElement(By.XPath(notes)).Click();


			driver.FindElement(By.Id("txtNote")).Clear();
			driver.FindElement(By.Id("txtNote")).SendKeys(GetDevNotesList(pbiList));
			driver.FindElement(By.Id("cmdSave")).Click();
			
			//driver.Close();
			//driver.Quit();
		}

		private void ClearCellContents(IWebDriver driver, string cellName)
		{
			for (int i = 0; i < 4; i++)
			{
				driver.FindElement(By.Id(cellName)).SendKeys(Keys.Backspace);
			}
		}

		private string GetDevNotesList(string pbiList)
		{
			var devNotes = @"PBI's: " + pbiList + @"
Bug Triage 
Planning 
Refinement 
Team Code Review / Dev Meeting
Scrum of Scrums meeting";
			return devNotes;
		}
	}
}
