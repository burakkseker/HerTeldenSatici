using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace her_telden_satici
{
    public partial class stoklar : Form
    {
        public Form1 form1;
        public stoklar()
        {
            InitializeComponent();
        }
        public string  sil;
        private void Form3_Load(object sender, EventArgs e)
        {
               form1.stokGoster();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form1.dtst.Tables["StokBilgi"].Clear();
            this.Close();                 
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    form1.bag.Open();
                    form1.komut.Connection = form1.bag;
                    form1.komut.CommandText = "INSERT INTO StokBilgi(UrunAd,Fiyat,Adet) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "') ";
                    form1.komut.ExecuteNonQuery();
                    form1.komut.Dispose();
                    form1.bag.Close();
                    MessageBox.Show("Kayıt işlemi tamamlandı ! ");
                    form1.dtst.Tables["StokBilgi"].Clear();
                    form1.dtst.Tables["MusteriBilgi"].Clear();
                    form1.müsteriListele();
                    form1.stokGoster();
                    form1.form4.comboBox1.Items.Clear();
                    form1.form4.comboBox2.Items.Clear();
                    form1.form4.comboBox3.Items.Clear();
                    form1.urunIsım();
                }
                catch
                {

                    ;
                }
            }
            else
            {
                MessageBox.Show("Müşteri No'yu girmelisiniz !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {                
                int row = 0;
                for (row = 0; row < dataGridView1.Rows.Count; row++)
                {

                    if (dataGridView1.Rows[row].Cells[0].Selected == true || dataGridView1.Rows[row].Cells[1].Selected == true || dataGridView1.Rows[row].Cells[2].Selected == true)
                    {
                        break;

                    }
                }
                sil= dataGridView1.Rows[row].Cells[0].Value.ToString();

                DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    form1.bag.Open();
                    form1.komut.Connection = form1.bag;
                    form1.komut.CommandText = "DELETE from StokBilgi WHERE UrunAd='" + sil+ "'";
                    form1.komut.ExecuteNonQuery();
                    form1.komut.Dispose();
                    form1.bag.Close();
                    form1.dtst.Clear();
                    form1.stokGoster();
                    form1.müsteriListele();
                }
            }
            catch
            {
                ;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From MusteriBilgi", form1.bag);
            if (textBox4.Text == "")
            {
                form1.komut.Connection = form1.bag;
                form1.komut.CommandText = "Select * from StokBilgi";
                adtr.SelectCommand = form1.komut;
                adtr.Fill(form1.dtst, "StokBilgi");
            }
            if (Convert.ToBoolean(form1.bag.State) == false)
            {
                form1.bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From StokBilgi" +
                 " where(UrunAd like '%" + textBox4.Text + "%' )";
            form1.dtst.Clear();
            adtr.Fill(form1.dtst, "StokBilgi");
            form1.bag.Close();     
        }
    }
}
