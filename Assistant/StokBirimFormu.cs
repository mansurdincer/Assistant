using System.Data.Entity;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System;
using System.Linq;

namespace Assistant
{
    public partial class StokBirimFormu : XtraForm
    {
        private readonly AssistantEntities dbContext = new AssistantEntities();
        public StokBirimFormu()
        {
            InitializeComponent();
        }

        private void BirimFormu_Load(object sender, System.EventArgs e)
        {
            dbContext.StokBirim.Load();
            stokBirimBindingSource.DataSource = dbContext.StokBirim.Local.ToBindingList();
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
                var birimId = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colID));

                var count = dbContext.Stok.Count(t => t.StokBirimID == (short)birimId);

                if (count != 0)
                    MessageBox.Show(@"Seçili kayıt kullanımda olduğu için silinemez", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void BirimFormu_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}