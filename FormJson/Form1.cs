using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace FormJson
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();

            string value1 = ConfigurationManager.AppSettings["url"];

            WebClient client = new WebClient();
            Stream data = client.OpenRead(value1);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            dynamic jsonObj = JsonConvert.DeserializeObject(s);
            var data1 = client.DownloadString(value1);
            JObject o = JObject.Parse(data1);

            if (o["status"].ToString()=="success")
            {
                label7.Text = "Aktif";
                label7.ForeColor = Color.Green;

            }
            else
            {
                label7.Text = "Pasif";
                label7.ForeColor = Color.Red;
                timer1.Stop();
            }

            label10.Text = o["msg"].ToString();
            bool durum = true;
            var ekle = o["result"];

            listView1.Items.Clear();

            foreach (var item in ekle)
            {

                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Clear();

                lvi.SubItems.Add(item["TITLE"].ToString());
                lvi.SubItems.Add(item["DESC"].ToString());
                lvi.SubItems.Add(item["STATUS"].ToString());
                lvi.SubItems.Add(item["LAST_WORK"].ToString());
                lvi.SubItems.Add(item["MODE"].ToString());
                lvi.SubItems.Add(item["MAX_TIME"].ToString());

                if (durum.ToString() == item["STATUS"].ToString())
                {
                    lvi.Text = "Çalışıyor";
                }
                else
                {
                    lvi.Text = "Çalışmıyor";
                    lvi.ForeColor = Color.Red;
                }
                lvi.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));
                listView1.Items.Add(lvi);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
       
            string value1 = ConfigurationManager.AppSettings["url"];

            WebClient client = new WebClient();
            Stream data = client.OpenRead(value1);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            dynamic jsonObj = JsonConvert.DeserializeObject(s);
            var data1 = client.DownloadString(value1);
            JObject o = JObject.Parse(data1);

            bool durum = true;
            var ekle = o["result"];

            listView1.Items.Clear();

            foreach (var item in ekle)
            {

                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Clear();

                lvi.SubItems.Add(item["TITLE"].ToString());
                lvi.SubItems.Add(item["DESC"].ToString());
                lvi.SubItems.Add(item["STATUS"].ToString());
                lvi.SubItems.Add(item["LAST_WORK"].ToString());
                lvi.SubItems.Add(item["MODE"].ToString());
                lvi.SubItems.Add(item["MAX_TIME"].ToString());

                if (durum.ToString() == item["STATUS"].ToString())
                {
                    lvi.Text = "Çalışıyor";
                }
                else
                {
                    lvi.Text = "Çalışmıyor";
                    lvi.ForeColor = Color.Red;
                }
                lvi.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));
                listView1.Items.Add(lvi);
            }
        }        
    }
}
