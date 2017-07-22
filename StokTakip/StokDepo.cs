namespace StokTakip
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StokDepo")]
    public partial class StokDepo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StokDepo()
        {
            StokHareket = new HashSet<StokHareket>();
            Kullanici = Properties.Settings.Default["Kullanici"].ToString();
            KayitTarihi = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StokDepoID { get; set; }

        public int StokID { get; set; }

        public short DepoID { get; set; }

        public decimal? KritikStok { get; set; }

        public short? BirimID { get; set; }

        [StringLength(250)]
        public string Aciklama { get; set; }

        [StringLength(50)]
        public string Kullanici { get; set; }

        public DateTime? KayitTarihi { get; set; }

        public DateTime? Guncelleme { get; set; }

        public virtual Depo Depo { get; set; }

        public virtual Stok Stok { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StokHareket> StokHareket { get; set; }
    }
}
