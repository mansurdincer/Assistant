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

namespace Assistant
{
    public partial class DegisimLogFormu : DevExpress.XtraEditors.XtraForm
    {
        AssistantEntities dbContext = new AssistantEntities();
        private string entityName;
        private string primaryKeyValue;


        public DegisimLogFormu(string EntityName, string PrimaryKeyValue)
        {
            entityName = EntityName;
            primaryKeyValue = PrimaryKeyValue;
            InitializeComponent();
        }

        private void DegisimLogForm_Load(object sender, EventArgs e)
        {
            dbContext.DegisimLog.Load();
            degisimLogBindingSource.DataSource = dbContext.DegisimLog.Local.Where(p => p.TabloAdi == entityName && p.AnahtarID == primaryKeyValue).OrderByDescending(p => p.DegisimTarihi).ToList();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
    }
}