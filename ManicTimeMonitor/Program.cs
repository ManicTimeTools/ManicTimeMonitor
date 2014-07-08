using System;
using System.Windows.Forms;
using CommandLine;

namespace ManicTimeMonitor
{
	internal static class Program
	{
		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				WinForm();
				return;
			}

			Console(args);
		}

		private static void WinForm()
		{
			ConsoleVisibility.Hide();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		private static void Console(string[] args)
		{
			string invokedVerb = null;
			object invokedVerbInstance = null;
			var options = new Options();
			if (!Parser.Default.ParseArguments(args, options,
				(verb, subOptions) =>
				{
					// if parsing succeeds the verb name and correct instance
					// will be passed to onVerbCommand delegate (string,object)
					invokedVerb = verb;
					invokedVerbInstance = subOptions;
				}))
			{
				Environment.Exit(Parser.DefaultExitCodeFail);
			}

			if (invokedVerb == "update")
			{
				UpdateOptions updateOptions = (UpdateOptions) invokedVerbInstance;

				System.Console.WriteLine("Updating remote data using the following settings:");
				System.Console.WriteLine("Database Location: " + updateOptions.DatabaseLocation);
				System.Console.WriteLine("Update URL: " + updateOptions.UpdateUrl);

				Updater updater = new Updater(updateOptions.UpdateUrl, updateOptions.DatabaseLocation);
				updater.ProgressMessage += message => System.Console.WriteLine("[" + DateTime.Now + "] " + message);
				updater.Update();
			}
		}
	}
}