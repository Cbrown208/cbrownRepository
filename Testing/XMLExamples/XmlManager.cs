using System;

namespace XMLExamples
{
	public class XmlManager
	{
		public string GetXmlValue(string content, string startString, string endString)
		{
			if (content.Contains(startString) && content.Contains(endString))
			{
				var start = content.IndexOf(startString, 0, StringComparison.Ordinal) + startString.Length;
				var end = content.IndexOf(endString, start, StringComparison.Ordinal);
				return content.Substring(start, end - start);
			}
			return string.Empty;
		}
	}
}
