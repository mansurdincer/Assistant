using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("HareketYon")]
    public partial class HareketYon : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HareketYon()
        {
            HareketTip = new HashSet<HareketTip>();
        }

        [StringLength(50)]
        public string HareketYonu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HareketTip> HareketTip { get; set; }
    }
}
