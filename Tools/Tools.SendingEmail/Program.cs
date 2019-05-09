namespace Tools.SendingEmail
{
	public class Program
	{
		public static readonly SendingEmailManager Manager = new SendingEmailManager();
		static void Main()
		{
			Manager.SendBackupLogEmail();
		}
	}
}
