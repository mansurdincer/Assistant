using System;
using System.Data;
using System.Data.Entity;
using System.Windows.Forms;
using Assistant.Classes;
using Assistant.Entities;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace Assistant.Forms
{
    public partial class DovizKurFormu : XtraForm
    {
        private readonly AssistantEntities dbContext = new AssistantEntities();


        public DovizKurFormu()
        {
            InitializeComponent();
        }

        private readonly Helper helper = new Helper();
        DataTable dtDovizKur = new DataTable();

        private void DovizKurFormu_Load(object sender, EventArgs e)
        {
            dtDovizKur = helper.TcmbKurGetir(DateTime.Now);
            gridControl1.DataSource = dtDovizKur;

            dbContext.DovizKur.Load();
            dovizKurBindingSource.DataSource = dbContext.DovizKur.Local.ToBindingList();
            DovizCinsLookUp();
        }

        private void DovizCinsLookUp()
        {
            var myLookup = new RepositoryItemLookUpEdit
            {
                DisplayMember = "Doviz",
                ValueMember = "Id"
            };

            myLookup.Columns.Add(new LookUpColumnInfo("Doviz", 0, "Doviz"));
            dbContext.DovizCins.Load();
            myLookup.DataSource = dbContext.DovizCins.Local.ToBindingList();

            colDovizCinsId.ColumnEdit = myLookup;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            dtDovizKur = helper.TcmbKurGetir((DateTime)barEditItemTarih.EditValue);
            gridControl1.DataSource = dtDovizKur;
        }

        private void barEditItemTarih_EditValueChanged(object sender, EventArgs e)
        {
            dtDovizKur = helper.TcmbKurGetir((DateTime)barEditItemTarih.EditValue);
            gridControl1.DataSource = dtDovizKur;
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            //using (var context = new AssistantEntities())
            {
                for (var i = 0; i < dtDovizKur.Rows.Count; i++)
                {
                    byte dovizId = 0;

                    switch (dtDovizKur.Rows[i]["Doviz"].ToString())
                    {
                        case "USD": dovizId = 2; break;
                        case "EUR": dovizId = 3; break;
                        case "GBP": dovizId = 4; break;
                    }

                    dbContext.DovizKur.Add(new DovizKur
                    {
                        Tarih = Convert.ToDateTime(dtDovizKur.Rows[i]["Tarih"]),
                        DovizCinsId = dovizId,
                        Alis = Convert.ToDecimal(dtDovizKur.Rows[i]["Alis"].ToString().Replace(".", ",")),
                        Satis = Convert.ToDecimal(dtDovizKur.Rows[i]["Satis"].ToString().Replace(".", ","))
                    });
                }

                dbContext.SaveChanges();
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = MessageBox.Show(@"Seçili kaydı silmek istediğinizden emin misiniz?", @"Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlg == DialogResult.Yes)
            {
                gridView2.DeleteRow(gridView2.FocusedRowHandle);
            }
            dbContext.SaveChanges();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show(helper.PariteHesapla(3, 2).ToString("0.##"), @"Parite");
        }
    }
}