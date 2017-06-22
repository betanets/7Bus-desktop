using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bus
{
    public partial class Site : Form
    {
        public Site()
        {
            InitializeComponent();
        }

        public int lat = 0;
        public int lng = 0;

        private void Site_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //webBrowser1.Navigate("https://yandex.ru/maps/197/barnaul/?ll=" + ((double)lng / 1000000) + "%2C" + ((double)lat / 1000000) + "&z=14");
            //webControl1.LoadHTML("https://www.google.ru/maps/@" + ((double)lat / 1000000) + "," + ((double)lng / 1000000) + ",15z");
            //webBrowser1.Navigate("https://www.google.ru/maps/@" + ((double)lat / 1000000) + "," + ((double)lng / 1000000) + ",15z");//+ 53.3561804,83.6871964,14.5z");
            webBrowser1.Navigate("http://www.openstreetmap.org/?mlat=" + ((double)lat / 1000000) + "&mlon="  + ((double)lng / 1000000) + "&zoom=18");
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        public int stid = 0;

        
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //MessageBox.Show(webBrowser1.Url.ToString());
            if (webBrowser1.Url.ToString().IndexOf("stid") != -1)
            {
                int from = webBrowser1.Url.ToString().IndexOf("stid");
                int newlen = webBrowser1.Url.ToString().Length - webBrowser1.Url.ToString().IndexOf("stid");
                string cut = webBrowser1.Url.ToString().Substring(from, newlen);

                from = 0;
                newlen = cut.IndexOf("&");
                cut = cut.Substring(from, newlen);

                from = 5;
                newlen = cut.Length - 5;
                cut = cut.Substring(from, newlen);

                this.stid = Convert.ToInt32(cut);
                this.Close();
            }
        }
    }
}
