using CommandLine;

namespace ManicTimeMonitor
{
	internal class UpdateOptions
	{
		public UpdateOptions()
		{
			Settings settings = new Settings();
			DatabaseLocation = settings.DatabaseLocation;
			UpdateUrl = settings.UpdateUrl;
		}

		[Option('d', "database", HelpText = "Path to the ManicTime database.")]
		public string DatabaseLocation { get; set; }

		[Option('u', "url", HelpText = "Remote server URL for updates.")]
		public string UpdateUrl { get; set; }
	}
}