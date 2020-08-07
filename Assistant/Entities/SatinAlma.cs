using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("SatinAlma")]
    public class SatinAlma : BaseEntity
    {

        public int? StokTalepId { get; set; }

        public int StokId { get; set; }

        public decimal Miktar { get; set; }

        public int BirimId { get; set; }

        public decimal BirimFiyat { get; set; }

        public int DovizId { get; set; }

        public int TedarikciId { get; set; }

        public string Uretici { get; set; }

        [StringLength(10)]
        public string Lot { get; set; }
        
        public DateTime? Guncelleme { get; set; }

        public string Aciklama { get; set; }

        public virtual Stok Stok { get; set; }

        public virtual StokTalep StokTalep { get; set; }

    }
}
