using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("StokHareket")]
    public class StokHareket : BaseEntity
    {
      
        public int HareketTipId { get; set; }

        public int StokDepoId { get; set; }
        
        [AssistantEntities.NonZero]
        public decimal? Miktar { get; set; }

        public DateTime? Guncelleme { get; set; }

        //[StringLength(250)]
        public string Aciklama { get; set; }
        
        public HareketTip HareketTip { get; set; }

        public StokDepo StokDepo { get; set; }
    }
}
