using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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

		public string XmlSerialize<T>(T sourceValue) where T : class
		{
			// If source is empty, throw Exception
			if (sourceValue == null)
				throw new NullReferenceException("sourceValue is required");

			// Define encoding
			var encoding = Encoding.ASCII;

			// Declare the resultant variable
			string targetValue;

			// Using MemoryStream for In-Process conversion
			using (var memoryStream = new MemoryStream())
			{
				// Declare Stream with required Encoding
				using (var streamWriter = new StreamWriter(memoryStream, encoding))
				{
					// Declare Xml Serializer with source value Type (serializing type)
					var xmlSerializer = new XmlSerializer(sourceValue.GetType());

					// Perform Serialization of the source value and write to Stream
					xmlSerializer.Serialize(streamWriter, sourceValue);

					// Grab the serialized string
					targetValue = encoding.GetString(memoryStream.ToArray());
				}
			}

			// Return the resultant value;
			return targetValue;
		}

		public T XmlDeserialize<T>(string sourceValue) where T : class
		{
			// If source is empty, throw Exception
			if (string.IsNullOrWhiteSpace(sourceValue))
				throw new NullReferenceException("sourceValue is required");

			// Define encoding
			var encoding = Encoding.ASCII;

			// Declare the resultant variable
			T targetValue;

			// Declare Xml Serializer with target value Type (serialized type)
			var xmlSerializer = new XmlSerializer(typeof(T));

			// Get the source value to bytes with required Encoding
			byte[] sourceBytes = encoding.GetBytes(sourceValue);

			// Using MemoryStream for In-Process conversion
			using (var memoryStream = new MemoryStream(sourceBytes))
			{
				// Read stream into XML-based reader
				using (var xmlTextReader = new XmlTextReader(memoryStream))
				{
					// Perform Deserialization from the stream and Convert to target type
					targetValue = xmlSerializer.Deserialize(xmlTextReader) as T;
				}
			}

			// Return the resultant value;
			return targetValue;
		}
	}
}
