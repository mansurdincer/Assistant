using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("Depo")]
    public class Depo : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Depo()
        {
            StokDepo = new HashSet<StokDepo>();
        }

        [StringLength(150)]
        public string DepoAd { get; set; }

        public int DepoTurId { get; set; }

        public virtual DepoTur DepoTur { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<StokDepo> StokDepo { get; set; }
    }
}
