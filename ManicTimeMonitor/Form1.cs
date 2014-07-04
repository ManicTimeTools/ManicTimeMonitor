using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ManicTimeMonitor
{
	public partial class Form1 : Form
	{
		private Thread worker;
		public String UpdateUrl;
		public String DatabaseLocation;

		public Form1()
		{
			InitializeComponent();
		}

		void Updater_ProgressMessage(string message)
		{
			UpdateBtn.Invoke(new Action(() => LogTxt.AppendText("[" + DateTime.Now + "] " + message + "\r\n")));
		}

		protected void LoadDatabaseLocation()
		{
			if (Properties.Settings.Default.DatabaseLocation == "")
			{
				DatabaseLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Finkit\\ManicTime\\ManicTime.sdf";
			}
			else
			{
				DatabaseLocation = Properties.Settings.Default.DatabaseLocation;
			}
		}

		protected void UpdateDatabaseLocation(String databaseLocation)
		{
			if (DatabaseLocation == databaseLocation)
			{
				return;
			}

			DatabaseLocation = databaseLocation;
			Properties.Settings.Default.DatabaseLocation = DatabaseLocation;
			Properties.Settings.Default.Save();
		}

		protected void LoadUpdateUrl()
		{
			if (Properties.Settings.Default.UpdateUrl == "")
			{
				UpdateUrl = "https://alexou.net/manictime/?pc=" + Environment.MachineName;
			}
			else
			{
				UpdateUrl = Properties.Settings.Default.UpdateUrl;
			}

		}

		protected void UpdateUpdateUrl(String updateUrl)
		{
			if (UpdateUrl == updateUrl)
			{
				return;
			}

			UpdateUrl = updateUrl;
			Properties.Settings.Default.UpdateUrl = UpdateUrl;
			Properties.Settings.Default.Save();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			LoadUpdateUrl();
			LoadDatabaseLocation();
			UpdateUrlTxt.Text = UpdateUrl;
			DatabaseLocationTxt.Text = DatabaseLocation;
		}

		private void UpdateBtn_Click(object sender, EventArgs e)
		{
			if (worker == null)
			{
				UpdateBtn.Text = "Stop";
				LogTxt.Text = "";
				worker = new Thread(() =>
				{
					try
					{
						Updater updater = new Updater(UpdateUrl, DatabaseLocation);
						updater.ProgressMessage += new ManicTimeMonitor.Updater.NotificationEventHandler(Updater_ProgressMessage);
						updater.Update();
					}
					catch (ThreadAbortException)
					{
						// do nothing
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.StackTrace, ex.Message);
					}
					UpdateBtn.Invoke(new Action(() => UpdateBtn.Text = "Update now"));
					worker = null;
				});
				worker.Start();
			}
			else
			{
				worker.Abort();
				worker = null;
				UpdateBtn.Text = "Update now";
				LogTxt.AppendText("Stopped");
			}
		}

		private void QuitBtn_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void DatabaseLocationTxt_TextChanged(object sender, EventArgs e)
		{
			UpdateDatabaseLocation(DatabaseLocationTxt.Text);
		}

		private void UpdateUrlTxt_Leave(object sender, EventArgs e)
		{
			UpdateUpdateUrl(UpdateUrlTxt.Text);
		}

		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			DatabaseLocationTxt.Text = openFileDialog.FileName;
		}

		private void DatabaseLocationBtn_Click(object sender, EventArgs e)
		{
			openFileDialog.FileName = Path.GetFileName(DatabaseLocation);
			openFileDialog.InitialDirectory = Path.GetDirectoryName(DatabaseLocation);
			openFileDialog.ShowDialog();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (worker != null)
			{
				worker.Abort();
			}
		}
	}
}