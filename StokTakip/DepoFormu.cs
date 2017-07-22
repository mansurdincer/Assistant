using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using StokTakip.Properties;

namespace StokTakip
{
    public partial class DepoFormu : XtraForm
    {
        private readonly StokDbEntities dbContext = new StokDbEntities();
        public DepoFormu()
        {
            InitializeComponent();
        }

        private void DepoFormu_Load(object sender, EventArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;

            dbContext.Depo.Load();
            depoBindingSource.DataSource = dbContext.Depo.Local.ToBindingList();

            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "DepoTuru",
                ValueMember = "DepoTuruID"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("DepoTuru", 0, "DepoTuru"));
            dbContext.DepoTur.Load();
            myLookup.DataSource = dbContext.DepoTur.Local.ToBindingList();

            gridView1.Columns["DepoTuruID"].ColumnEdit = myLookup;

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.ShowEditForm();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = MessageBox.Show(@"Seçili kaydı silmek istediğinizden emin misiniz?", @"Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlg == DialogResult.Yes)
            {
                var depoId = Convert.ToInt32(gridView1.GetFocusedRowCellValue("DepoID"));

                var count = dbContext.StokDepo.Count(t => t.DepoID == (short)depoId);

                if (count != 0)
                    MessageBox.Show(@"Seçili kayıt kullanımda olduğu için silinemez", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void DepoFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbContext.ChangeTracker.HasChanges())
            {
                var dlg = MessageBox.Show(Text + @"'nda yaptığınız değişiklikleri kaydetmek istiyor musunuz?", @"Kayıt", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dlg == DialogResult.Yes)
                {
                    dbContext.SaveChanges();
                    e.Cancel = false;
                }
                else if (dlg == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }

            }
            else
            {
                e.Cancel = false;
            }
        }

        private void gridView1_EditFormShowing(object sender, DevExpress.XtraGrid.Views.Grid.EditFormShowingEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colKullanici, Settings.Default["Kullanici"]);
            gridView1.SetFocusedRowCellValue(colKayitTarihi, DateTime.Now);
        }
    }
}