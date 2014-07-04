using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml.Linq;

namespace ManicTimeMonitor
{
	class Updater
	{
		private IDictionary<string, String> Queries = new Dictionary<string, String>();
		private IDictionary<string, int> LastRecord = new Dictionary<string, int>();
		private String DatabaseLocation;
		private String UpdateUrl;

		public delegate void NotificationEventHandler(String message);

		public event NotificationEventHandler ProgressMessage;

		public Updater(string updateUrl, string databaseLocation)
		{
			Queries["Activity"] = "ActivityId, TimelineId, DisplayName, GroupId, StartLocalTime, EndLocalTime";
			Queries["Group"] = "GroupId, TimelineId, DisplayName, Icon32, FolderId, TextData";
			Queries["Timeline"] = "TimelineId, TypeName, SourceTypeName";

			LastRecord["Activity"] = 0;
			LastRecord["Group"] = 0;
			LastRecord["Timeline"] = 0;

			DatabaseLocation = databaseLocation;
			UpdateUrl = updateUrl;
		}

		public void Update()
		{
			int failures = 0;
			OnProgressMessage("Let's do this");

			// See if we can open the database before wasting precioous bandwidth
			SqlCeConnection conn = new SqlCeConnection("Data Source = " + DatabaseLocation);
			conn.Open();

			String refresh = httpRequest(UpdateUrl + "&refresh");
			if (refresh.Trim() != "")
			{
				XDocument doc = XDocument.Parse(refresh);
				foreach (var table in doc.Descendants())
				{
					if (table.Name.ToString() == "root") continue;
					OnProgressMessage("Got last record for " + table.Name.ToString() + ": " + table.Value);
					LastRecord[table.Name.ToString()] = Convert.ToInt32(table.Value);
				}
			}

			foreach (string table in new List<string>(Queries.Keys))
			{
				OnProgressMessage("Processing " + table);
				DataSet ds = null;
				do
				{
					SqlCeCommand cmd = new SqlCeCommand("select " + Queries[table] + " from [" + table + "] where [" + table + "Id] > " + LastRecord[table] + " order by [" + table + "Id] ASC", conn);
					ds = new DataSet(table);
					new SqlCeDataAdapter(cmd).Fill(ds, 0, 1500, "Row");

					if (ds.Tables[0].Rows.Count > 0)
					{
						String response = httpRequest(UpdateUrl, ds.GetXml());
						if (response == ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][table + "id"].ToString())
						{
							LastRecord[table] = Convert.ToInt32(response);
							OnProgressMessage("Pushed up to ID " + LastRecord[table] + "  (" + ds.Tables[0].Rows.Count + " Rows)");
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


		private string httpRequest(String url, String POST = null)
		{
			WebRequest httpWebRequest = WebRequest.Create(url);

			if (POST != null)
			{
				byte[] bytes = System.Text.Encoding.ASCII.GetBytes(POST);
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

			using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
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
