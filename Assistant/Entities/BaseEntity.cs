using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            Kullanici = Properties.Settings.Default.Kullanici;
            KayitTarihi = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Kullanici { get; set; }

        public DateTime? KayitTarihi { get; set; }}
}
