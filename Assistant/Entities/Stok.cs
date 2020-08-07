using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("Stok")]
    public class Stok : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stok()
        {
            SatinAlma = new HashSet<SatinAlma>();
            StokDepo = new HashSet<StokDepo>();
            StokTalep = new HashSet<StokTalep>();
        }

        [StringLength(10)]
        public string StokKod { get; set; }

        [StringLength(250)]
        public string StokAd { get; set; }

        public int? StokGrupId { get; set; }

        public DateTime? Guncelleme { get; set; }

        [StringLength(250)]
        public string Aciklama { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<SatinAlma> SatinAlma { get; set; }

        public StokGrup StokGrup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<StokDepo> StokDepo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<StokTalep> StokTalep { get; set; }
    }
}
