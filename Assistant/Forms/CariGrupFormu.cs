using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Assistant.Classes;
using Assistant.Entities;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace Assistant.Forms
{
    public partial class CariGrupFormu : XtraForm
    {
        private readonly AssistantEntities dbContext = new AssistantEntities();
        private readonly LayoutData layout = new LayoutData();

        public CariGrupFormu()
        {
            InitializeComponent();
            
        }

        private void CariGrupFormu_Load(object sender, System.EventArgs e)
        {
            dbContext.CariGrup.Load();
            cariGrupBindingSource.DataSource = dbContext.CariGrup.Local.ToBindingList();

            var getLayout = layout.GetGridLayout(Name, gridView1.Name);
            if (getLayout != null)
            {
                gridView1.RestoreLayoutFromStream(getLayout);
            }}

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
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
                var id = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colId));

                var count = dbContext.Cari.Count(t => t.CariGrupId == id);

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

        private void CariGrupFormu_FormClosing(object sender, FormClosingEventArgs e)
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

        private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                view.DeleteSelectedRows();
                e.Handled = true;
            }
        }
    }
}
