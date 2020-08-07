using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("StokTalep")]
    public partial class StokTalep : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StokTalep()
        {
            SatinAlma = new HashSet<SatinAlma>();
            Termin = DateTime.Now;
        }

        public int? StokId { get; set; }

        public decimal? Miktar { get; set; }

        public int? StokBirimId { get; set; }

        public int? DepartmanId { get; set; }

        public string SarfYeri { get; set; }
        public int TedarikciId { get; set; }

        public string Uretici { get; set; }

        public string Marka { get; set; }
        
        public string Lot { get; set; }

        public DateTime Termin { get; set; }

        public DateTime? Guncelleme { get; set; }

        public string Aciklama { get; set; }

        public virtual Cari Cari { get; set; }

        public virtual Departman Departman { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatinAlma> SatinAlma { get; set; }

        public virtual Stok Stok { get; set; }

        public virtual StokBirim StokBirim { get; set; }
    }
}
