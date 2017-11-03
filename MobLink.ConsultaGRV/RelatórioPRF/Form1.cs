using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelatórioPRF
{
    public partial class Form1 : Form
    {

        public struct Deposito
        {
            public int id_cliente { get; set; }
            public int id_deposito { get; set; }
            public string deposito_nome { get; set; }
        }

        private int _idcliente = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<object> dados = new List<object>();
            dados.Add(new { cod = 13, desc = "SRPRF/PB" });
            dados.Add(new { cod = 14, desc = "SRPRF/PE" });

            comboBox1.DisplayMember = "desc";
            comboBox1.ValueMember = "cod";
            comboBox1.DataSource = dados;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id_cliente = (int)((ComboBox)sender).SelectedValue;
            _idcliente = id_cliente;

            List<Deposito> dados = new List<Deposito>();

            if (id_cliente == 13)
            {
                //13  10  CAMPINA GRANDE
                //13  11  JOÃO PESSOA
                //13  13  CAJAZEIRAS
                //13  12  PATOS

                dados.Add(new Deposito() { id_cliente = 13, id_deposito = 10, deposito_nome = "CAMPINA GRANDE" });
                dados.Add(new Deposito() { id_cliente = 13, id_deposito = 11, deposito_nome = "JOÃO PESSOA" });
                dados.Add(new Deposito() { id_cliente = 13, id_deposito = 13, deposito_nome = "CAJAZEIRAS" });
                dados.Add(new Deposito() { id_cliente = 13, id_deposito = 12, deposito_nome = "PATOS" });

                listBox1.DataSource = dados;
                listBox1.ValueMember = "id_deposito";
                listBox1.DisplayMember = "deposito_nome";
            }

            if (id_cliente == 14)
            {
                //14  5   PETROLINA
                //14  14  GARANHUNS
                //14  15  SALGUEIRO
                //14  16  SERRA TALHADA
                //14  17  CARUARU

                dados.Add(new Deposito { id_cliente = 14, id_deposito = 5, deposito_nome = "PETROLINA" });
                dados.Add(new Deposito { id_cliente = 14, id_deposito = 14, deposito_nome = "GARANHUNS" });
                dados.Add(new Deposito { id_cliente = 14, id_deposito = 15, deposito_nome = "SALGUEIRO" });
                dados.Add(new Deposito { id_cliente = 14, id_deposito = 16, deposito_nome = "SERRA TALHADA" });
                dados.Add(new Deposito { id_cliente = 14, id_deposito = 17, deposito_nome = "CARUARU" });

                listBox1.DataSource = dados;
                listBox1.ValueMember = "id_deposito";
                listBox1.DisplayMember = "deposito_nome";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Deposito> depositosSelecionados = new List<Deposito>();
            
            foreach (Deposito item in listBox1.SelectedItems)
            {
                depositosSelecionados.Add(item);
            }

            var teste = string.Join(",", depositosSelecionados.Select(p => p.id_deposito));

            Saida s = new Saida();

            s.webBrowser1.Navigate("http://localhost:60187/GRV/RelatorioPRF?id=" + _idcliente + "&depositos=" + teste);

            s.Show();
            
        }

        
    }
}
