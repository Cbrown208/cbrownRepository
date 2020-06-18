using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace WebDriverUiTesting
{
	public class UiTestCases
	{
		public void RunTestCases()
		{
			TimeCardTest();
			//IeTestCase();
			//ChromeTestCase();
		}

		public void IeTestCase()
		{
			IWebDriver driver = new InternetExplorerDriver();
			driver.Url = "https://google.com";

			// Open a new tab

			driver.FindElement(By.Name("q")).SendKeys("who am i");
			driver.FindElement(By.Name("q")).Submit();

			//driver.FindElement(By.LinkText("About")).Click();
		}

		public void TimeCardTest()
		{

			var pbiList = "791233,761553,771368";

			IWebDriver driver = new InternetExplorerDriver();
			driver.Url = "http://timex.nthrive.com/BP/Project/Project%20Center%20Pages/Time.aspx";

			driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ2");

			//driver.FindElement(By.LinkText("[Duplicate]")).Click();


			driver.FindElement(By.PartialLinkText("1000")).Click();

			driver.SwitchTo().ParentFrame();
			Thread.Sleep(1000);
			driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ3");
			var pageSource = driver.PageSource;
			driver.SwitchTo().Frame("dynamicpart");

			pageSource = driver.PageSource; 

			//Monday Maintanance
			ClearCellContents(driver, "day2_hr1_5");
			driver.FindElement(By.Id("day2_hr1_5")).SendKeys("1.00");


			//Monday Dev Work
			var cellId = "day2_hr1_4";
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

			//driver.FindElement(By.PartialLinkText("Edit notes for this line"));
			//driver.FindElement(By.Name("Edit notes for this line"));
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
			var devNotes = @"PBI's: "+ pbiList + @"
Bug Triage 
Planning 
Refinement 
Team Code Review / Dev Meeting
Scrum of Scrums meeting";
			return devNotes;
		}

		public void ChromeTestCase()
		{
			IWebDriver driver = new ChromeDriver();
			driver.Url = "https://google.com";
		}
	}
}
