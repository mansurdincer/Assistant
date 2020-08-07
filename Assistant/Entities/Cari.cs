using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("Cari")]
    public class Cari : BaseEntity
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cari()
        {
            StokTalep = new HashSet<StokTalep>();
        }

        [Index(IsUnique = true)]
        public string CariKod { get; set; }
        public int CariGrupId { get; set; }
        public string FirmaAd { get; set; }
        public string Yetkili { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string InternetAdres { get; set; }
        public string Adres { get; set; }
        public string SevkAdresi { get; set; }
        public string Aciklama { get; set; }

        public virtual CariGrup CariGrup { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StokTalep> StokTalep { get; set; }
    }
}
