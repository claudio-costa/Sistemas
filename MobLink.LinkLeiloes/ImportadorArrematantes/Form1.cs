using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportadorArrematantes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT* FROM sys.all_objects WHERE type = 'U'";

            var ret = database.Arrematante.ConsultaTabelas(sql);


            comboBox1.DataSource = ret;
            comboBox1.ValueMember = "object_id";
            comboBox1.DisplayMember = "name";


            //"wf_segoe-ui_semilight", "Segoe UI Semilight", "Segoe WP Semilight", "Segoe UI", "Segoe WP", Tahoma, Arial, sans - serif
        }
    }
}
