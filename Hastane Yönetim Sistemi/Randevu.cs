using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Hastane_Yönetim_Sistemi
{
    public partial class Randevu : Form
    {
        public Randevu()
        {
            InitializeComponent();
        }
        ConnectionString MyCon = new ConnectionString();
        private void fillHasta()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HAd from HastaTbl", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd", typeof(string));
            dt.Load(rdr);
            RadCb.ValueMember = "HAd";
            RadCb.DataSource = dt;
            baglanti.Close();
        }

        private void fillTedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select TAd from TedaviTbl", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TAd", typeof(string));
            dt.Load(rdr);
            RtedaviCb.ValueMember = "TAd";
            RtedaviCb.DataSource = dt;
            baglanti.Close();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Randevu_Load(object sender, EventArgs e)
        {
            fillHasta();
            fillTedavi();
            uyeler();
            Reset();
        }
        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from RandevuTbl";
            DataSet ds = Hs.ShowHasta(query);
            RandevuDgv.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            RadCb.SelectedIndex = -1;
            RtedaviCb.SelectedIndex = -1;
            Rtarih.Text = "";
            SaatCb.Text= "";
        }
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            string query = "insert into RandevuTbl values('" + RadCb.SelectedValue.ToString() + "','" + RtedaviCb.SelectedValue.ToString() + "','" + Rtarih.Text+ "','"+SaatCb.Text+"')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("Randevu Başarıyla Eklendi");
                uyeler();
                Reset();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int key = 0;
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Randevuyu Seçiniz");
            }
            else
            {
                try
                {
                    string query = "Update RandevuTbl set Hasta='" + RadCb.SelectedValue.ToString() + "',Tedavi='" + RtedaviCb.SelectedValue.ToString()+ "',RTarih='" + Rtarih.Text+ "',RSaat='"+SaatCb.Text+"' where RId=" + key + ";";
                    Hs.HastaSil(query);
                    MessageBox.Show("Randevu İşlemi Başarıyla Güncellendi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            }

        private void RandevuDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RadCb.SelectedValue = RandevuDgv.SelectedRows[0].Cells[1].Value.ToString();
            RtedaviCb.SelectedValue = RandevuDgv.SelectedRows[0].Cells[2].Value.ToString();
            Rtarih.Text = RandevuDgv.SelectedRows[0].Cells[3].Value.ToString();
            SaatCb.Text = RandevuDgv.SelectedRows[0].Cells[4].Value.ToString();

            if (RadCb.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(RandevuDgv.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Randevuyu Seçiniz");
            }
            else
            {
                try
                {
                    string query = "Delete from RandevuTbl where RId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("Randevu Başarıyla Silindi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Hide();
        }

        private void RtedaviCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RadCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
