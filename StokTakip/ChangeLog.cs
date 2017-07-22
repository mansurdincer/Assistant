namespace StokTakip
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChangeLog")]
    public partial class ChangeLog
    {
        
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [StringLength(100)]
        public string EntityName { get; set; }

        [StringLength(100)]
        public string PropertyName { get; set; }

        [StringLength(50)]
        public string PrimaryKeyValue { get; set; }

        [StringLength(100)]
        public string OldValue { get; set; }

        [StringLength(100)]
        public string NewValue { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? DateChanged { get; set; }
    }
}
