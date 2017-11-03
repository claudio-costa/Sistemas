using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobLink.WebLeilao.Importador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            comboBox1.DisplayMember = "descricao";
            comboBox1.ValueMember = "id";

            var itens = new List<object>();
            itens.Add(new { id = 1, descricao = "Arrematantes" });
            itens.Add(new { id = 2, descricao = "Lotes"  });

            foreach (var item in itens)
            {
                comboBox1.Items.Add(item);
            }
        }
    }
}
