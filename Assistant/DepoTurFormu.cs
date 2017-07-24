using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Assistant
{
    public partial class DepoTurFormu : XtraForm
    {
        private readonly AssistantEntities dbContext = new AssistantEntities();

        public DepoTurFormu()
        {
            InitializeComponent();
        }

        private void DepoTurFormu_Load(object sender, EventArgs e)
        {
            dbContext.DepoTur.Load();
            depoTurBindingSource.DataSource = dbContext.DepoTur.Local.ToBindingList();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.ShowEditForm();
        }
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = MessageBox.Show(@"Seçili kaydı silmek istediğinizden emin misiniz?", @"Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlg == DialogResult.Yes)
            {
                var depoTurId = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colID));

                var count = dbContext.Depo.Count(t => t.DepoTurID == (short)depoTurId);

                if (count != 0)
                    MessageBox.Show(@"Seçili kayıt kullanımda olduğu için silinemez", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void DepoTurFormu_FormClosing(object sender, FormClosingEventArgs e)
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

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            DegisimLogFormu frm = new DegisimLogFormu(Name.Replace("Formu", ""), gridView1.GetFocusedRowCellValue(colID).ToString());
            frm.ShowDialog();
        }
    }
}