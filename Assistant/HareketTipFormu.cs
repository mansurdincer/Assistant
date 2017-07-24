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
    public partial class HareketTipFormu : DevExpress.XtraEditors.XtraForm
    {
        Assistant.AssistantEntities dbContext = new Assistant.AssistantEntities();

        public HareketTipFormu()
        {
            InitializeComponent();
        }

        private void HareketTipFormu_Load(object sender, EventArgs e)
        {
            dbContext.HareketTip.Load();
            hareketTipBindingSource.DataSource = dbContext.HareketTip.Local.ToBindingList();
            DepoTuruLookUp();
        }

        private void DepoTuruLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "HareketYonu",
                ValueMember = "ID"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("HareketYonu", 0, "HareketYonu"));
            dbContext.HareketYon.Load();
            myLookup.DataSource = dbContext.HareketYon.Local.ToBindingList();

            gridView1.Columns["HareketYonuID"].ColumnEdit = myLookup;
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
                var hareketTipId = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colID));

                var count = dbContext.StokHareket.Count(t => t.HareketTipID == (short)hareketTipId);

                if (count != 0)
                    MessageBox.Show(@"Seçili kayıt kullanımda olduğu için silinemez", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void HareketTipFormu_FormClosing(object sender, FormClosingEventArgs e)
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

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DegisimLogFormu frm = new DegisimLogFormu(Name.Replace("Formu", ""), gridView1.GetFocusedRowCellValue(colID).ToString());
            frm.ShowDialog();
        }
    }
}