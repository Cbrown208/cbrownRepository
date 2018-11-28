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
			if (sourceValue == null)
				throw new NullReferenceException("sourceValue is required");

			var encoding = Encoding.ASCII;
			string targetValue;

			using (var memoryStream = new MemoryStream())
			{
				using (var streamWriter = new StreamWriter(memoryStream, encoding))
				{
					var xmlSerializer = new XmlSerializer(sourceValue.GetType());
					xmlSerializer.Serialize(streamWriter, sourceValue);
					targetValue = encoding.GetString(memoryStream.ToArray());
				}
			}
			return targetValue;
		}

		public string SerializeToString<T>(T value) where T : class
		{
			string message;
			var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
			var serializer = new XmlSerializer(value.GetType());
			var settings = new XmlWriterSettings();
			// Default Indent XML ********************************************************************************************************
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;

			using (var stream = new StringWriter())
			using (var writer = XmlWriter.Create(stream, settings))
			{
				serializer.Serialize(writer, value, emptyNamepsaces);
				message = stream.ToString();
			}
			//message = message.Replace("\r\n", string.Empty);
			message = message.Replace("<AdtMessage>",
				"<?xml version=\"1.0\" encoding=\"utf-8\"?><ADTMessage xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
			message = message.Replace("</AdtMessage>", "</ADTMessage>");
			message = message.Replace("<Ssn>", "<SSN>");
			message = message.Replace("</Ssn>", "</SSN>");
			message = message.Replace("<Mrn>", "<MRN>");
			message = message.Replace("</Mrn>", "</MRN>");
			return message;
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
