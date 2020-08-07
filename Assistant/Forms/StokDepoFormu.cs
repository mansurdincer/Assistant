using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Assistant.Classes;
using Assistant.Entities;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;

namespace Assistant.Forms
{
    public partial class StokDepoFormu : DevExpress.XtraEditors.XtraForm
    {
        private readonly AssistantEntities dbContext = new AssistantEntities();
        private readonly LayoutData layout = new LayoutData();

        public StokDepoFormu()
        {
            InitializeComponent();
        }

        private void StokDepoFormu_Load(object sender, EventArgs e)
        {
            dbContext.StokDepo.Load();
            stokDepoBindingSource.DataSource = dbContext.StokDepo.Local.ToBindingList();

            var getLayout = layout.GetGridLayout(Name, gridView1.Name);
            if (getLayout != null)
            {
                gridView1.RestoreLayoutFromStream(getLayout);
            }

            var getLayout2 = layout.GetGridLayout(Name, gridView2.Name);
            if (getLayout2 != null)
            {
                gridView2.RestoreLayoutFromStream(getLayout2);
            }

            BirimLookUp();
            StokLookUp();
            DepoLookUp();
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

        private void DepoLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "DepoAd",
                ValueMember = "Id"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("DepoAd", 0, "DepoAd"));
            dbContext.Depo.Load();
            myLookup.DataSource = dbContext.Depo.Local.ToBindingList();

            colDepoId.ColumnEdit = myLookup;
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

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dlg = MessageBox.Show(@"Seçili kaydı silmek istediğinizden emin misiniz?", @"Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlg == DialogResult.Yes)
            {
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void StokDepoFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (Stream str = new MemoryStream())
            {
                gridView1.SaveLayoutToStream(str);
                layout.SaveGridLayout(Name, gridView1.Name, str);
            }

            using (Stream str = new MemoryStream())
            {
                gridView2.SaveLayoutToStream(str);
                layout.SaveGridLayout(Name, gridView2.Name, str);
            }

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

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int stokDepoId = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colId));

            StokHareketFormu frm = new StokHareketFormu(stokDepoId);
            frm.ShowDialog();
            HareketleriListele();

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DegisimLogFormu frm = new DegisimLogFormu(Name.Replace("Formu", ""), gridView1.GetFocusedRowCellValue(colId).ToString());
            frm.ShowDialog();
        }

        private void gridView1_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            BirimLookUp();
            StokLookUp();
            DepoLookUp();

            if (!gridView1.IsNewItemRow(gridView1.FocusedRowHandle))
            {
                gridView1.SetFocusedRowCellValue(colGuncelleme, DateTime.Now);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            HareketleriListele();
        }

        private void HareketleriListele()
        {
            int stokDepoId = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colId));

            dbContext.StokHareket.Load();
            stokHareketBindingSource.DataSource = dbContext.StokHareket.Local.Where(p => p.StokDepoId == stokDepoId).OrderByDescending(p => p.KayitTarihi).ToList();

        }

        private void Tartim()
        {
            if (gridView1.RowCount <= 0 || gridView1.FocusedRowHandle == GridControl.AutoFilterRowHandle) return;

            var stokDepoId = gridView1.GetFocusedRowCellValue("Id");
            var stokAd = gridView1.GetFocusedRowCellValue("StokId");

            var yeni = new SeriPortOkumaFormu() { StokAd = stokAd.ToString() };
            yeni.ShowDialog();

            if (yeni.DataValue > 0)
            {
                using (var context = new AssistantEntities())
                {
                    StokHareket hareket = new StokHareket() { StokDepoId = (int)stokDepoId, Aciklama = "Tartım", HareketTipId = 1, Miktar = yeni.DataValue };
                    context.StokHareket.Add(hareket);
                    if (context.SaveChanges() > 0)
                        HareketleriListele();
                }
            }

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Tartim();
        }
    }
}