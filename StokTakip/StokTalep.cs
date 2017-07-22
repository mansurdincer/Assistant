namespace StokTakip
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StokTalep")]
    public partial class StokTalep
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StokTalep()
        {
            SatinAlma = new HashSet<SatinAlma>();
            Kullanici = Properties.Settings.Default["Kullanici"].ToString();
            KayitTarihi = DateTime.Now;
        }

        [Key]
        public int TalepID { get; set; }

        public int? StokID { get; set; }

        public decimal? Miktar { get; set; }

        public short? BirimID { get; set; }

        public short? SarfYeriID { get; set; }

        public short? KullanimYeriID { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatinAlma> SatinAlma { get; set; }

        public virtual Stok Stok { get; set; }
    }
}
