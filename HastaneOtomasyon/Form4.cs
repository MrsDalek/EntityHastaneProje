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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        HastaneDLLEntities hdb = new HastaneDLLEntities();

        private void Form4_Load(object sender, EventArgs e)
        {
            buttonGuncelle.Enabled = false;
            buttonSil.Enabled = false;
        }

        private void buttonSorgu_Click(object sender, EventArgs e)
        {
            HastaGetir();
        }

        private void HastaGetir()
        {
            dataGridView1.DataSource = hdb.Hastas.Where(hd => hd.durum == true).Select(h => new
            {
                Ad = h.HastaAdi,
                Soyad = h.HastaSoyAdi,
                TCKN = h.Tc_Passaport,
                Kan = h.HastaKanGrubu,
                Boy = h.HastaBoy,
                Yas = h.HastaYas,
                Kilo = h.HastaKilo,
                Adres = h.HastaAdres,
                Tel = h.HastaTel,
                Durumu = h.durum,
                hid = h.HastaID
            }).ToList();
            dataGridView1.Columns["Durumu"].Visible = false;
            dataGridView1.Columns["hid"].Visible = false;
        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            if (textBoxAd.Text != "" && textBoxSoyad.Text != "" && textBoxTC.Text != "" && textBoxTC.Text.Length < 11 && textBoxYas.Text != "" && textBoxTel.Text != "" && textBoxKilo.Text != "" && textBoxBoy.Text != "" && comboBoxKan.Text != "" && richTextBox1.Text != "")
            {
                Hasta h = new Hasta()
                {
                    HastaAdi = textBoxAd.Text,
                    HastaSoyAdi = textBoxSoyad.Text,
                    Tc_Passaport = textBoxTC.Text,
                    HastaKanGrubu = comboBoxKan.Text,
                    HastaBoy = textBoxBoy.Text,
                    HastaYas = Convert.ToInt32(textBoxYas.Text),
                    HastaKilo = textBoxKilo.Text,
                    HastaAdres = richTextBox1.Text,
                    HastaTel = textBoxTel.Text,
                    durum = true

                };
                hdb.Hastas.Add(h);
                hdb.SaveChanges();

                MessageBox.Show("Hastta başarıyla eklendi.");
                HastaGetir();
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütefen boş alan bırakmayanız ve TCKN'yi doğru girdiğinizden emin olunuz.");
            }
        }

      
        Hasta guncellenen;


        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            if (textBoxAd.Text != "" && textBoxSoyad.Text != "" && textBoxTC.Text != "" && textBoxTC.Text.Length < 11 && textBoxYas.Text != "" && textBoxTel.Text != "" && textBoxKilo.Text != "" && textBoxBoy.Text != "" && comboBoxKan.Text != "" && richTextBox1.Text != "")
            {
                guncellenen.HastaAdi = textBoxAd.Text;
                guncellenen.HastaSoyAdi = textBoxSoyad.Text;
                guncellenen.Tc_Passaport = textBoxTC.Text;
                guncellenen.HastaKanGrubu = comboBoxKan.Text;
                guncellenen.HastaBoy = textBoxBoy.Text;
                guncellenen.HastaYas = Convert.ToInt32(textBoxYas.Text);
                guncellenen.HastaKilo = textBoxKilo.Text;
                guncellenen.HastaAdres = richTextBox1.Text;
                guncellenen.HastaTel = textBoxTel.Text;
                hdb.SaveChanges();
                HastaGetir();
                Temizle();
                buttonEkle.Enabled = true;
                buttonGuncelle.Enabled = false;
                buttonSil.Enabled = false;
            }
            else
            {
                MessageBox.Show("Lütefen boş alan bırakmayanız ve TCKN'yi doğru girdiğinizden emin olunuz.");
            }
        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            guncellenen.durum = false;
            hdb.SaveChanges();
            HastaGetir();
            Temizle();

            buttonEkle.Enabled = true;
            buttonGuncelle.Enabled = false;
            buttonSil.Enabled = false;
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
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            guncellenen = hdb.Hastas.Find(id);
            textBoxAd.Text = guncellenen.HastaAdi;
            textBoxSoyad.Text = guncellenen.HastaSoyAdi;
            textBoxTC.Text = guncellenen.Tc_Passaport;
            comboBoxKan.Text = guncellenen.HastaKanGrubu;
            textBoxBoy.Text = guncellenen.HastaBoy;
            textBoxYas.Text = guncellenen.HastaYas.ToString();
            textBoxKilo.Text = guncellenen.HastaKilo;
            richTextBox1.Text = guncellenen.HastaAdres;
            textBoxTel.Text = guncellenen.HastaTel;

            buttonEkle.Enabled = false;
            buttonSil.Enabled = true;
            buttonGuncelle.Enabled = true;

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
