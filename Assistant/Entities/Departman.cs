using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("Departman")]
    public class Departman : BaseEntity
    {
        public Departman()
        {
            StokTalep = new HashSet<StokTalep>();
        }
        public string DepartmanAd { get; set; }

        public virtual ICollection<Personel> Personel { get; set; }
        public virtual ICollection<StokTalep> StokTalep { get; set; }
    }
}