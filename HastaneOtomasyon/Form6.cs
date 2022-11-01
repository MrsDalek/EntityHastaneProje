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
    public partial class Form6 : Form
    {
        public static Form6 instance;
        public string unvan;
        int tc_id = 0;
        int sifre_id = 0;
        Form5 frm5;
        public bool degisken;      
        public Form6()
        {
            InitializeComponent();

        }
        HastaneDLLEntities hdb = new HastaneDLLEntities();
        private void Form6_Load(object sender, EventArgs e)
        {
            instance = this;
            button1.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (degisken == false)
            {
            degisken = true;
            TCKNkontrol();
            SifreKontrol();
            if (tc_id == sifre_id && tc_id != 0 && sifre_id != 0)
            {
                unvan = hdb.Personellers.FirstOrDefault(pe => pe.PersonelTc == textBox1.Text).Unvan.UnvanAdi;
                if (unvan == "Doktor" || unvan == "Yonetici" || unvan == "Sekreter" || unvan == "Blm.bsk.Doktor")
                {
                    frm5 = new Form5();
                    frm5.MdiParent = this;
                    Form5.instance.unvan = unvan;
                    frm5.Show();
                }
                else
                {
                    MessageBox.Show("Giriş yetkiniz yoktur.");
                }
            }
            else
            {
                MessageBox.Show("TCKN veya şifre yanlış girildi.");
            }
            textBox1.Clear();
            textBox2.Clear();
        }
            else
            {
                MessageBox.Show("Giriş için önce çıkış yapmalısınız");
            }
        }
        private void TCKNkontrol()
        {
            var tc_VarMi = hdb.Personellers.Select(p => new
            {
                p.PersonelTc

            }).ToList();
            foreach (var item in tc_VarMi)
            {
                if (textBox1.Text == item.PersonelTc)
                {
                    tc_id = hdb.Personellers.FirstOrDefault(pe => pe.PersonelTc == textBox1.Text).PersonelID;
                    break;
                }
            }
        }
        private void SifreKontrol()
        {
            var sifre_VarMi = hdb.PersonelSifres.Select(ps => new
            {
                ps.Sifre
            }).ToList();
            foreach (var item in sifre_VarMi)
            {
                if (textBox2.Text == item.Sifre)
                {
                    sifre_id = hdb.PersonelSifres.FirstOrDefault(ps => ps.Sifre == textBox2.Text).PSifreID;
                    break;
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                button1.Enabled = true;         
        }
    }
}
