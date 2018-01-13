using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace ManicTimeMonitor
{
	internal class Updater
	{
		public delegate void NotificationEventHandler(String message);

		private readonly String _databaseLocation;
		private readonly IDictionary<string, int> _lastRecord = new Dictionary<string, int>();
		private readonly IDictionary<string, String> _queries = new Dictionary<string, String>();
		private readonly String _updateUrl;

		public Updater(string updateUrl, string databaseLocation)
		{
			_queries["Activity"] = "ActivityId, TimelineId, DisplayName, GroupId, StartLocalTime, EndLocalTime";
			_queries["Group"] = "GroupId, TimelineId, DisplayName, Icon32, FolderId, TextData";
			_queries["Timeline"] = "TimelineId, TypeName, SourceTypeName";

			_lastRecord["Activity"] = 0;
			_lastRecord["Group"] = 0;
			_lastRecord["Timeline"] = 0;

			_databaseLocation = databaseLocation;
			_updateUrl = updateUrl;
		}

		public event NotificationEventHandler ProgressMessage;

		public void Update()
		{
			int failures = 0;
			OnProgressMessage("Let's do this");

			// See if we can open the database before wasting precioous bandwidth
			SqlCeConnection conn = new SqlCeConnection("Data Source = " + _databaseLocation + ";Max Database Size=4000;");
			conn.Open();

			String refresh = httpRequest(_updateUrl + "&refresh");
			if (refresh.Trim() != "")
			{
				XDocument doc = XDocument.Parse(refresh);
				foreach (var table in doc.Descendants())
				{
					if (table.Name.ToString() == "root") continue;
					OnProgressMessage("Got last record for " + table.Name + ": " + table.Value);
					_lastRecord[table.Name.ToString()] = Convert.ToInt32(table.Value);
				}
			}

			foreach (string table in new List<string>(_queries.Keys))
			{
				OnProgressMessage("Processing " + table);
				DataSet ds;
				do
				{
					SqlCeCommand cmd = new SqlCeCommand("select " + _queries[table] + " from [" + table + "] where [" + table + "Id] > " + _lastRecord[table] + " order by [" + table + "Id] ASC", conn);
					ds = new DataSet(table);
					new SqlCeDataAdapter(cmd).Fill(ds, 0, 1500, "Row");

					if (ds.Tables[0].Rows.Count > 0)
					{
						String response = httpRequest(_updateUrl, ds.GetXml());
						DataRow lastRow = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
						if (response == lastRow[table + "Id"].ToString())
						{
							_lastRecord[table] = Convert.ToInt32(response);
							if (table == "Activity")
							{
								OnProgressMessage("Pushed up to ID " + _lastRecord[table] + " (" + lastRow["EndLocalTime"] + ") (" + ds.Tables[0].Rows.Count + " Rows)");
							}
							else
							{
								OnProgressMessage("Pushed up to ID " + _lastRecord[table] + " (" + ds.Tables[0].Rows.Count + " Rows)");
							}
							
						}
						else
						{
							OnProgressMessage("FAILED: " + response);
							if (++failures > 10)
							{
								OnProgressMessage("Giving up after 10 failures.");
								throw new Exception("Giving up, bro");
							}
						}
					}
				} while (ds.Tables[0].Rows.Count != 0);
			}
			OnProgressMessage("Done");
			conn.Close();
		}


		private string httpRequest(String url, String post = null)
		{
			WebRequest httpWebRequest = WebRequest.Create(url);

			if (post != null)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(post);
				httpWebRequest.Method = "POST";
				using (Stream request = httpWebRequest.GetRequestStream())
				{
					using (var zipStream = new GZipStream(request, CompressionMode.Compress))
					{
						zipStream.Write(bytes, 0, bytes.Length);
						zipStream.Flush();
						zipStream.Close();
					}
					request.Flush();
					request.Close();
				}
			}

			Stream response = httpWebRequest.GetResponse().GetResponseStream();

			using (StreamReader streamReader = new StreamReader(response))
			{
				return streamReader.ReadToEnd();
			}
		}

		protected virtual void OnProgressMessage(String message)
		{
			if (ProgressMessage != null)
			{
				ProgressMessage(message);
			}
		}
	}
}