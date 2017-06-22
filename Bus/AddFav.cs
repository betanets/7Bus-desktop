using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bus
{
    public partial class AddFav : Form
    {
        public AddFav()
        {
            InitializeComponent();
        }

        public int currentStationID;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter("fav/fav.db", true);

                sw.WriteLine(currentStationID);
                sw.WriteLine(this.textBox1.Text);

                sw.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Close();
        }
    }
}
