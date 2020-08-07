using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("Modul")]
    public partial class Modul
    {
        public Modul()
        {
            PersonelModulYetki = new HashSet<PersonelModulYetki>();
        }

        public int Id { get; set; }

        public string FormAd { get; set; }

        public bool Aktiflik { get; set; }

        public int AnaId { get; set; }

        public string MinimumVersionGereksinimi { get; set; }

        public virtual ICollection<PersonelModulYetki> PersonelModulYetki { get; set; }
    }
}
