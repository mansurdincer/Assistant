using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Windows.Forms;
using Assistant.Entities;
using Assistant.Properties;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Assistant.Forms
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
            StokBirimFormu frm = new StokBirimFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            StokGrupFormu frm = new StokGrupFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            StokFormu frm = new StokFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            StokDepoFormu frm = new StokDepoFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            HareketTipFormu frm = new HareketTipFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private static bool CheckDatabaseExists()
        {
            SqlConnection connectionString = new SqlConnection("server=.\\SQLEXPRESS;Trusted_Connection=True");

            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                using (var command = new SqlCommand($"SELECT db_id('Assistant')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            StokTalepFormu frm = new StokTalepFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (Settings.Default["YedekKonumu"] == null)
            {
                MessageBox.Show(@"Yedekleme için C: sürücüsü dışında bir konum belirleyin.", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                folder.SelectedPath = Settings.Default["YedekKonumu"].ToString();
            }

            DialogResult result = folder.ShowDialog();
            if (result == DialogResult.OK)
            {
                Settings.Default["YedekKonumu"] = folder.SelectedPath;

                string backupDb = $@"{folder.SelectedPath}\AssistantYedek({DateTime.Now.ToString("yyyy-M-d-HH-mm-ss")}).bak";
                string databaseName = "Assistant"; // This is not the MDF file, but the logical database name

                using (var db = new AssistantEntities())
                {
                    var parms = new object[2];
                    parms[0] = databaseName;
                    parms[1] = backupDb;

                    var cmd = $"BACKUP DATABASE {databaseName} TO DISK=\'{backupDb}\' WITH FORMAT;";

                    try
                    {
                        db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd, parms);

                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, @"Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Settings.Default["YedekKonumu"] = null;
                    }

                }
            }
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            CariGrupFormu frm = new CariGrupFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            CariFormu frm = new CariFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            SatinAlmaFormu frm = new SatinAlmaFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();}

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            AssistantEntities context = new AssistantEntities();

            var connectionString = context.Database.Connection.ConnectionString;

            Database.Delete(connectionString);

        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            DovizKurFormu frm = new DovizKurFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraForm1 frm = new XtraForm1();
            frm.ShowDialog();
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            PersonelFormu frm = new PersonelFormu();
            if (IsFormOpen(frm.Name)) return;
            frm.MdiParent = this;
            frm.Show();
        }
    }
}