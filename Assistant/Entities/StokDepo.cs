using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("StokDepo")]
    public class StokDepo : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StokDepo()
        {
            StokHareket = new HashSet<StokHareket>();
        }

        [Index("UX_StokIdDepoId", 1, IsUnique = true)]
        public int StokId { get; set; }

        [Index("UX_StokIdDepoId", 2, IsUnique = true)]
        public int DepoId { get; set; }

        public decimal? KritikStok { get; set; }

        public int? BirimId { get; set; }

        public DateTime? Guncelleme { get; set; }
        
        public string Aciklama { get; set; }

        public virtual Depo Depo { get; set; }

        public virtual Stok Stok { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StokHareket> StokHareket { get; set; }
    }
}
