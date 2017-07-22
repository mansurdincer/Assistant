using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StokTakip.Properties;

namespace StokTakip
{
    public partial class GirisFormu : XtraForm
    {
        public GirisFormu()
        {
            InitializeComponent();
        }

        private void GirisFormu_Load(object sender, EventArgs e)
        {
            txtKullanici.Text = Settings.Default["Kullanici"].ToString();
            txtSifre.Text = Settings.Default["Sifre"].ToString();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            Settings.Default["Kullanici"] = txtKullanici.Text;
            Settings.Default["Sifre"] = txtSifre.Text;
            Settings.Default.Save();
            Close();
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            Settings.Default["Kullanici"] = string.Empty;
            Settings.Default["Sifre"] = string.Empty;
            Settings.Default.Save();Application.Exit();
        }
    }
}