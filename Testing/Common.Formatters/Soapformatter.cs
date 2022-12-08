using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Soap;

namespace Common.Formatters
{
	public class Soapformatter
	{
		public static string ToSoap(object Object)
		{
			if (Object == null)
			{
				throw new ArgumentException("Object can not be null");
			}

			try
			{
				SoapFormatter Serializer = new SoapFormatter();
				using (var Stream = new MemoryStream())
				{
					Serializer.Serialize(Stream, Object);
					Stream.Flush();
					return UTF8Encoding.UTF8.GetString(Stream.GetBuffer(), 0, (int)Stream.Position);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
