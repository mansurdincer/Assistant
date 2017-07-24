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
    public partial class StokHareketFormu : DevExpress.XtraEditors.XtraForm
    {
        Assistant.AssistantEntities dbContext = new Assistant.AssistantEntities();

        public StokHareketFormu()
        {
            InitializeComponent();
        }

        private void StokHareketFormu_Load(object sender, EventArgs e)
        {
            dbContext.StokHareket.Load();
            stokHareketBindingSource.DataSource = dbContext.StokHareket.Local.ToBindingList();
            HareketTipLookUp();
        }

        private void HareketTipLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "HareketTipi",
                ValueMember = "ID"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("HareketTipi", 0, "HareketTipi"));
            dbContext.HareketTip.Load();
            myLookup.DataSource = dbContext.HareketTip.Local.ToBindingList();

            gridView1.Columns["HareketTipID"].ColumnEdit = myLookup;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.ShowEditForm();
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
            DegisimLogFormu frm = new DegisimLogFormu(Name.Replace("Formu", ""), gridView1.GetFocusedRowCellValue(colID).ToString());
            frm.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dbContext.SaveChanges();
        }
    }
}