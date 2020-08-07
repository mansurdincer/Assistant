using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Entity;
using System.IO;
using Assistant.Classes;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace Assistant.Forms
{
    public partial class PersonelFormu : XtraForm
    {
        private readonly Entities.AssistantEntities dbContext = new Entities.AssistantEntities();
        private readonly LayoutData layout = new LayoutData();


        public PersonelFormu()
        {
            InitializeComponent();
        }

        private void PersonelFormu_Load(object sender, EventArgs e)
        {
            dbContext.Personel.Load();
            personelBindingSource.DataSource = dbContext.Personel.Local.ToBindingList();
            DepartmanLookUp();

            var getLayout = layout.GetGridLayout(Name, gridView1.Name);
            if (getLayout != null)
            {
                gridView1.RestoreLayoutFromStream(getLayout);
            }
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
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DegisimLogFormu frm = new DegisimLogFormu(Name.Replace("Formu", ""), gridView1.GetFocusedRowCellValue(colId).ToString());
            frm.ShowDialog();
        }

        private void PersonelFormu_FormClosing(object sender, FormClosingEventArgs e)
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