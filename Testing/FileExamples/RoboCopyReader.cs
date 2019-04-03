using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FileExamples
{
	public class RoboCopyReader
	{
		public void ReadRoboCopyLog()
		{
			using (StreamReader r = new StreamReader(@"C:\myscripts\temp\LogFile.log"))
			{
				// 2.
				// Read each line until EOF.
				string line;
				while ((line = r.ReadLine()) != null)
				{
					// 3.
					// Do stuff with line.
					if (line.Contains("Files"))
					{
						if (!line.Contains("Files : *.*"))
						{
							var content = line;

							string[] splitContent = Regex.Replace(content, @"\s+", " ")
								.Split(' ');

							var fileString = "";
							foreach (string s in splitContent)
							{
								fileString = fileString + s + " ";
							}
							Console.WriteLine(fileString);

							//Console.WriteLine(splitContent[3]);
						}
					}

					if (line.Contains("Dirs"))
					{
						if (!line.Contains("Dirs : *.*"))
						{
							var content = line;

							string[] splitContent = Regex.Replace(content, @"\s+", " ")
								.Split(' ');
							var dirsString = "";
							foreach (string s in splitContent)
							{
								dirsString = dirsString + s + " ";
							}
							Console.WriteLine(dirsString);

							//Console.WriteLine(splitContent[3]);
						}
					}

					if (line.Contains("Bytes"))
					{
						if (!line.Contains("Bytes : *.*"))
						{
							var content = line;

							string[] splitContent = Regex.Replace(content, @"\s+", " ")
								.Split(' ');
							var dirsString = "";
							foreach (string s in splitContent)
							{
								dirsString = dirsString + s + " ";
							}
							Console.WriteLine(dirsString);

							//Console.WriteLine(splitContent[3]);
						}
					}
				}
			}
		}
	}
}
