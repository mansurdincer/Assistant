namespace Assistant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stok")]
    public partial class Stok
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stok()
        {
            SatinAlma = new HashSet<SatinAlma>();
            StokDepo = new HashSet<StokDepo>();
            StokTalep = new HashSet<StokTalep>();
            Kullanici = Properties.Settings.Default["Kullanici"].ToString();
            KayitTarihi = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(10)]
        public string StokKod { get; set; }

        [StringLength(250)]
        public string StokAd { get; set; }

        public short? StokBirimID { get; set; }

        public short? StokGrupID { get; set; }

        [StringLength(250)]
        public string Aciklama { get; set; }

        [StringLength(50)]
        public string Kullanici { get; set; }

        public DateTime? KayitTarihi { get; set; }

        public DateTime? Guncelleme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatinAlma> SatinAlma { get; set; }

        public virtual StokBirim StokBirim { get; set; }

        public virtual StokGrup StokGrup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StokDepo> StokDepo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StokTalep> StokTalep { get; set; }
    }
}
