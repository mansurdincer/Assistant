using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("StokBirim")]
    public class StokBirim : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StokBirim()
        {
            StokTalep = new HashSet<StokTalep>();
        }

        [StringLength(50)]
        public string Birim { get; set; }

        [StringLength(50)]
        public string Sembol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StokTalep> StokTalep { get; set; }

    }
}
