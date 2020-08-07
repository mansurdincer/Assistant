using System;
using System.Data.Entity;
using System.Windows.Forms;
using Assistant.Classes;
using Assistant.Entities;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace Assistant.Forms
{
    public partial class StokHareketFormu : DevExpress.XtraEditors.XtraForm
    {
        private readonly int stokDepoId;
        private readonly AssistantEntities dbContext = new AssistantEntities();
        private readonly LayoutData layout = new LayoutData();

        public StokHareketFormu(int stokDepoIdParam)
        {
            stokDepoId = stokDepoIdParam;
            InitializeComponent();
        }

        private void StokHareketFormu_Load(object sender, EventArgs e)
        {
            dbContext.StokHareket.Load();
            stokHareketBindingSource.DataSource = dbContext.StokHareket.Local.ToBindingList();//.Where(p => p.StokDepoId == stokDepoId).OrderByDescending(p => p.KayitTarihi).ToList();
            gridView1.ActiveFilterString = $"StokDepoId = {stokDepoId}";

            //var getLayout = layout.GetGridLayout(Name, gridView1.Name);
            //if (getLayout != null)
            //{
            //    gridView1.RestoreLayoutFromStream(getLayout);
            //}

            HareketTipLookUp();
        }

        private void HareketTipLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "HareketTipi",
                ValueMember = "Id"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("HareketTipi", 0, "HareketTipi"));
            dbContext.HareketTip.Load();
            myLookup.DataSource = dbContext.HareketTip.Local.ToBindingList();

            colHareketTipId.ColumnEdit = myLookup;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var dlg = MessageBox.Show(@"Seçili kaydı silmek istediğinizden emin misiniz?", @"Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlg == DialogResult.Yes)
            {
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DegisimLogFormu frm = new DegisimLogFormu(Name.Replace("Formu", ""), gridView1.GetFocusedRowCellValue(colId).ToString());
            frm.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void gridView1_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colStokDepoId, stokDepoId);
        }

        private void StokHareketFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Stream str = new MemoryStream();
            //gridView1.SaveLayoutToStream(str);
            //layout.SaveGridLayout(Name, gridView1.Name, str);

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