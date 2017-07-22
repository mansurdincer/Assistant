using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using StokTakip.Properties;

namespace StokTakip
{
    public partial class AnaForm : XtraForm
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            UserLookAndFeel.Default.SkinName = Settings.Default["ApplicationSkinName"].ToString();

            GirisFormu frm = new GirisFormu();
            frm.ShowDialog();

            if (Settings.Default["Kullanici"].ToString() == "Mansur" && Settings.Default["Sifre"].ToString() == "050913")
            {
                btmDurum.Caption = @"Bağlandı";
            }
            else
            {
                btmDurum.Caption = @"Giriş Yapın";
                Close();
            }
        }

        void barManager1_UnMerge(object sender, BarManagerMergeEventArgs e)
        {
            barManager1.Bars["Tools"].UnMerge();
        }
        void barManager1_Merge(object sender, BarManagerMergeEventArgs e)
        {
            var parentTools = barManager1.Bars["Tools"];
            var childTools = e.ChildManager.Bars["Tools"];
            if (childTools != null)
                parentTools.Merge(childTools);
        }

        private bool IsFormOpen(string name)
        {
            //Form activeform; activeform = new Form();
            var isOpen = false;
            foreach (var childForm in MdiChildren)
            {
                if (childForm.Text != name) continue;
                childForm.Activate();
                isOpen = true;
                break;
            }
            return isOpen;
        }

        private void btmDurum_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show(Settings.Default["Kullanici"].ToString());
        }

        private void AnaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default["ApplicationSkinName"] = UserLookAndFeel.Default.SkinName;
            Settings.Default.Save();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            DepoTurFormu frm = new DepoTurFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            DepoFormu frm = new DepoFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            BirimFormu frm = new BirimFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }
    }
}