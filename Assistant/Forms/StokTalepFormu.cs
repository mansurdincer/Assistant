using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Assistant.Classes;
using Assistant.Entities;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace Assistant.Forms
{
    public partial class StokTalepFormu : XtraForm
    {
        private readonly AssistantEntities dbContext = new AssistantEntities();
        private readonly LayoutData layout = new LayoutData();
        private readonly int stokId;

        public StokTalepFormu(int stokIdParam)
        {
            stokId = stokIdParam;
            InitializeComponent();
        }

        public StokTalepFormu()
        {
            InitializeComponent();
        }

        private void StokTalepFormu_Load(object sender, System.EventArgs e)
        {
            dbContext.StokTalep.Load();
            stokTalepBindingSource.DataSource = dbContext.StokTalep.Local.ToBindingList();

            var getLayout = layout.GetGridLayout(Name, gridView1.Name);
            if (getLayout != null)
            {
                gridView1.RestoreLayoutFromStream(getLayout);
            }

            DepartmanLookUp();
            BirimLookUp();
            StokLookUp();
            TedarikciLookUp();

            if (stokId == 0) return;
            gridView1.AddNewRow();
            gridView1.ShowEditForm();
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

            colStokBirimId.ColumnEdit = myLookup;
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


        private void TedarikciLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "FirmaAd",
                ValueMember = "Id"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("FirmaAd", 0, "Tedarikci"));
            dbContext.Cari.Load();
            myLookup.DataSource = dbContext.Cari.Local.ToBindingList();

            colTedarikciId.ColumnEdit = myLookup;
        }

        private void DepartmanLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "DepartmanAd",
                ValueMember = "Id"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("DepartmanAd", 0, "DepartmanAd"));
            dbContext.Departman.Load();
            myLookup.DataSource = dbContext.Departman.Local.ToBindingList();

            colDepartmanId.ColumnEdit = myLookup;
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

        private void StokTalepFormu_FormClosing(object sender, FormClosingEventArgs e)
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
