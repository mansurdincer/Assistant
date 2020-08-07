using System;
using System.Data.Entity;
using System.Linq;
using Assistant.Entities;

namespace Assistant.Forms
{
    public partial class DegisimLogFormu : DevExpress.XtraEditors.XtraForm
    {
        private readonly AssistantEntities dbContext = new AssistantEntities();
        private readonly string entityName;
        private readonly string primaryKeyValue;
        
        public DegisimLogFormu(string EntityName, string PrimaryKeyValue)
        {
            entityName = EntityName;
            primaryKeyValue = PrimaryKeyValue;
            InitializeComponent();
        }

        private void DegisimLogForm_Load(object sender, EventArgs e)
        {
            dbContext.DegisimLog.Load();
            degisimLogBindingSource.DataSource = dbContext.DegisimLog.Local.Where(p => p.TabloAdi == entityName && p.AnahtarId == primaryKeyValue).OrderByDescending(p => p.KayitTarihi).ToList();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
    }
}