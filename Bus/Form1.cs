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
using System.IO;

namespace Bus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Переменные формы

        private int currentStationID;   //Хранит ID текущей остановки

        private Dictionary<int, string> stations;   //Словарь остановок (для избранного) - id станции, название станции

        private Dictionary<int, string> vehiclesDic = new Dictionary<int,string>(); //Словарь транспортных средств (для отображения гос.номера) - id автобуса, гос.номер

        private string vehicles = "";   //Приёмник JSON-пакета транспортных средств

        private bool isGosNumNeeded = false;    //Показывать ли номера ТС?

        #endregion

        #region Методы отображения информации

        //Показывает ТС, не отображая их гос.номер
        private void ShowBusesByStationID(int id)
        {
            this.toolStripStatusLabel_lastRenew.Text = "Время последнего обновления: " + DateTime.Now;
            if(isGosNumNeeded)
            {
                ShowBusesByStationIDWithGosNum(id);
                return;
            }

            try
            {
                this.label_currentStation.Text = "Текущая остановка: " + stations[id];
            }
            catch(Exception)
            {
                //this.label_currentStation.Text = "Текущая остановка: <нет названия>";
            }
            this.currentStationID = id;

            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            string str = wc.DownloadString("http://traffic22.ru/php/getStationForecasts.php?type=0&city=barnaul&sid=" + id.ToString());
            var jmas = JArray.Parse(str);

            dataGridView1.Rows.Clear();

            int i = 0;
            foreach (var mas in jmas)
            {
                var obj = JObject.Parse(mas.ToString());

                bool insert = true;
                String[] row = new String[6];

                foreach (var o in obj)
                {
                    if (o.Key.ToString() == "rid")      //ID маршрута
                        row[0] = o.Value.ToString();
                    if (o.Key.ToString() == "rnum")     //Номер маршрута
                        row[1] = o.Value.ToString();
                    if (o.Key.ToString() == "arrt")     //Время прибытия
                        row[2] = Math.Round(Convert.ToInt32(o.Value) / 60.0).ToString();
                    if (o.Key.ToString() == "rtype")    //Тип ТС
                        row[3] = o.Value.ToString();
                    if (o.Key.ToString() == "lastst")   //Последняя остановка
                        row[4] = o.Value.ToString();
                    if (o.Key.ToString() == "where")    //Конечная остановка
                        row[5] = o.Value.ToString();
                }

                int indexOfExistedBus = -1;

                for (int k = 0; k < dataGridView1.Rows.Count; k++)
                    if (dataGridView1.Rows[k].Cells[0].Value.ToString() == row[0].ToString())
                    {
                        insert = false;
                        indexOfExistedBus = k;
                        break;
                    }

                if (insert == true)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = row[0];
                    dataGridView1.Rows[i].Cells[1].Value = row[1];
                    dataGridView1.Rows[i].Cells[2].Value = row[2];
                    dataGridView1.Rows[i].Cells[3].Value = row[3];
                    dataGridView1.Rows[i].Cells[4].Value = row[4];
                    dataGridView1.Rows[i].Cells[5].Value = row[5];
                    i++;
                }
                else
                {
                    dataGridView1.Rows[indexOfExistedBus].Cells[2].Value += " " + row[2];
                }
            }

            //Скрываем ID маршрута
            dataGridView1.Columns[0].Visible = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[3].Value.ToString() == "Т")
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                else
                {
                    if (row.Cells[3].Value.ToString() == "М")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
                    else
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 192);
                }
            }

        }

        //Показывает ТС и отображает их гос.номер
        private void ShowBusesByStationIDWithGosNum(int id)
        {
            try
            {
                this.label_currentStation.Text = "Текущая остановка: " + stations[id];
            }
            catch (Exception)
            {
                //this.label_currentStation.Text = "Текущая остановка: <нет названия>";
            }
            this.currentStationID = id;

            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            string str = wc.DownloadString("http://traffic22.ru/php/getStationForecasts.php?type=0&city=barnaul&sid=" + id.ToString());
            var jmas = JArray.Parse(str);

            dataGridView1.Rows.Clear();

            int i = 0;
            foreach (var mas in jmas)
            {
                var obj = JObject.Parse(mas.ToString());

                bool insert = true;
                String[] row = new String[7];

                foreach (var o in obj)
                {
                    if (o.Key.ToString() == "rid")      //ID маршрута
                        row[0] = o.Value.ToString();
                    if (o.Key.ToString() == "rnum")     //Номер маршрута
                        row[1] = o.Value.ToString();
                    if (o.Key.ToString() == "arrt")     //Время прибытия
                        row[2] = Math.Round(Convert.ToInt32(o.Value) / 60.0).ToString();
                    if (o.Key.ToString() == "rtype")    //Тип ТС
                        row[3] = o.Value.ToString();
                    if (o.Key.ToString() == "lastst")   //Последняя остановка
                        row[4] = o.Value.ToString();
                    if (o.Key.ToString() == "where")    //Конечная остановка
                        row[5] = o.Value.ToString();
                    if (o.Key.ToString() == "vehid")    //ID ТС
                        row[6] = o.Value.ToString();
                }

                int indexOfExistedBus = -1;

                for (int k = 0; k < dataGridView1.Rows.Count; k++)
                    if (dataGridView1.Rows[k].Cells[0].Value.ToString() == row[0].ToString())
                    {
                        insert = false;
                        indexOfExistedBus = k;
                        break;
                    }

                string value = "";
                vehiclesDic.TryGetValue(Convert.ToInt32(row[6]), out value);
                if (value == null)
                    value = "?";

                if (insert == true)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = row[0];
                    dataGridView1.Rows[i].Cells[1].Value = row[1];
                    //dataGridView1.Rows[i].Cells[2].Value = row[2] + " (" + ShowVehiclesGosNum(Convert.ToInt32(row[6])) + ")";
                    dataGridView1.Rows[i].Cells[2].Value = row[2] + " (" + value + ")";
                    dataGridView1.Rows[i].Cells[3].Value = row[3];
                    dataGridView1.Rows[i].Cells[4].Value = row[4];
                    dataGridView1.Rows[i].Cells[5].Value = row[5];
                    i++;
                }
                else
                {
                    dataGridView1.Rows[indexOfExistedBus].Cells[2].Value += " " + row[2] + " (" + value + ")"; ;
                }
            }

            //Скрываем ID маршрута
            dataGridView1.Columns[0].Visible = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[3].Value.ToString() == "Т")
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                else
                {
                    if (row.Cells[3].Value.ToString() == "М")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
                    else
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 192);
                }
            }

        }

        //Выдает гос.номер ТС по его ID
        private string ShowVehiclesGosNum(int id)
        {
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
                            if (oo.Value.ToString() == id.ToString())
                            {
                                foreach (var k in onew)
                                {
                                    if (k.Key.ToString() == "gos_num")
                                        return k.Value.ToString();
                                }
                            }
                        }
                    }
                }
            }

            return "(?)";
        }

        #endregion

        #region Загрузчики

        private void loadVehiclesDictionary()
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;

            this.vehicles = "";
            this.vehicles += wc.DownloadString("http://traffic22.ru/php/getVehiclesMarkers.php?rids=20-0,21-0,20-1,19-1,8-0,9-0,50-0,51-0,11-1,12-1,4-1,5-1,9-1,10-1,58-0,59-0,8-1,7-1,12-0,13-0,67-0,68-0,14-0,15-0,14-1,13-1,16-1,15-1,18-1,17-1,70-0,69-0,64-0,63-0,3-1,2-1,116-0,115-0,31-0,30-0,25-0,24-0,120-0,119-0,99-0,100-0,101-0,102-0,40-0,41-0,43-0,42-0,104-0,103-0,54-0,55-0,121-0,122-0,66-0,65-0,48-0,49-0,94-0,93-0,137-0,138-0,16-0,17-0,18-0,19-0,106-0,105-0,107-0,108-0,92-0,91-0,33-0,32-0,125-0,126-0,44-0,45-0,62-0,61-0,38-0,39-0,27-0,26-0,22-0,23-0,111-0,112-0,127-0,128-0,11-0,10-0,139-0,140-0,136-0,135-0,114-0,113-0,95-0,96-0,131-0,132-0,143-0,144-0,98-0,97-0,35-0,34-0,28-0,29-0,53-0,52-0,83-0,84-0,75-0,76-0,88-0,87-0,71-0,72-0,73-0,74-0,90-0,89-0,141-0,142-0,134-0,133-0,109-0,110-0,82-0,81-0,79-0,80-0,77-0,78-0,56-0,57-0,86-0,85-0,145-0,146-0&lat0=0&lng0=0&lat1=90&lng1=180&curk=0&city=barnaul&info=12345");

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
                                foreach (var k in onew)
                                {
                                    if (k.Key.ToString() == "gos_num")
                                        vehiclesDic.Add(Convert.ToInt32(oo.Value.ToString()), k.Value.ToString());
                                        //return k.Value.ToString();
                                }
                            //}
                        }
                    }
                }
            }
        }

        //Загрузчик избранного (с настройкой кнопок добавления/удаления избранного)
        private void loadFavorites(bool stayOnLast)
        {
            //Пытаемся прочитать файл с избранным, а если его нет - создаём его
            try
            {
                stations = File.ReadLines("fav/fav.db").Select((v, i) =>
                    new { Index = i, Value = v }).GroupBy(p => p.Index / 2).
                    ToDictionary(g => Convert.ToInt32(g.First().Value), g => g.Last().Value);

                if (stations.Count > 0)
                {
                    comboBox_stations.DataSource = new BindingSource(stations, null);
                    comboBox_stations.DisplayMember = "Value";
                    comboBox_stations.ValueMember = "Key";
                    this.button_delFromFav.Enabled = true;
                }
                else
                {
                    comboBox_stations.DataSource = null;
                    comboBox_stations.Items.Clear();
                    comboBox_stations.SelectedItem = null;
                    this.button_delFromFav.Enabled = false;
                }

                if (currentStationID == 0)
                {
                    comboBox_stations.SelectedItem = null;
                    this.button_delFromFav.Enabled = false;
                }
                else
                {
                    if (stayOnLast == true)
                        comboBox_stations.SelectedItem = comboBox_stations.Items[comboBox_stations.Items.Count - 1];
                }

            }
            catch(Exception)
            {
                Directory.CreateDirectory("fav");
                File.Create("fav/fav.db");
            }
        }
        
        //Загрузчик самой формы
        private void Form1_Load(object sender, EventArgs e)
        {
            //!!!!!!!!!!!!//Только один раз подгружаем базу ТС, чтобы не нагружать сервер
            
            //this.vehicles += wc.DownloadString("http://traffic22.ru/php/getVehiclesMarkers.php?&city=barnaul&curk=0");

            loadFavorites(false);

            loadVehiclesDictionary();

        }

        #endregion

        #region Обработчики событий

        //Блокировщик выделения ячеек в таблице
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0)
                dataGridView1.SelectedCells[0].Selected = false;
        }

        //Действие по кнопке "Обновить данные"
        private void button_renew_Click(object sender, EventArgs e)
        {
            ShowBusesByStationID(currentStationID);
        }

        //Действие по кнопке "Выход"
        private void button_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Действие при установке галочки "Показать гос.номер"
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                isGosNumNeeded = true;
                ShowBusesByStationID(currentStationID);
                this.button_renewVehiclesDic.Enabled = true;
            }
            else
            {
                isGosNumNeeded = false;
                ShowBusesByStationID(currentStationID);
                this.button_renewVehiclesDic.Enabled = false;
            }
        }

        //Действие при выборе другой остановки из избранного
        private void comboBox_stations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.currentStationID = Convert.ToInt32(((KeyValuePair<int, string>)comboBox_stations.SelectedItem).Key);
                this.button_addToFav.Enabled = false; //!!!!!!!!
                ShowBusesByStationID(currentStationID);
            }
            catch(Exception)
            {

            }
        }

        //Действие при нажатии на кнопку "Добавить в избранное"
        private void button_addToFavourites_Click(object sender, EventArgs e)
        {
            AddFav af = new AddFav();
            af.currentStationID = this.currentStationID;
            af.ShowDialog();

            button_addToFav.Enabled = false;

            loadFavorites(true);
        }

        //Действие при нажатии на кнопку "Удалить из избранного"
        private void button_delFromFav_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту остановку из избранного?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if ((this.comboBox_stations.SelectedItem != null) && (res == DialogResult.OK))
            {
                string strToDel = this.comboBox_stations.SelectedValue.ToString();

                StreamReader sr = new StreamReader("fav/fav.db");
                int numToDel = 0;
                while (!sr.EndOfStream)
                {
                    string st = sr.ReadLine();
                    if (st.ToString() == strToDel.ToString())
                    {
                        break;// останавливаем цикл
                    }
                    numToDel++;
                }
                sr.Close();
                
                //MessageBox.Show(i.ToString());

                string[] readText = File.ReadAllLines("fav/fav.db");
                using (StreamWriter file = new StreamWriter("fav/fav.db", false))
                {
                    for (int i = 0; i < readText.Length; i++)
                    {
                        if (i != numToDel)
                            file.WriteLine(readText[i]);
                        else
                            i++;
                    }
                    file.Close();
                }

                loadFavorites(false);
                //this.button_delFromFav.Enabled = false;
            }
        }

        //Работа по тику таймера (20с)
        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowBusesByStationID(currentStationID);
        }

        //Действие по нажатию на кнопку "Обновить инф. о номерах ТС"
        private void button_renewVehiclesDic_Click(object sender, EventArgs e)
        {
            this.vehiclesDic.Clear();
            loadVehiclesDictionary();
            ShowBusesByStationID(currentStationID);
        }

        //Действие при поиске других остановок (по нажатию на кнопку)
        private void button1_Click(object sender, EventArgs e)
        {
            Find f = new Find();
            f.ShowDialog();

            if (f.stid != 0)
            {
                this.currentStationID = f.stid;
                this.label_currentStation.Text = "Текущая остановка: " + f.stname;
                ShowBusesByStationID(currentStationID);

                //Сначала уберём выделение с комбобокса
                this.comboBox_stations.SelectedItem = null;

                //Затем смотрим, выбрали ли какую-то остановку
            
                bool enableFav = true;

                //Проверим, есть ли выбранная остановка уже в избранном. 
                //Если есть, выделяем её в комбобоксе и блокируем добавление в избранное
                foreach(var item in comboBox_stations.Items)
                {
                    if (currentStationID == ((KeyValuePair<int, string>)item).Key)
                    {
                        enableFav = false;
                        this.comboBox_stations.SelectedItem = item;
                        break;
                    }
                }
                this.button_addToFav.Enabled = enableFav;
            }
        }

        #endregion

        //TODO: На стадии разработки
        private void button_spybus_Click(object sender, EventArgs e)
        {
            SpyBus sb = new SpyBus();
            sb.ShowDialog();
        }
    }
}
