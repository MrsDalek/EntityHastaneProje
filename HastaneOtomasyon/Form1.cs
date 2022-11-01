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
    public partial class Form1 : Form
    {
        String tc;
        int id;
        string getpassword ;
        string password;
        public Form1()
        {
            InitializeComponent();
        }
        HastaneDLLEntities hdb = new HastaneDLLEntities();
        private void Form1_Load(object sender, EventArgs e)
        {
          
            var unvan1 = hdb.Unvans.ToList();
            comboBoxUnvan.DataSource = unvan1;
            comboBoxUnvan.DisplayMember = "UnvanAdi";
            comboBoxUnvan.ValueMember = "UnvanID";

            var hast = hdb.Hastanelers.ToList();
            comboBoxHastane.DataSource = hast;
            comboBoxHastane.DisplayMember = "HastaneAdi";
            comboBoxHastane.ValueMember = "HastaneID";

            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    ComboBox cbox = (ComboBox)item;
                    cbox.Text = "";
                }
            }
            button_guncelle.Enabled = false;
            button_Sil.Enabled = false;
        }

        private void button_goster_Click(object sender, EventArgs e)
        {
            PersonelleriGetir();
        }

        private void PersonelleriGetir()
        {
            //select PersonelAdi,PersonelSoyad,HastaneAdi,UnvanAdi,PersonelYas,PersonelCinsiyet,PersonelTel,PersonelMail,PersonelAdres,PersonelTc,PersonelID from Personeller,PersonelDetay,Unvan,Hastaneler where PersonelID = PDID and Personeller.UnvanID = Unvan.UnvanID  and Hastaneler.HastaneID = Personeller.HastaneID and Personeller.durum = 1
           
            dataGridView1.DataSource = hdb.Personellers.Where(pe=> pe.Durum == true).Select(p => new
            {
                Ad = p.PersonelAdi,
                Soyad = p.PersonelSoyad,
                TCKN = p.PersonelTc,
                HastAd = p.Hastaneler.HastaneAdi,
                Unvani = p.Unvan.UnvanAdi,
                Yas = p.PersonelDetay.PersonelYas,
                Cinsi = p.PersonelDetay.PersonelCinsiyet,
                Tel = p.PersonelDetay.PersonelTel,
                Mail = p.PersonelDetay.PersonelMail,
                Adres = p.PersonelDetay.PersonelAdres,
                durum = p.Durum,
                pid = p.PersonelID
            }).ToList();
            dataGridView1.Columns["durum"].Visible = false;
            dataGridView1.Columns["pid"].Visible = false;


        }

     
        private void button_Kayit_Click(object sender, EventArgs e)
        {
          
            getpassword = RandomPassword();
       ;
            if (textBoxAd.Text != "" && textBoxSoyad.Text != "" && textBoxYas.Text != "" && textBoxTC.Text != "" && textBoxTC.Text.Length == 11 && textBoxTel.Text != "" && textBoxMail.Text != "" && comboBoxHastane.Text != "" && comboBoxUnvan.Text != "" && richTextBox1.Text != "")
              {
                Personeller p = new Personeller()
                  {
                      PersonelAdi = textBoxAd.Text,
                      PersonelSoyad = textBoxSoyad.Text,
                      HastaneID = comboBoxHastane.SelectedIndex,
                      UnvanID = comboBoxUnvan.SelectedIndex,
                      PersonelTc = textBoxTC.Text,
                      Durum = true
                  };
                  hdb.Personellers.Add(p);
                  hdb.SaveChanges();
                  tc = textBoxTC.Text;
                  id = hdb.Personellers.FirstOrDefault(pe =>pe.PersonelTc == tc).PersonelID;

                  PersonelSifre ps = new PersonelSifre()
                  {
                      PSifreID = id,
                      Sifre = getpassword,
                      Durum = true
                  };
                  hdb.PersonelSifres.Add(ps);
                  hdb.SaveChanges();


                  PersonelDetay pd = new PersonelDetay()
                  {
                      PDID = id,
                      PersonelYas = Convert.ToInt32(textBoxYas.Text),
                      PersonelCinsiyet = textBoxCins.Text,
                      PersonelTel = textBoxTel.Text,
                      PersonelMail = textBoxMail.Text,
                      PersonelAdres = richTextBox1.Text
                  };
                  hdb.PersonelDetays.Add(pd);
                  hdb.SaveChanges();


                  MessageBox.Show("Çalışan başarıyla eklendi.");
                  PersonelleriGetir();
                  Temizle();
              }
              else
              {
                  MessageBox.Show("Lütefen boş alan bırakmayınız ve TCKN'yi doğru girdiğinizden emin olunuz.");
              }
        }

        private string RandomPassword()
        {

            password=null;
            Random rndLenght = new Random();
            int passwordlenght = rndLenght.Next(8, 11);
          
            Random rndbool = new Random();
            Random rndLetter = new Random();
            Random rndNumber = new Random();
            int numberORletter;

            for (int i = 0; i < passwordlenght; i++)
            {
                numberORletter = rndbool.Next(0, 3);
            
                if (numberORletter == 0)
                {                
                    int number = rndNumber.Next(0, 10);    
                    password = password + number.ToString();
                }
                else if (numberORletter == 1)
                {               
                    int letterBigASCII = rndLetter.Next(65, 91);            
                    password = password + Convert.ToChar(letterBigASCII) ;              
                }
                else if(numberORletter == 2)
                {                   
                    int letterLittleASCII = rndLetter.Next(97, 123);
                    password = password + Convert.ToChar(letterLittleASCII);
                }
            }
            SifreKontrol(password);      
            return password;

        }
        private void SifreKontrol(string password)
        {
            var sifre_VarMi = hdb.PersonelSifres.Select(ps => new
            {
                ps.Sifre
            });
            foreach (var item in sifre_VarMi)
            {
                if (password == item.Sifre)
                {
                    RandomPassword();        
                }
            }
        }

        Personeller  guncellenen;
 
        private void button_guncelle_Click(object sender, EventArgs e)
        {
            if (textBoxAd.Text != "" && textBoxSoyad.Text != "" && textBoxYas.Text != "" && textBoxTC.Text != "" && textBoxTC.Text.Length == 11 && textBoxTel.Text != "" && textBoxMail.Text != "" && comboBoxHastane.Text != "" && comboBoxUnvan.Text != "" && richTextBox1.Text != "")
            {
                guncellenen.PersonelAdi = textBoxAd.Text;
                guncellenen.PersonelSoyad = textBoxSoyad.Text;
                guncellenen.PersonelTc = textBoxTC.Text;
                guncellenen.Hastaneler.HastaneAdi = comboBoxHastane.Text;
                guncellenen.Unvan.UnvanAdi = comboBoxUnvan.Text;
                guncellenen.PersonelDetay.PersonelYas = Convert.ToInt32(textBoxYas.Text);
                guncellenen.PersonelDetay.PersonelCinsiyet = textBoxCins.Text;
                guncellenen.PersonelDetay.PersonelTel = textBoxTel.Text;
                guncellenen.PersonelDetay.PersonelMail = textBoxMail.Text;
                guncellenen.PersonelDetay.PersonelAdres = richTextBox1.Text;
                hdb.SaveChanges();
                PersonelleriGetir();
                Temizle();
                button_Kayit.Enabled = true;
                button_guncelle.Enabled = false;
                button_Sil.Enabled = false;
            }
            else
            {          
                    MessageBox.Show("Lütefen boş alan bırakmayınız ve TCKN'yi doğru girdiğinizden emin olunuz.");       
            }
        }

        private void button_Sil_Click(object sender, EventArgs e)
        {
            guncellenen.Durum = false;
            hdb.SaveChanges();
            PersonelleriGetir();
            Temizle();
            button_Kayit.Enabled = true;
            button_guncelle.Enabled = false;
            button_Sil.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id;
                List<Hasta> lp = new List<Hasta>();
                id = (hdb.Hastas.FirstOrDefault(pe => pe.HastaAdi == textBoxAra.Text).HastaID);
                lp.Add(hdb.Hastas.Find(id));
                dataGridView1.DataSource = lp;
            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı İsim girişi."); ;
            }
            Temizle();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
            guncellenen = hdb.Personellers.Find(id);
            textBoxAd.Text = guncellenen.PersonelAdi;
            textBoxSoyad.Text = guncellenen.PersonelSoyad;
            textBoxTC.Text = guncellenen.PersonelTc;
            comboBoxHastane.Text = guncellenen.Hastaneler.HastaneAdi;
            comboBoxUnvan.Text = guncellenen.Unvan.UnvanAdi;
            textBoxYas.Text = guncellenen.PersonelDetay.PersonelYas.ToString();
            textBoxCins.Text = guncellenen.PersonelDetay.PersonelCinsiyet;
            textBoxTel.Text = guncellenen.PersonelDetay.PersonelTel;
            textBoxMail.Text = guncellenen.PersonelDetay.PersonelMail;
            richTextBox1.Text = guncellenen.PersonelDetay.PersonelAdres;

            button_Kayit.Enabled = false;
            button_Sil.Enabled = true;
            button_guncelle.Enabled = true;
        }
        private void Temizle()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = (TextBox)item;
                    txt.Clear();
                }
                if (item is ComboBox)
                {
                    ComboBox cb = (ComboBox)item;
                    cb.Text = "";
                }
                if (item is RichTextBox)
                {
                    RichTextBox rtxt = (RichTextBox)item;
                    rtxt.Clear();
                }
            }
        }
    }
}
