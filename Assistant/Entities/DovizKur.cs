using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("DovizKur")]
    public class DovizKur
    {
        public int Id { get; set; }

        [Index("UX_DovizCinsTarih", 1, IsUnique = true)]
        public byte DovizCinsId { get; set; }

        [Index("UX_DovizCinsTarih", 2, IsUnique = true)]
        [Column(TypeName = "Date")]
        public DateTime Tarih { get; set; }

        public decimal Alis { get; set; }

        public decimal Satis { get; set; }

        public virtual DovizCins DovizCins { get; set; }
    }
}
