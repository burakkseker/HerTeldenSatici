using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace her_telden_satici
{
    public partial class müsteriDüzenle : Form
    {
        public Form1 form1;
        public müsteriDüzenle()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            textBox1.Text = form1.dataGridView1.Rows[form1.satir].Cells[0].Value.ToString();
            textBox2.Text = form1.dataGridView1.Rows[form1.satir].Cells[1].Value.ToString();
            textBox3.Text = form1.dataGridView1.Rows[form1.satir].Cells[2].Value.ToString();
            textBox4.Text = form1.dataGridView1.Rows[form1.satir].Cells[3].Value.ToString();
            textBox5.Text = form1.dataGridView1.Rows[form1.satir].Cells[4].Value.ToString();
            comboBox1.Text = form1.dataGridView1.Rows[form1.satir].Cells[5].Value.ToString();
            textBox6.Text = form1.dataGridView1.Rows[form1.satir].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();     
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                form1.bag.Open();
                form1.komut.Connection = form1.bag;
                form1.komut.CommandText = "UPDATE MusteriBilgi SET MusteriNo='" + textBox1.Text + "',Ad='" + textBox2.Text + "',Soyad='" + textBox3.Text + "',Telefon='" + textBox4.Text + "',Bolge='" + textBox5.Text + "',UrunTercihi='" + comboBox1.Text + "',Adres='" + textBox6.Text + "' WHERE MusteriNo='" + form1.mno + "'";
                form1.komut.ExecuteNonQuery();
                form1.komut.Dispose();
                form1.bag.Close();
                form1.dtst.Tables["MusteriBilgi"].Clear();                
                form1.müsteriListele();                
                 this.Close();
            }
            catch
            {
                ;
            }
        }
    }
}
