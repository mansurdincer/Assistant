using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Entity;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace Assistant
{
    public partial class StokDepoFormu : DevExpress.XtraEditors.XtraForm
    {
        Assistant.AssistantEntities dbContext = new Assistant.AssistantEntities();

        public StokDepoFormu()
        {
            InitializeComponent();
        }

        private void StokDepoFormu_Load(object sender, EventArgs e)
        {
            //dbContext.StokDepo.LoadAsync().ContinueWith(loadTask =>
            //{
            //    stokDepoBindingSource.DataSource = dbContext.StokDepo.Local.ToBindingList();
            //}, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());
            dbContext.StokDepo.Load();
            stokDepoBindingSource.DataSource = dbContext.StokDepo.Local.ToBindingList();

            BirimLookUp();
            StokLookUp();
            DepoLookUp();
        }

        private void BirimLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "Birim",
                ValueMember = "ID"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("Birim", 0, "Birim"));
            dbContext.StokBirim.Load();
            myLookup.DataSource = dbContext.StokBirim.Local.ToBindingList();

            gridView1.Columns["BirimID"].ColumnEdit = myLookup;
        }

        private void StokLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "StokAd",
                ValueMember = "ID"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("StokAd", 0, "StokAd"));
            dbContext.Stok.Load();
            myLookup.DataSource = dbContext.Stok.Local.ToBindingList();

            gridView1.Columns["StokID"].ColumnEdit = myLookup;
        }

        private void DepoLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "DepoAd",
                ValueMember = "ID"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("DepoAd", 0, "DepoAd"));
            dbContext.Depo.Load();
            myLookup.DataSource = dbContext.Depo.Local.ToBindingList();

            gridView1.Columns["DepoID"].ColumnEdit = myLookup;
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
            StokHareketFormu frm = new StokHareketFormu();
            frm.ShowDialog();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DegisimLogFormu frm = new DegisimLogFormu(Name.Replace("Formu", ""), gridView1.GetFocusedRowCellValue(colID).ToString());
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
    }
}