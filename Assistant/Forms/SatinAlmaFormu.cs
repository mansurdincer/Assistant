using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Entity;
using System.IO;
using Assistant.Classes;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace Assistant.Forms
{
    public partial class SatinAlmaFormu : XtraForm
    {
        private readonly Entities.AssistantEntities dbContext = new Entities.AssistantEntities();
        private readonly LayoutData layout = new LayoutData();

        public SatinAlmaFormu()
        {
            InitializeComponent();
        }

        private void SatinAlmaFormu_Load(object sender, EventArgs e)
        {
            dbContext.SatinAlma.Load();
            satinAlmaBindingSource.DataSource = dbContext.SatinAlma.Local.ToBindingList();

            var getLayout = layout.GetGridLayout(Name, gridView1.Name);
            if (getLayout != null)
            {
                gridView1.RestoreLayoutFromStream(getLayout);
            }

            BirimLookUp();
            StokLookUp();
        }

        private void BirimLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "Birim",
                ValueMember = "Id"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("Birim", 0, "Birim"));
            dbContext.StokBirim.Load();
            myLookup.DataSource = dbContext.StokBirim.Local.ToBindingList();

            colBirimId.ColumnEdit = myLookup;
        }

        private void StokLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "StokAd",
                ValueMember = "Id"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("StokAd", 0, "StokAd"));
            dbContext.Stok.Load();
            myLookup.DataSource = dbContext.Stok.Local.ToBindingList();

            colStokId.ColumnEdit = myLookup;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            gridView1.AddNewRow();
            gridView1.ShowEditForm();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dbContext.SaveChanges();

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var dlg = MessageBox.Show(@"Seçili kaydı silmek istediğinizden emin misiniz?", @"Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlg == DialogResult.Yes)
            {
                var depoTurId = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colId));

                var count = dbContext.Depo.Count(t => t.DepoTurId == depoTurId);

                if (count != 0)
                    MessageBox.Show(@"Seçili kayıt kullanımda olduğu için silinemez", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DegisimLogFormu frm = new DegisimLogFormu(Name.Replace("Formu", ""), gridView1.GetFocusedRowCellValue(colId).ToString());
            frm.ShowDialog();
        }

        private void SatinAlmaFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stream str = new MemoryStream();
            gridView1.SaveLayoutToStream(str);
            layout.SaveGridLayout(Name, gridView1.Name, str);

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
    }
}