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
			var pbiList = "844441,842959,842956,843223,843361,827135,845406,845404,844657,842467";
			var addDevNotes = false;

			IWebDriver driver = new InternetExplorerDriver();
			driver.Url = "http://timex.nthrive.com/BP/Project/Project%20Center%20Pages/Time.aspx";
			try
			{
				Thread.Sleep(1000);
				var tsList = driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ2");
				Thread.Sleep(1000);
				var inProcessTable = tsList.FindElement(By.Id("tblTimecard0"));
				inProcessTable.FindElement(By.PartialLinkText("1000")).Click();
				inProcessTable.FindElement(By.PartialLinkText("1000")).Click();
				//driver.FindElement(By.LinkText("[Duplicate]")).Click();

				driver.SwitchTo().ParentFrame();
				Thread.Sleep(2000);
				driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ3").SwitchTo().Frame("dynamicpart");
				Thread.Sleep(2000);

				CheckForError(driver);

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

				if (addDevNotes)
				{
					var notes = "(//A[starts-with(@title, 'Edit notes for this line')])[2]";
					driver.FindElement(By.XPath(notes)).Click();
					Thread.Sleep(1000);

					driver.FindElement(By.Id("txtNote")).Clear();
					driver.FindElement(By.Id("txtNote")).SendKeys(GetDevNotesList(pbiList));
					driver.FindElement(By.Id("cmdSave")).Click();
				}
			}
			catch (Exception)
			{
				driver.Close();
				throw;
			}

			driver.Close();
			driver.Quit();
		}

		private void CheckForError(IWebDriver driver)
		{
			try
			{
				var temp = driver.FindElement(By.ClassName("clsErrorText"));
				RefreshPage(driver);
				var tsList = driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ2");
				Thread.Sleep(1000);
				var inProcessTable = tsList.FindElement(By.Id("tblTimecard0"));
				inProcessTable.FindElement(By.PartialLinkText("1000")).Click();

				driver.SwitchTo().ParentFrame();
				Thread.Sleep(2000);
				driver.SwitchTo().Frame("MSOPageViewerWebPart_WebPartWPQ3").SwitchTo().Frame("dynamicpart");
			}
			catch (Exception)
			{
				// ignored
			}
		}

		private void RefreshPage(IWebDriver driver)
		{
			driver.Navigate().Refresh();
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
