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
    public partial class siparisAl : Form
    {
        public Form1 form1;
        public siparisAl()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear(); comboBox2.Items.Clear();
            form1.urunIsım();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();                
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int sonuc = 0, fiyat, adet;
                string tutar, tarih, aciklama = "Satış";
                string[] araci;
                tarih = DateTime.Now.ToShortDateString();
                araci = comboBox2.Text.Split(' ');
                fiyat = int.Parse(araci[0]);
                adet = int.Parse(comboBox3.Text);
                sonuc = fiyat * adet;
                tutar = sonuc.ToString() + " TL";
                form1.bag.Open();
                form1.komut.Connection = form1.bag; 
                form1.komut.CommandText = "INSERT INTO siparisler(MusteriNo,Ad,Soyad,Durum,Adres,Tutar,Tarih) VALUES ('" + form1.mno + "','" + form1.ad + "','" + form1.soyad + "','" + "','" + form1.adres + "','" + tutar + "','" +tarih + "') ";
                form1.komut.ExecuteNonQuery();
                form1.komut.CommandText = "INSERT INTO GelirBilgi(Aciklama,Tutar,Tarih) VALUES ('" + aciklama + "','" + tutar + "','" + tarih + "') ";
                form1.komut.ExecuteNonQuery();
                form1.komut.Dispose();
                form1.bag.Close();
                form1.dtst.Tables["siparisler"].Clear();
                MessageBox.Show("Kayıt işlemi başarı ile tamamlandı ! ");
                this.Close();
            }
            catch
            {

                ;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            index = comboBox1.SelectedIndex;
            comboBox2.SelectedIndex = index;
        }
    }
}
