using System.IO;
using System.Linq;
using System.Text;
using Assistant.Entities;
using Assistant.Properties;

namespace Assistant.Classes
{
    public class LayoutData
    {
        private readonly AssistantEntities context = new AssistantEntities();
        private readonly string kullanici = Settings.Default["Kullanici"].ToString();

        public void SaveGridLayout(string activeFormName, string controlName, Stream str)
        {
            str.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(str);
            var text = reader.ReadToEnd();

            using (var db = new AssistantEntities())
            {
                var result = db.LayoutSetting.SingleOrDefault(p => p.FormName == activeFormName && p.ControlName == controlName && p.Kullanici == kullanici);
                if (result != null)
                {
                    result.Layout = text;
                }
                else
                {
                    LayoutSetting layoutSetting = new LayoutSetting { FormName = activeFormName, ControlName = controlName, Layout = text, Kullanici = kullanici };
                    db.LayoutSetting.Add(layoutSetting);
                }

                db.SaveChanges();
            }
        }

        public MemoryStream GetGridLayout(string activeFormName, string controlName)
        {
            var setting = context.LayoutSetting.FirstOrDefault(p => p.FormName == activeFormName && p.ControlName == controlName && p.Kullanici == kullanici);
            if (setting == null) return null;
            var text = setting.Layout;
            var byteArray = Encoding.ASCII.GetBytes(text);
            var stream = new MemoryStream(byteArray);
            //GridView.RestoreLayoutFromStream(stream);
            return stream;
        }
    }
}
