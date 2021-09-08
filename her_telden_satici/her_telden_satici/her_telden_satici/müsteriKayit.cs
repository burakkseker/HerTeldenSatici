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
    public partial class müsteriKayit : Form
    {
        public Form1 form1;
        public müsteriKayit()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            form1.urunIsım();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();              
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "")
            {
                form1.bag.Open();
                form1.komut.Connection = form1.bag;
                form1.komut.CommandText = "INSERT INTO MusteriBilgi(MusteriNo,Ad,Soyad,Telefon,Adres,UrunTercihi,Bolge) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox6.Text + "','" + comboBox1.Text + "','" + textBox5.Text + "') ";
                form1.komut.ExecuteNonQuery();
                form1.komut.Dispose();
                form1.bag.Close();
                comboBox1.Items.Clear();
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
                textBox5.Clear(); textBox6.Clear();
                comboBox1.Text = "";
                form1.dtst.Tables["MusteriBilgi"].Clear();
                form1.müsteriListele();
                form1.urunIsım();
                MessageBox.Show("Kayıt işlemi başarı ile tamamlandı ! ");
            }
            else
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız !!!");
            }
        }
    }
}
