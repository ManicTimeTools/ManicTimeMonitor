using CommandLine;
using CommandLine.Text;

namespace ManicTimeMonitor
{
	internal class Options
	{
		[VerbOption("update", HelpText = "Submit entries update to the remote server.")]
		public UpdateOptions UpdateVerb { get; set; }

		[HelpVerbOption]
		public string GetUsage(string verb)
		{
			return HelpText.AutoBuild(this, verb);
		}
	}
}