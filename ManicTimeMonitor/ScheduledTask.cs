using System;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;

namespace ManicTimeMonitor
{
	internal class ScheduledTask
	{
		private const string TaskName = "ManicTimeMonitor";

		public static void Register()
		{
			using (TaskService ts = new TaskService())
			{
				Task oldTask = ts.FindTask(TaskName);
				Task t = null;
				if (oldTask == null)
				{
					TimeTrigger timeTrigger = new TimeTrigger { Enabled = true };
					timeTrigger.Repetition.Interval = TimeSpan.FromMinutes(5);

					ExecAction execAction = new ExecAction(System.Reflection.Assembly.GetExecutingAssembly().Location, "update");

					t = ts.AddTask(TaskName, timeTrigger, execAction);
				}
				else
				{
					t = oldTask;
				}

				TaskEditDialog editorForm = new TaskEditDialog(t, true, true);
				DialogResult result = editorForm.ShowDialog();

				if (result != DialogResult.OK && oldTask == null)
				{
					ts.RootFolder.DeleteTask(TaskName);
				}
			}
		}
	}
}