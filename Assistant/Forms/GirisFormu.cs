using System;
using System.Linq;
using System.Windows.Forms;
using Assistant.Entities;
using Assistant.Properties;
using DevExpress.XtraEditors;

namespace Assistant.Forms
{
    public partial class GirisFormu : XtraForm
    {
        public GirisFormu()
        {
            InitializeComponent();
        }

        private readonly AssistantEntities context = new AssistantEntities();

        private void GirisFormu_Load(object sender, EventArgs e)
        {
            txtKullanici.Text = Settings.Default["Kullanici"].ToString();
            txtSifre.Text = Settings.Default["Sifre"].ToString();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (context.Personel.Any(x => x.Kullanici == txtKullanici.Text && x.Sifre == txtSifre.Text))
            {
                Settings.Default["Kullanici"] = txtKullanici.Text;
                Settings.Default["Sifre"] = txtSifre.Text;
                Settings.Default.Save();
                Close();
            }
            else
            {
                MessageBox.Show(@"Kullanıcı adı veya şifre hatalı.",@"Kullanıcı Bulunamadı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            //Settings.Default["Kullanici"] = string.Empty;
            //Settings.Default["Sifre"] = string.Empty;
            //Settings.Default.Save();
            Application.Exit();
        }
    }
}