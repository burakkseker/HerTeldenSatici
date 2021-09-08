using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace her_telden_satici
{
    public partial class hesapDökümü : Form
    {
        public hesapDökümü()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font printFont = new Font("Times New Roman", 12);
            e.Graphics.DrawString(richTextBox1.Text, printFont, Brushes.Black, 0, 0);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void hesapDökümü_Load(object sender, EventArgs e)
        {

        }
    }
}
