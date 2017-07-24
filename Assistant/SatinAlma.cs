namespace Assistant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SatinAlma")]
    public partial class SatinAlma
    {
        public SatinAlma()
        {
            Kullanici = Properties.Settings.Default["Kullanici"].ToString();
            KayitTarihi = DateTime.Now;
        }

        public int ID { get; set; }

        public int StokTalepID { get; set; }

        public int? StokID { get; set; }

        public decimal? Miktar { get; set; }

        public short? BirimID { get; set; }

        public decimal? BirimFiyat { get; set; }

        public byte? DovizID { get; set; }

        public short? TedarikciID { get; set; }

        public short? UreticiID { get; set; }

        [StringLength(10)]
        public string Lot { get; set; }

        [StringLength(250)]
        public string Aciklama { get; set; }

        [StringLength(50)]
        public string Kullanici { get; set; }

        public DateTime? KayitTarihi { get; set; }

        public DateTime? Guncelleme { get; set; }

        public virtual Stok Stok { get; set; }

        public virtual StokTalep StokTalep { get; set; }
    }
}
