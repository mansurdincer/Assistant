namespace Assistant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepoTur")]
    public partial class DepoTur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepoTur()
        {
            Depo = new HashSet<Depo>();
            Kullanici = Properties.Settings.Default["Kullanici"].ToString();
            KayitTarihi = DateTime.Now;
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short ID { get; set; }

        [StringLength(50)]
        public string DepoTuru { get; set; }

        [StringLength(50)]
        public string Kullanici { get; set; }

        public DateTime? KayitTarihi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Depo> Depo { get; set; }
    }
}
