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
    public partial class Form1 : Form
    {
        public müsteriKayit form2;
        public stoklar form3;
        public siparisAl form4;
        public müsteriDüzenle form5;
        public satislar form6; 
        public Form1()
        {
            InitializeComponent();
            form2 = new müsteriKayit();
            form3 = new stoklar();
            form4 = new siparisAl();
            form5 = new müsteriDüzenle();
            form6 = new satislar();
            form2.form1 = this;
            form3.form1 = this;
            form4.form1 = this;
            form5.form1 = this;
            form6.form1 = this;
        }
        public OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=veri.mdb");
        public OleDbCommand komut = new OleDbCommand();
        public OleDbDataAdapter adp = new OleDbDataAdapter();
        public DataSet dtst = new DataSet();       
        public string sil;
        public string mno,ad,soyad,adres;
        public int satir = 0;
        public void müsteriListele()
        {
            bag.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select MusteriNo,Ad,Soyad,Telefon,Adres,UrunTercihi,Bolge From MusteriBilgi ", bag);
            adp.Fill(dtst, "MusteriBilgi");            
            dataGridView1.DataSource = dtst.Tables["MusteriBilgi"];
            adp.Dispose();
            bag.Close();
        }
       
        public void stokGoster()
        {
            bag.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * From StokBilgi ", bag);
            adp.Fill(dtst, "StokBilgi");            
            form3.dataGridView1.DataSource = dtst.Tables["StokBilgi"];
            adp.Dispose();
            bag.Close();
        }
        public void urunIsım()
        {
            bag.Open();
            komut.Connection = bag;
            komut.CommandText = "Select * from StokBilgi";
            OleDbDataReader oku;
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                form2.comboBox1.Items.Add(oku[0].ToString());
                form4.comboBox1.Items.Add(oku[0].ToString());
                form4.comboBox2.Items.Add(oku[1].ToString());
            }
            bag.Close();
            oku.Dispose();
        }
       
       
       
        private void Form1_Load(object sender, EventArgs e)
        {            
            müsteriListele();            
            dataGridView1.Columns[1].HeaderText = "Adı";
            dataGridView1.Columns[2].HeaderText = "Soyadı";
            dataGridView1.Columns[3].HeaderText = "Telefon";
            dataGridView1.Columns[4].HeaderText = "Adres";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 200;            
                       
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
        
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
                sil = dataGridView1.Rows[row].Cells[0].Value.ToString(); 

                DialogResult cevap;
                cevap = MessageBox.Show("Silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    bag.Open();
                    komut.Connection = bag;
                    komut.CommandText = "DELETE from MusteriBilgi WHERE MusteriNo='" + sil + "'";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    bag.Close();
                    dtst.Clear();
                    dtst.Tables["MusteriBilgi"].Clear();
                    müsteriListele();
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
            mno = dataGridView1.Rows[satir].Cells[0].Value.ToString(); 

            form5.ShowDialog();           
        }     

        private void Button3_Click(object sender, EventArgs e)
        {
            form6.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            form3.ShowDialog();            
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("select * From MusteriBilgi", bag);
            if (TextBox1.Text == "")
            {
                komut.Connection = bag;
                komut.CommandText = "Select * from MusteriBilgi";
                adp.SelectCommand = komut;
                adp.Fill(dtst, "MusteriBilgi");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adp.SelectCommand.CommandText = " Select * From MusteriBilgi" +
                 " where(MusteriNo like '%" + TextBox1.Text + "%' )";
            dtst.Clear();
            adp.Fill(dtst, "MusteriBilgi");
            bag.Close();        
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("select * From MusteriBilgi", bag);
            if (TextBox2.Text == "")
            {
                komut.Connection = bag;
                komut.CommandText = "Select * from MusteriBilgi";
                adp.SelectCommand = komut;
                adp.Fill(dtst, "MusteriBilgi");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adp.SelectCommand.CommandText = " Select * From MusteriBilgi" +
                 " where(Ad like '%" + TextBox2.Text + "%' )";
            dtst.Clear();
            adp.Fill(dtst, "MusteriBilgi");
            bag.Close();   
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("select * From MusteriBilgi", bag);
            if (textBox3.Text == "")
            {
                komut.Connection = bag;
                komut.CommandText = "Select * from MusteriBilgi";
                adp.SelectCommand = komut;
                adp.Fill(dtst, "MusteriBilgi");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adp.SelectCommand.CommandText = " Select * From MusteriBilgi" +
                 " where(Soyad like '%" + textBox3.Text + "%' )";
            dtst.Clear();
            adp.Fill(dtst, "MusteriBilgi");
            bag.Close();   
        }

        private void hesapDökümüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hesapDökümü hesap = new hesapDökümü();
            hesap.ShowDialog();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hakkinda hakkinda1 = new hakkinda();
            hakkinda1.ShowDialog();
        }

        private void fişiTakmakİçinTıklayınızToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        

        private void fişiTakmakİçinTıklayınızToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string yazi = label5.Text;
            int adet = yazi.Length;
            string ilk, sonrasi;
            ilk = yazi.Substring(0, 1);
            sonrasi = yazi.Substring(1, adet - 1);
            label5.Text = sonrasi + ilk;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void tabelaFişiniÇıkartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason==CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(3000);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Müşteri Kaydı İçin Tıklayınız.", Button1);
        }

        private void Button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Müşteri Silmek İçin Tıklayınız.",Button2);
        }

        private void Button5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Müşteri Düzenlemek İçin Tıklayınız.",Button5);
        }

        private void Button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Kasayı Açmak İçin Tıklayınız.",Button3);
        }

        private void Button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Stokları Görmek İçin Tıklayınız.",Button4);
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Sipariş Almak İçin Tıklayınız.",button6);
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Çıkış Yapmak İçin Tıklayınız.",button9);
        }

        private void TextBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Lütfen Müşteri Numarası Giriniz.",TextBox1);
        }

        private void TextBox2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Lütfen Müşteri Adı Giriniz.",TextBox2);
        }

        private void textBox3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Lütfen Müşteri Soyadı Giriniz.",textBox3);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToString();
        }

        private void iletişimToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void okulİletişimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Okuliletisim Iletisim = new Okuliletisim();
            Iletisim.ShowDialog();
        }

        private void kişiselİletişimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kisiseliletisim Kisisel = new Kisiseliletisim();
            Kisisel.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {            
            form4.ShowDialog();          

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            mno = dataGridView1.Rows[row].Cells[0].Value.ToString(); 
            ad = dataGridView1.Rows[row].Cells[1].Value.ToString();
            soyad = dataGridView1.Rows[row].Cells[2].Value.ToString();
            adres = dataGridView1.Rows[row].Cells[4].Value.ToString();
        }
        

        
    }
}
