using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("Personel")]
    public class Personel
    {
        public Personel()
        {
            PersonelModulYetki = new HashSet<PersonelModulYetki>();
            KayitTarihi = DateTime.Now;
        }

        public int Id { get; set; }

        public string Kullanici { get; set; }

        public string Sifre { get; set; }

        public int DepartmanId { get; set; }

        public string AdSoyad { get; set; }

        public string EmailAdres { get; set; }

        public string DahiliNo { get; set; }

        public bool Aktiflik { get; set; }

        public DateTime KayitTarihi { get; set; }

        public string Aciklama { get; set; }

        public virtual ICollection<PersonelModulYetki> PersonelModulYetki { get; set; }
    }
}
