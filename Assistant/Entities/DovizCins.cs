using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("DovizCins")]
    public class DovizCins
    {
        public byte Id { get; set; }
        public string Sembol { get; set; }
        public string Doviz { get; set; }
        public ICollection<DovizKur> DovizKur { get; set; }
    }
}
