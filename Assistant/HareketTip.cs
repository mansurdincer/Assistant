using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant
{
    [Table("HareketTip")]
    public partial class HareketTip
    {
        public HareketTip()
        {          
            Kullanici = Properties.Settings.Default["Kullanici"].ToString();
            KayitTarihi = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ID { get; set; }

        public byte? HareketYonID { get; set; }

        [StringLength(50)]
        public string HareketTipi { get; set; }

        [StringLength(50)]
        public string Kullanici { get; set; }

        public DateTime? KayitTarihi { get; set; }

        public virtual HareketYon HareketYon { get; set; }
    }
}
