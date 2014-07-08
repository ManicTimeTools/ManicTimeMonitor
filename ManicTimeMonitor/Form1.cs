using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using Action = System.Action;

namespace ManicTimeMonitor
{
	public partial class Form1 : Form
	{
		private Settings _settings = new Settings();
		private Thread _worker;

		public Form1()
		{
			InitializeComponent();
		}

		private void Updater_ProgressMessage(string message)
		{
			UpdateBtn.Invoke(new Action(() => LogTxt.AppendText("[" + DateTime.Now + "] " + message + "\r\n")));
		}

		protected void UpdateDatabaseLocation(String databaseLocation)
		{
			_settings.DatabaseLocation = databaseLocation;
		}

		protected void UpdateUpdateUrl(String updateUrl)
		{
			_settings.UpdateUrl = updateUrl;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			UpdateUrlTxt.Text = _settings.UpdateUrl;
			DatabaseLocationTxt.Text = _settings.DatabaseLocation;
		}

		private void UpdateBtn_Click(object sender, EventArgs e)
		{
			if (_worker == null)
			{
				UpdateBtn.Text = "Stop";
				LogTxt.Text = "";
				_worker = new Thread(() =>
				{
					try
					{
						Updater updater = new Updater(_settings.UpdateUrl, _settings.DatabaseLocation);
						updater.ProgressMessage += Updater_ProgressMessage;
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
					_worker = null;
				});
				_worker.Start();
			}
			else
			{
				_worker.Abort();
				_worker = null;
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
			openFileDialog.FileName = Path.GetFileName(_settings.DatabaseLocation);
			openFileDialog.InitialDirectory = Path.GetDirectoryName(_settings.DatabaseLocation);
			openFileDialog.ShowDialog();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_worker != null)
			{
				_worker.Abort();
			}
		}

		private void ConfigureTaskScheduleBtn_Click(object sender, EventArgs e)
		{
			ScheduledTask.Register();
		}
	}
}