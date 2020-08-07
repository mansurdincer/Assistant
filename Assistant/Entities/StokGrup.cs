using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("StokGrup")]
    public class StokGrup : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StokGrup()
        {
            Stok = new HashSet<Stok>();
        }

        [StringLength(50)]
        public string GrupKod { get; set; }

        [StringLength(250)]
        public string Grup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Stok> Stok { get; set; }
    }
}
