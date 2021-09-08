using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace su_ve_tup_takip
{
    public partial class Form1 : Form
    {
        public Form2 frm2;
        public Form3 frm3;
        public Form4 frm4;
        public Form5 frm5;
        public Form6 frm6; 
        public Form1()
        {
            InitializeComponent();
            frm2 = new Form2();
            frm3 = new Form3();
            frm4 = new Form4();
            frm5 = new Form5();
            frm6 = new Form6();
            frm2.frm1 = this;
            frm3.frm1 = this;
            frm4.frm1 = this;
            frm5.frm1 = this;
            frm6.frm1 = this;
        }
        public OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=data.mdb");
        public OleDbCommand kmt = new OleDbCommand();
        public OleDbDataAdapter adtr = new OleDbDataAdapter();
        public DataSet dtst = new DataSet();       
        public string silinecek;
        public string mno,ad,soyad,adres;
        public int satir = 0;
        public void listele()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select MusteriNo,Ad,Soyad,Telefon,Adres,UrunTercihi,Bolge From musbil ", bag);
            adtr.Fill(dtst, "musbil");            
            dataGridView1.DataSource = dtst.Tables["musbil"];
            adtr.Dispose();
            bag.Close();
        }
        public void siparisler()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From siparisler WHERE Tarih='" + dateTimePicker1.Text + "'", bag);
            adtr.Fill(dtst, "siparisler");           
            dataGridView2.DataSource = dtst.Tables["siparisler"];
            adtr.Dispose();
            bag.Close();
        }
        public void stoklistele()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From stokbil ", bag);
            adtr.Fill(dtst, "stokbil");            
            frm3.dataGridView1.DataSource = dtst.Tables["stokbil"];
            adtr.Dispose();
            bag.Close();
        }
        public void urunad()
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "Select * from stokbil";
            OleDbDataReader oku;
            oku = kmt.ExecuteReader();
            while (oku.Read())
            {
                frm2.comboBox1.Items.Add(oku[0].ToString());
                frm4.comboBox1.Items.Add(oku[0].ToString());
                frm4.comboBox2.Items.Add(oku[1].ToString());
            }
            bag.Close();
            oku.Dispose();
        }
       
        public void filter()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From gelirbil WHERE Tarih='" +frm6.dateTimePicker1.Text+ "'", bag);
            adtr.Fill(dtst, "gelirbil");
           frm6.dataGridView1.DataSource = dtst.Tables["gelirbil"];
            adtr.Dispose();
            bag.Close();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {            
            listele();            
            dataGridView1.Columns[1].HeaderText = "Adı";
            dataGridView1.Columns[2].HeaderText = "Soyadı";
            dataGridView1.Columns[3].HeaderText = "Telefon";
            dataGridView1.Columns[4].HeaderText = "Adres";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 200;            
            siparisler();         
                       
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            frm2.ShowDialog();
        
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                int row = 0;
                for (row = 0; row < dataGridView1.Rows.Count; row++)
                {

                    if (dataGridView1.Rows[row].Cells[0].Selected == true || dataGridView1.Rows[row].Cells[1].Selected == true || dataGridView1.Rows[row].Cells[2].Selected == true || dataGridView1.Rows[row].Cells[3].Selected == true || dataGridView1.Rows[row].Cells[4].Selected == true || dataGridView1.Rows[row].Cells[5].Selected == true || dataGridView1.Rows[row].Cells[6].Selected == true)
                    {
                        break;

                    }
                }
                silinecek = dataGridView1.Rows[row].Cells[0].Value.ToString(); 

                DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "DELETE from musbil WHERE MusteriNo='" + silinecek + "'";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    dtst.Clear();
                    dtst.Tables["musbil"].Clear();
                    siparisler();
                    listele();
                }
            }
            catch
            { 
                ;            
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {

            for (satir = 0; satir < dataGridView1.Rows.Count; satir++)
            {

                if (dataGridView1.Rows[satir].Cells[0].Selected == true || dataGridView1.Rows[satir].Cells[1].Selected == true || dataGridView1.Rows[satir].Cells[2].Selected == true || dataGridView1.Rows[satir].Cells[3].Selected == true || dataGridView1.Rows[satir].Cells[4].Selected == true)
                {
                    break;

                }
            }
            mno = dataGridView1.Rows[satir].Cells[0].Value.ToString(); //DataGridView1.rows[row].Cells[0].Value.ToString();

            frm5.ShowDialog();           
        }     

        private void Button3_Click(object sender, EventArgs e)
        {
            frm6.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            frm3.ShowDialog();            
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From musbil", bag);
            if (TextBox1.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from musbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "musbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From musbil" +
                 " where(MusteriNo like '%" + TextBox1.Text + "%' )";
            dtst.Clear();
            adtr.Fill(dtst, "musbil");
            bag.Close();        
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From musbil", bag);
            if (TextBox2.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from musbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "musbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From musbil" +
                 " where(Ad like '%" + TextBox2.Text + "%' )";
            dtst.Clear();
            adtr.Fill(dtst, "musbil");
            bag.Close();   
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From musbil", bag);
            if (textBox3.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from musbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "musbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From musbil" +
                 " where(Soyad like '%" + textBox3.Text + "%' )";
            dtst.Clear();
            adtr.Fill(dtst, "musbil");
            bag.Close();   
        }
        private void button6_Click(object sender, EventArgs e)
        {            
            frm4.ShowDialog();          

        }

        private void button7_Click(object sender, EventArgs e)
        {          
            try
            {
                string durum;
                durum = "Yola Çıktı";
                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText = "UPDATE siparisler SET Durum='" + durum + "' WHERE MusteriNo='" + mno + "'";
                kmt.ExecuteNonQuery();
                kmt.Dispose();
                bag.Close();
                dtst.Tables["siparisler"].Clear();
                siparisler();                
            }
            catch
            {
                ;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {           
            try
            {
                string durum;
                durum = "Teslim Edildi";
                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText = "UPDATE siparisler SET Durum='" + durum + "' WHERE MusteriNo='" + mno + "'";
                kmt.ExecuteNonQuery();
                kmt.Dispose();
                bag.Close();
                dtst.Tables["siparisler"].Clear();
                siparisler();                
            }
            catch
            {

                ;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                int row = 0;
                for (row = 0; row <= dataGridView2.Rows.Count; row++)
                {

                    if (dataGridView2.Rows[row].Cells[0].Selected == true || dataGridView2.Rows[row].Cells[1].Selected == true || dataGridView2.Rows[row].Cells[2].Selected == true || dataGridView2.Rows[row].Cells[3].Selected == true || dataGridView2.Rows[row].Cells[4].Selected == true || dataGridView2.Rows[row].Cells[5].Selected == true)
                    {
                        break;

                    }
                }
                silinecek = dataGridView2.Rows[row].Cells[0].Value.ToString(); //DataGridView1.rows[row].Cells[0].Value.ToString();

                DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "DELETE from siparisler WHERE MusteriNo='" + silinecek + "'";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    dtst.Tables["siparisler"].Clear();
                    siparisler();
                   }
            }
            catch
            { ;}
        }
        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "DELETE * from siparisler ";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    dtst.Tables["siparisler"].Clear();
                    siparisler();
                   }           
        }
        private void button12_Click(object sender, EventArgs e)
        {
            dtst.Tables["siparisler"].Clear();
            siparisler();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = 0;
            for (row = 0; row < dataGridView1.Rows.Count; row++)
            {

                if (dataGridView1.Rows[row].Cells[0].Selected == true || dataGridView1.Rows[row].Cells[1].Selected == true || dataGridView1.Rows[row].Cells[2].Selected == true || dataGridView1.Rows[row].Cells[3].Selected == true || dataGridView1.Rows[row].Cells[4].Selected == true)
                {
                    break;
                }
            }
            mno = dataGridView1.Rows[row].Cells[0].Value.ToString(); //DataGridView1.rows[row].Cells[0].Value.ToString();
            ad = dataGridView1.Rows[row].Cells[1].Value.ToString();
            soyad = dataGridView1.Rows[row].Cells[2].Value.ToString();
            adres = dataGridView1.Rows[row].Cells[4].Value.ToString();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = 0;
            for (row = 0; row < dataGridView1.Rows.Count; row++)
            {

                if (dataGridView2.Rows[row].Cells[0].Selected == true || dataGridView2.Rows[row].Cells[1].Selected == true || dataGridView2.Rows[row].Cells[2].Selected == true || dataGridView2.Rows[row].Cells[3].Selected == true || dataGridView2.Rows[row].Cells[4].Selected == true || dataGridView2.Rows[row].Cells[5].Selected == true)
                {
                    break;
                }
            }
            mno = dataGridView2.Rows[row].Cells[0].Value.ToString(); 
        }
    }
}
