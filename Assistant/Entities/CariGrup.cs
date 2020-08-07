using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("CariGrup")]
    public class CariGrup : BaseEntity
    {
        public CariGrup()
        {
            Cari = new HashSet<Cari>();
        }

        public string Grup { get; set; }
        public virtual ICollection<Cari> Cari { get; set; }
    }
}
