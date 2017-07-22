namespace StokTakip
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StokHareket")]
    public partial class StokHareket
    {
        public StokHareket()
        {
            Kullanici = Properties.Settings.Default["Kullanici"].ToString();
            KayitTarihi = DateTime.Now;
        }

        [Key]
        public int HareketID { get; set; }

        public int? StokDepoID { get; set; }

        public byte? HareketYonuID { get; set; }

        public byte? HareketTipID { get; set; }

        public decimal? Miktar { get; set; }

        [StringLength(250)]
        public string Aciklama { get; set; }

        [StringLength(50)]
        public string Kullanici { get; set; }

        public DateTime? KayitTarihi { get; set; }

        public DateTime? Guncelleme { get; set; }

        public virtual HareketYon HareketYon { get; set; }

        public virtual StokDepo StokDepo { get; set; }
    }
}
