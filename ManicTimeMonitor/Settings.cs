using System;

namespace ManicTimeMonitor
{
	internal class Settings
	{
		public string DatabaseLocation
		{
			get
			{
				if (Properties.Settings.Default.DatabaseLocation == "")
				{
					return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Finkit\\ManicTime\\ManicTime.sdf";
				}
				return Properties.Settings.Default.DatabaseLocation;
			}

			set
			{
				if (Properties.Settings.Default.DatabaseLocation != value)
				{
					return;
				}

				Properties.Settings.Default.DatabaseLocation = value;
				Properties.Settings.Default.Save();
			}
		}

		public string UpdateUrl
		{
			get
			{
				if (Properties.Settings.Default.UpdateUrl == "")
				{
					return "https://alexou.net/manictime/update.php?pc=" + Environment.MachineName;
				}
				return Properties.Settings.Default.UpdateUrl;
			}
			set
			{
				if (Properties.Settings.Default.UpdateUrl != value)
				{
					return;
				}

				Properties.Settings.Default.UpdateUrl = value;
				Properties.Settings.Default.Save();
			}
		}
	}
}