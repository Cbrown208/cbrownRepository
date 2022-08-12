namespace Common.Formatters
{
	public class ByteFormatter
	{
		/// <summary>
		/// Format raw bytes value (long) into a readable string.
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public string FormatBytes(long bytes)
		{
			string[] suffix = { "B", "KB", "MB", "GB", "TB" };
			int i;
			double dblSByte = bytes;
			for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
			{
				dblSByte = bytes / 1024.0;
			}

			return $"{dblSByte:0.##} {suffix[i]}";
		}

		/// <summary>
		/// Format raw bytes value (double) into a readable string.
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public string FormatBytes(double bytes)
		{
			string[] suffix = { "B", "KB", "MB", "GB", "TB" };
			int i;
			double dblSByte = bytes;
			for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
			{
				dblSByte = bytes / 1024.0;
			}

			return $"{dblSByte:0.##} {suffix[i]}";
		}

		/// <summary>
		/// Format raw Kilobytes value (double) into a readable string.
		/// </summary>
		/// <param name="kiloBytes"></param>
		/// <returns></returns>
		public string FormatKiloBytes(double kiloBytes)
		{
			string[] suffix = { "KB", "MB", "GB", "TB" };
			int i;
			double dblSByte = kiloBytes;
			for (i = 0; i < suffix.Length && kiloBytes >= 1024; i++, kiloBytes /= 1024)
			{
				dblSByte = kiloBytes / 1024.0;
			}

			return $"{dblSByte:0.##} {suffix[i]}";
		}

		/// <summary>
		/// Format raw Kilobytes value (double) into a readable string.
		/// </summary>
		/// <param name="kiloBytes"></param>
		/// <returns></returns>
		public string FormatMegaBytes(double megaBytes)
		{
			string[] suffix = { "MB", "GB", "TB" };
			int i;
			double dblSByte = megaBytes;
			for (i = 0; i < suffix.Length && megaBytes >= 1024; i++, megaBytes /= 1024)
			{
				dblSByte = megaBytes / 1024.0;
			}

			return $"{dblSByte:0.##}{suffix[i]}";
		}
	}
}
