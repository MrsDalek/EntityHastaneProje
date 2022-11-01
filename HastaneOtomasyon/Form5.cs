using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneOtomasyon
{
    public partial class Form5 : Form
    {
        public static Form5 instance;
        public string unvan ;
      
        public Form5()
        {
            InitializeComponent();
            instance = this;

        }
 
        private void Form5_Load(object sender, EventArgs e)
        {
            
          if(unvan == "Doktor" || unvan=="Blm.bsk.Doktor")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;

            }
          else if (unvan == "Yonetici")
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
          else if (unvan=="Sekreter")
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
                frm1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {          
            Form5.instance.Close();
            Form6.instance.degisken = false;
        }   
    }
}
