using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("HareketTip")]
    public partial class HareketTip : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HareketTip()
        {
            StokHareket = new HashSet<StokHareket>();
        }

        public int? HareketYonId { get; set; }

        [StringLength(50)]
        public string HareketTipi { get; set; }

        public virtual HareketYon HareketYon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StokHareket> StokHareket { get; set; }
    }
}
