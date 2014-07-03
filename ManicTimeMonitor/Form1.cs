using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace ManicTimeMonitor
{
    public partial class Form1 : Form
    {
        public String UpdateUrl;
        public String DatabaseLocation;

        public Form1()
        {
            InitializeComponent();
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


        private void PushUpdates()
        {
            try
            {
                IDictionary<string, String> Queries = new Dictionary<string, String>();
                IDictionary<string, int> LastRecord = new Dictionary<string, int>();
                int failures = 0;

                Queries["Activity"] = "ActivityId, TimelineId, DisplayName, GroupId, StartLocalTime, EndLocalTime";
                Queries["Group"] = "GroupId, TimelineId, DisplayName, Icon32, FolderId, TextData";
                Queries["Timeline"] = "TimelineId, TypeName, SourceTypeName";

                LastRecord["Activity"] = 0;
                LastRecord["Group"] = 0;
                LastRecord["Timeline"] = 0;

                LogTxt.Text = "Let's do this\r\n";

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
                        LogTxt.AppendText("Got last record for " + table.Name.ToString() + ": " + table.Value + "\r\n");
                        LastRecord[table.Name.ToString()] = Convert.ToInt32(table.Value);
                    }
                }


                foreach (string table in new List<string>(Queries.Keys))
                {
                    LogTxt.AppendText("Processing " + table + "\r\n");
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
                                LogTxt.AppendText("Pushed up to ID " + LastRecord[table] + "  (" + ds.Tables[0].Rows.Count + " Rows)\r\n");
                            }
                            else
                            {
                                LogTxt.AppendText("FAILED: " + response);
                                if (++failures > 10)
                                {
                                    LogTxt.AppendText("Giving up after 10 failures.");
                                    throw new Exception("Giving up, bro");
                                }
                            }
                        }
                        Application.DoEvents();
                    } while (ds.Tables[0].Rows.Count != 0);
                }
                LogTxt.AppendText("Done");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateUrl = "https://alexou.net/manictime/?pc=" + Environment.MachineName;
            DatabaseLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Finkit\\ManicTime\\ManicTime.sdf";
            UpdateUrlTxt.Text = UpdateUrl;
            DatabaseLocationTxt.Text = DatabaseLocation;
        }


        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateBtn.Enabled = false;
            PushUpdates();
            UpdateBtn.Enabled = true;
        }


        private void QuitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void DatabaseLocationTxt_TextChanged(object sender, EventArgs e)
        {
            DatabaseLocation = DatabaseLocationTxt.Text;
        }


        private void UpdateUrlTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateUrl = UpdateUrlTxt.Text;
        }
    }
}