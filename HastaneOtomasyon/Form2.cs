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

    public partial class Form2 : Form
    {
        string tarih;
        string saat;
        string Alan;
        string doktorAd;

        public Form2()
        {
            InitializeComponent();
        }

        HastaneDLLEntities hdb = new HastaneDLLEntities();
        private void Form2_Load(object sender, EventArgs e)
        {
            //Select BolgeAdi, BolgeID from Bolge"
            var bolge = hdb.Bolges.ToList();
            comboBox1.DataSource = bolge;
            comboBox1.DisplayMember = "BolgeAdi";
            comboBox1.ValueMember = "BolgeID";

            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    ComboBox cbox = (ComboBox)item;
                    cbox.Text = null;
                }
            }
            monthCalendar1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Select AltBolgeAdi, AltBolgeID from AltBolge where BolgeID = @ID
            var sehir = hdb.AltBolges.Where(ab => ab.BolgeID == (int)comboBox1.SelectedValue).Select(al => new
            {

                al.AltBolgeAdi,
                al.AltBolgeID

            }).ToList();
            comboBox2.DataSource = sehir;
            comboBox2.DisplayMember = "AltBolgeAdi";
            comboBox2.ValueMember = "AltBolgeID";
            comboBox2.Text = null;
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
         
            var hastane = hdb.Hastanelers.Where(h => h.AltBolgeID == (int)comboBox2.SelectedValue).Select(h => new
            {
                h.HastaneAdi,
                h.HastaneID
            }).ToList();
            comboBox3.DataSource = hastane;
            comboBox3.DisplayMember = "HastaneAdi";
            comboBox3.ValueMember = "HastaneID";
            comboBox3.Text = null;

        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var tibbibirim = hdb.TibbiBirimlers.ToList();
            comboBox4.DataSource = tibbibirim;
            comboBox4.DisplayMember = "TibbiAdi";
            comboBox4.ValueMember = "TBID";
            Alan = comboBox4.Text;
            comboBox4.Text = null;
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Select * from vw_Doktor where  TBID  = @ID
            var doktor = hdb.vw_Doktor.Where(vd => vd.TBID == (int)comboBox4.SelectedValue).Select(d => new
            {
                d.Doktor,
                d.DoktorID
            }).ToList();

            comboBox5.DataSource = doktor;
            comboBox5.DisplayMember = "Doktor";
            comboBox5.ValueMember = "DoktorID";
            doktorAd = comboBox5.Text;
            comboBox5.Text = null;

        }

        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            monthCalendar1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = SaatGetir();
            dataGridView1.DataSource = dt;
            dataGridView1.Enabled = true;
        }
        DataTable dt2 = new DataTable();

        private DataTable SaatGetir()
        {
            dt2.Columns.Add(" ", typeof(string));
            dt2.Columns.Add("  ", typeof(string));
            dt2.Columns.Add("   ", typeof(string));
            dt2.Columns.Add("    ", typeof(string));


            dt2.Rows.Add("09:00", "09:15", "09:30", "09:45");
            dt2.Rows.Add("10:00", "10:15", "10:30", "10:45");
            dt2.Rows.Add("11:00", "11:15", "11:30", "11:45");
            dt2.Rows.Add("12:00", "12:15", "12:30", "12:45");
            dt2.Rows.Add("13:00", "13:15", "13:30", "13:45");
            dt2.Rows.Add("14:00", "14:15", "14:30", "14:45");
            dt2.Rows.Add("15:00", "15:15", "15:30", "15:45");
            dt2.Rows.Add("16:00", "16:15", "16:30", "16:45");

            return dt2;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (monthCalendar1.SelectionRange.Start > DateTime.Now)
            {

                button1.Enabled = true;
                tarih = monthCalendar1.SelectionRange.Start.ToShortDateString();

            }
            else
            {
                MessageBox.Show("Bugün tarihinden ileri bir tarih seçiniz.");
            }
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button2.Enabled = true;
            saat = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tarih + " " + saat + " tarihinde " + Alan + " bölümünden " + doktorAd + " ile randevunuz oluşturulmuştur.");


            TemizlemeMetodu();
        }

        private void TemizlemeMetodu()
        {

            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    ComboBox cbox = (ComboBox)item;
                    cbox.Text = null;
                }
                if (item is MonthCalendar)
                {
                    MonthCalendar mc = (MonthCalendar)item;
                    mc.SelectionRange.Start = DateTime.Now;
                    monthCalendar1.Enabled = false;
                }
                if (item is DataGridView)
                {
                    DataGridView dgv = (DataGridView)item;
                    dgv.Enabled = false;
                    dgv.DataSource = null;
                    dt2.Columns.Clear();
                    dt2.Rows.Clear();

                }
                if (item is Button)
                {
                    Button btn = (Button)item;
                    btn.Enabled = false;
                }
            }
        }
    }

}
