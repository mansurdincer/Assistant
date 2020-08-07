using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Assistant.Entities;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Assistant
{
    public partial class MainForm : XtraForm
    {
        class Menu
        {
            public string Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public int MenuType { get; set; }
            public string ParentId { get; set; }
            public string Tag { get; set; }
            public int OrderBy { get; set; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        void InitMenu()
        {
            var menus = new List<Menu>();

            menus.Add(new Menu() { Id = "1", Code = "1", MenuType = 1, Name = "File", ParentId = "", Tag = "" });
            menus.Add(new Menu() { Id = "2", Code = "2", MenuType = 2, Name = "Item", ParentId = "", Tag = "" });
            menus.Add(new Menu() { Id = "3", Code = "3", MenuType = 2, Name = "Conf", ParentId = "", Tag = "" });
        }


        private void CreateMenuItem(BarAndDockingController barItem, List<Menu> menus)
        {

            foreach (var menu in menus)
            {
                if (menu.MenuType == 1)
                {
                    //BarSubItem bs = new BarSubItem(barManager1,menu.Name);
                    //barManager1.Items.Add(bs);
                }
            }

        }



        //private void MainForm_Load(object sender, EventArgs e)
        //{

        //    AssistantEntities context = new AssistantEntities();
        //    var Menu = context.Modul.ToList();


        //    BarManager barManager = new BarManager();
        //    barManager.Form = this;
        //    // Prevent excessive updates while adding and customizing bars and bar items.
        //    // The BeginUpdate must match the EndUpdate method.
        //    barManager.BeginUpdate();
        //    // Create two bars and dock them to the top of the form.
        //    // Bar1 - is a main menu, which is stretched to match the form's width.
        //    // Bar2 - is a regular bar.
        //    Bar bar1 = new Bar(barManager, "My MainMenu");
        //    Bar bar2 = new Bar(barManager, "My Bar");
        //    bar1.DockStyle = BarDockStyle.Top;
        //    bar2.DockStyle = BarDockStyle.Top;
        //    // Position the bar1 above the bar2
        //    bar1.DockRow = 0;
        //    // The bar1 must act as the main menu.
        //    barManager.MainMenu = bar1;

        //    // Create bar items for the bar1 and bar2
        //    BarSubItem subMenuTanimlamalar = new BarSubItem(barManager, "Tanımlar");
        //    BarSubItem subMenuStok = new BarSubItem(barManager, "Stok");




        //    BarButtonItem buttonOpen = new BarButtonItem(barManager, "Open");
        //    BarButtonItem buttonExit = new BarButtonItem(barManager, "Exit");
        //    BarButtonItem buttonCopy = new BarButtonItem(barManager, "Copy");
        //    BarButtonItem buttonCut = new BarButtonItem(barManager, "Cut");
        //    BarButtonItem buttonViewOutput = new BarButtonItem(barManager, "Output");




        //    subMenuTanimlamalar.AddItems(new BarItem[] { buttonOpen, buttonExit });
        //    subMenuStok.AddItems(new BarItem[] { buttonCopy, buttonCut });
        //    //subMenuView.AddItem(buttonViewOutput);

        //    //Add the sub-menus to the bar1
        //    bar1.AddItems(new BarItem[] { subMenuStok,subMenuTanimlamalar });

        //    // Add the buttonViewOutput to the bar2.
        //    bar2.AddItem(buttonViewOutput);

        //    // A handler to process clicks on bar items
        //    barManager.ItemClick += barManager_ItemClick;

        //    barManager.EndUpdate();
        //}

        //void barManager_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    BarSubItem subMenu = e.Item as BarSubItem;
        //    if (subMenu != null) return;
        //    MessageBox.Show($"Item {e.Item.Caption} has been clicked");
        //}
    }
}
