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
    public partial class Find : Form
    {
        private Dictionary<int, List<string>> stations = new Dictionary<int,List<string>>();

        public int stid = 0;
        public string stname = "";

        public Find()
        {
            InitializeComponent();
        }

        private void Find_Load(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            string str = wc.DownloadString("http://traffic22.ru/php/getStations.php?city=barnaul");
            var jmas = JArray.Parse(str);

            dataGridView1.Rows.Clear();

            foreach (var mas in jmas)
            {

                var obj = JObject.Parse(mas.ToString());

                String[] row = new String[5];

                foreach (var o in obj)
                {
                    if(o.Key.ToString() == "id")        //ID остановки
                        row[0] = o.Value.ToString();
                    if (o.Key.ToString() == "name")     //Название остановки
                        row[1] = o.Value.ToString();
                    if (o.Key.ToString() == "descr")    //Краткое описание остановки (в сторону ... )
                        row[2] = o.Value.ToString();
                    if (o.Key.ToString() == "lat")      //Широта (для карты - точка остановки)
                        row[3] = o.Value.ToString();
                    if (o.Key.ToString() == "lng")      //Долгота (для карты - точка остановки)
                        row[4] = o.Value.ToString();
                }

                //Создатели этого сайта считают, что ID трамвайных остановок снова можно начинать писать с нуля.
                //Сомневаюсь в их адекватности
                try
                {
                    List<string> lst = new List<string>();
                    lst.Add(row[1]);
                    lst.Add(row[2]);
                    lst.Add(row[3]);
                    lst.Add(row[4]);
                    stations.Add(Convert.ToInt32(row[0]), lst);
                }
                catch(Exception)
                {
                    break;
                }
                //dataGridView1.Rows.Add();
                //dataGridView1.Rows[i].Cells[0].Value = row[0] + " (" + row[1] + ")";
                //dataGridView1.Rows[i].Cells[1].Value = "Выбрать";
            }
        }

        private void button_find_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if(this.textBox1.Text != String.Empty)
            {
                this.dataGridView1.Enabled = true;
                int i = 0;
                foreach(var st in stations)
                {
                    if(st.Value[0].ToString().ToLower().Contains(this.textBox1.Text.ToLower()))
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = st.Key;
                        dataGridView1.Rows[i].Cells[1].Value = st.Value[0] + " (" + st.Value[1] + ")";
                        dataGridView1.Rows[i].Cells[2].Value = "Показать на карте";
                        dataGridView1.Rows[i].Cells[3].Value = "Выбрать";
                        dataGridView1.Rows[i].Cells[4].Value = st.Value[2];
                        dataGridView1.Rows[i].Cells[5].Value = st.Value[3];
                        i++;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                this.stid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                this.stname = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.Close();
            }
            else
            {
                if (e.ColumnIndex == 2)
                {
                    Site s = new Site();
                    s.lat = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    s.lng = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                    s.ShowDialog();
                    //this.stid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    //this.stname = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //this.Close();
                }
            }
        }
    }
}
