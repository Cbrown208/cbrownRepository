using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Tools.TimeCardAutomation
{
	public class TimeCardManager
	{
		public void FillOutTimeCard()
		{
			var pbiList = "790977,790979,806941,815903,816067";

			IWebDriver driver = new InternetExplorerDriver();
			driver.Url = "http://timex.nthrive.com/BP/Project/Project%20Center%20Pages/Time.aspx";

			try
			{

				driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ2");

				//driver.FindElement(By.LinkText("[Duplicate]")).Click();

				driver.FindElement(By.PartialLinkText("1000")).Click();

				driver.SwitchTo().ParentFrame();
				Thread.Sleep(2000);
				driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ3").SwitchTo().Frame("dynamicpart");

				//Maintenance
				var cellId = "day2_hr1_5";
				ClearCellContents(driver, cellId);
				driver.FindElement(By.Id(cellId)).SendKeys("1.00");

				//Dev Work - Mon-Fri (2-6)
				for (var i = 2; i < 7; i++)
				{
					cellId = $"day{i}_hr1_4";
					var timeValue = "8.00";
					if (i == 2)
					{
						timeValue = "7.00";
					}

					ClearCellContents(driver, cellId);
					driver.FindElement(By.Id(cellId)).SendKeys(timeValue);
				}

				driver.FindElement(By.Id("theSubmit")).Click();

				var notes = "(//A[starts-with(@title, 'Edit notes for this line')])[2]";
				driver.FindElement(By.XPath(notes)).Click();
				Thread.Sleep(1000);

				driver.FindElement(By.Id("txtNote")).Clear();
				driver.FindElement(By.Id("txtNote")).SendKeys(GetDevNotesList(pbiList));
				driver.FindElement(By.Id("cmdSave")).Click();
			}
			catch (Exception)
			{
				driver.Close();
				throw;
			}

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
