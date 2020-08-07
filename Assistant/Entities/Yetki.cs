using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("Yetki")]
    public partial class Yetki
    {
        public Yetki()
        {
            PersonelModulYetki = new HashSet<PersonelModulYetki>();
        }

        public int Id { get; set; }

        public string YetkiAd { get; set; }

        public virtual ICollection<PersonelModulYetki> PersonelModulYetki { get; set; }
    }
}
