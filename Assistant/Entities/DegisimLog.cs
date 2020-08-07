using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("DegisimLog")]
    public class DegisimLog : BaseEntity
    {
        
        [StringLength(100)]
        public string TabloAdi { get; set; }

        [StringLength(100)]
        public string AlanAdi { get; set; }

        [StringLength(50)]
        public string AnahtarId { get; set; }
        
        public string EskiDegeri { get; set; }
        
        public string YeniDegeri { get; set; }
    }
}
