using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Bus
{
    public partial class SpyBus : Form
    {
        public SpyBus()
        {
            InitializeComponent();
        }

        private string vehicles = "";
        private Dictionary<int, List<string>> vehiclesDic = new Dictionary<int, List<string>>();
        private Dictionary<int, List<string>> currentFinding = new Dictionary<int, List<string>>();

        private string lat;
        private string lng;

        private void loadVehiclesDictionary()
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;

            this.vehicles = "";
            this.vehicles += wc.DownloadString("http://traffic22.ru/php/getVehiclesMarkers.php?rids=20-0,21-0,20-1,19-1,8-0,9-0,50-0,51-0,11-1,12-1,4-1,5-1,9-1,10-1,58-0,59-0,8-1,7-1,12-0,13-0,67-0,68-0,14-0,15-0,14-1,13-1,16-1,15-1,18-1,17-1,70-0,69-0,64-0,63-0,3-1,2-1,116-0,115-0,31-0,30-0,25-0,24-0,120-0,119-0,99-0,100-0,101-0,102-0,40-0,41-0,43-0,42-0,104-0,103-0,54-0,55-0,121-0,122-0,66-0,65-0,48-0,49-0,94-0,93-0,137-0,138-0,16-0,17-0,18-0,19-0,106-0,105-0,107-0,108-0,92-0,91-0,33-0,32-0,125-0,126-0,44-0,45-0,62-0,61-0,38-0,39-0,27-0,26-0,22-0,23-0,111-0,112-0,127-0,128-0,11-0,10-0,139-0,140-0,136-0,135-0,114-0,113-0,95-0,96-0,131-0,132-0,143-0,144-0,98-0,97-0,35-0,34-0,28-0,29-0,53-0,52-0,83-0,84-0,75-0,76-0,88-0,87-0,71-0,72-0,73-0,74-0,90-0,89-0,141-0,142-0,134-0,133-0,109-0,110-0,82-0,81-0,79-0,80-0,77-0,78-0,56-0,57-0,86-0,85-0&lat0=0&lng0=0&lat1=90&lng1=180&curk=0&city=barnaul&info=12345");

            var jmas = JObject.Parse(this.vehicles);

            var jjmas = jmas.First.Next;

            //Здесь пытаемся получить номера ТС
            foreach (var mas in jjmas)
            {
                var obj = JArray.Parse(mas.ToString());

                foreach (var o in obj)
                {
                    var onew = JObject.Parse(o.ToString());

                    foreach (var oo in onew)
                    {
                        if (oo.Key.ToString() == "id")
                        {
                            //if (oo.Value.ToString() == id.ToString())
                            //{
                            List<string> curVeh = new List<string>();
                            foreach (var k in onew)
                            {
                                if (k.Key.ToString() == "lon")
                                {
                                    curVeh.Add(k.Value.ToString());
                                }
                                if (k.Key.ToString() == "lat")
                                {
                                    curVeh.Add(k.Value.ToString());
                                }
                                if(k.Key.ToString() == "speed")
                                {
                                    curVeh.Add(k.Value.ToString());
                                }
                                if(k.Key.ToString() == "lasttime")
                                {
                                    curVeh.Add(k.Value.ToString());
                                }
                                if (k.Key.ToString() == "gos_num")
                                {
                                    string tmpstr = k.Value.ToString();
                                    tmpstr = tmpstr.Replace(" ", "");
                                    tmpstr = tmpstr.ToUpper();
                                    curVeh.Add(tmpstr);
                                    //vehiclesDic.Add(Convert.ToInt32(oo.Value.ToString()), tmpstr);
                                }
                                if(k.Key.ToString() == "rnum")
                                {
                                    curVeh.Add(k.Value.ToString());
                                    vehiclesDic.Add(Convert.ToInt32(oo.Value.ToString()), curVeh);
                                }
                                //try
                                //{
                                    
                                //}
                                //catch(Exception)
                                //{

                                //}

                                //return k.Value.ToString();
                            }
                            //}
                        }
                    }
                }
            }
        }

        private void SpyBus_Load(object sender, EventArgs e)
        {
            loadVehiclesDictionary();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.listBox_info.Items.Clear();
            this.listBox_sr.Items.Clear();
            this.currentFinding.Clear();
            this.button_showOnMap.Enabled = false;

            this.listBox_sr.Enabled = true;

            if (this.textBox_gosNum.Text.Length > 0)
            {
                foreach (var item in vehiclesDic)
                {
                    if (item.Value[4].ToString().Contains(textBox_gosNum.Text.ToString().ToUpper()))
                    {
                        this.listBox_sr.Items.Add(item.Value[4].ToString());
                        this.button_showBusInfo.Enabled = true;
                        currentFinding.Add(item.Key, item.Value);
                    }
                }
            }

            /*
            if (this.textBox_gosNum.Text.Length > 0)
            {
                foreach (var item in vehiclesDic)
                {
                    if(item.Value[4].ToString().Contains(textBox_gosNum.Text.ToString().ToUpper()))
                    {
                        //this.listBox1.Items.Add(item.Key.ToString());
                        this.listBox_info.Items.Add("Точный гос. номер: " + item.Value[4].ToString());
                        this.listBox_info.Items.Add("На маршруте: " + item.Value[5].ToString());
                        this.listBox_info.Items.Add("Скорость: " + item.Value[2].ToString() + " км/ч");
                        string time = item.Value[3].ToString();
                        DateTime dt = Convert.ToDateTime(time);
                        dt = dt.AddHours(7);
                        this.listBox_info.Items.Add("Время посл. передачи геоданных: " + dt.ToString());
                        
                        this.button_showOnMap.Enabled = true;
                        this.lat = item.Value[1].ToString();
                        this.lng = item.Value[0].ToString();
                        break;
                    }
                }
            }
            */
        }

        private void button_showOnMap_Click(object sender, EventArgs e)
        {
            Site s = new Site();
            s.lat = Convert.ToInt32(this.lat);
            s.lng = Convert.ToInt32(this.lng);
            s.ShowDialog();
        }

        private void button_showBusInfo_Click(object sender, EventArgs e)
        {
            this.listBox_info.Enabled = true;
            this.listBox_info.Items.Clear();

            int selectedIndex = this.listBox_sr.SelectedIndex;

            if (selectedIndex != -1)
            {
                var item = currentFinding.ElementAt(selectedIndex);

                this.listBox_info.Items.Add("Точный гос. номер: " + item.Value[4].ToString());
                this.listBox_info.Items.Add("На маршруте: " + item.Value[5].ToString());
                this.listBox_info.Items.Add("Скорость: " + item.Value[2].ToString() + " км/ч");
                string time = item.Value[3].ToString();
                DateTime dt = Convert.ToDateTime(time);
                dt = dt.AddHours(7);
                this.listBox_info.Items.Add("Время посл. передачи геоданных: " + dt.ToString());

                this.button_showOnMap.Enabled = true;
                this.lat = item.Value[1].ToString();
                this.lng = item.Value[0].ToString();
            }
            else
                MessageBox.Show("Выберите автобус из списка", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
