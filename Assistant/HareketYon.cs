namespace Assistant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HareketYon")]
    public partial class HareketYon
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public HareketYon()
        //{
        //    HareketTip = new HashSet<HareketTip>();
        //}

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ID { get; set; }

        [StringLength(50)]
        public string HareketYonu { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual HareketTip HareketTip { get; set; }
    }
}
