using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
    public partial class StokFormu : DevExpress.XtraEditors.XtraForm
    {

        Assistant.AssistantEntities dbContext = new Assistant.AssistantEntities();

        public StokFormu()
        {
            InitializeComponent();
        }

        private void StokFormu_Load(object sender, EventArgs e)
        {
            dbContext.Stok.Load();
            stokBindingSource.DataSource = dbContext.Stok.Local.ToBindingList();
            BirimLookUp();
            StokGrupLookUp();
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

            gridView1.Columns["StokBirimID"].ColumnEdit = myLookup;
        }

        private void StokGrupLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "Grup",
                ValueMember = "ID"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("Grup", 0, "Grup"));
            dbContext.StokGrup.Load();
            myLookup.DataSource = dbContext.StokGrup.Local.ToBindingList();

            gridView1.Columns["StokGrupID"].ColumnEdit = myLookup;
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
                var stokId = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colID));

                var count = dbContext.StokDepo.Count(t => t.StokID == (short)stokId);

                if (count != 0)
                    MessageBox.Show(@"Seçili kayıt kullanımda olduğu için silinemez", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void StokFormu_FormClosing(object sender, FormClosingEventArgs e)
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
            DegisimLogFormu frm = new DegisimLogFormu(Name.Replace("Formu", ""), gridView1.GetFocusedRowCellValue(colID).ToString());
            frm.ShowDialog();
        }

        private void gridView1_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            BirimLookUp();
            StokGrupLookUp();

            if (!gridView1.IsNewItemRow(gridView1.FocusedRowHandle))
            {
                gridView1.SetFocusedRowCellValue(colGuncelleme, DateTime.Now);
            }
        }
    }
}
