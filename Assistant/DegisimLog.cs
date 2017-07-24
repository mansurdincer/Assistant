namespace Assistant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DegisimLog")]
    public partial class DegisimLog
    {
        
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [StringLength(100)]
        public string TabloAdi { get; set; }

        [StringLength(100)]
        public string AlanAdi { get; set; }

        [StringLength(50)]
        public string AnahtarID { get; set; }

        [StringLength(100)]
        public string EskiDegeri { get; set; }

        [StringLength(100)]
        public string YeniDegeri { get; set; }

        [StringLength(50)]
        public string Kullanici { get; set; }

        public DateTime DegisimTarihi { get; set; }
    }
}
