using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    public class LayoutSetting {
        public LayoutSetting()
        {
            Kullanici = Properties.Settings.Default.Kullanici;
            KayitTarihi = DateTime.Now;
        }

        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Index("IX_Setting", 1, IsUnique = true)]
        public string Kullanici { get; set; }

        [Index("IX_Setting", 2, IsUnique = true)]
        public string FormName { get; set; }

        [Index("IX_Setting", 3, IsUnique = true)]
        public string ControlName { get; set; }
        
        public string Layout { get; set; }

        public DateTime KayitTarihi { get; set; }
    }
}
