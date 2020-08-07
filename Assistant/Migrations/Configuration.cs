using System;
using System.Data.SqlClient;
using Assistant.Entities;
using System.Linq;

namespace Assistant.Migrations
{
    using System.Data.Entity.Migrations;

    internal class Configuration : DbMigrationsConfiguration<AssistantEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Assistant.AssistantEntities";
        }

        private static bool CheckDatabaseExists()
        {
            AssistantEntities context = new AssistantEntities();

            var conn = context.Database.Connection.ConnectionString;
            SqlConnection connectionString = new SqlConnection(conn);

            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                using (var command = new SqlCommand($"SELECT db_id('Assistant')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }

        protected override void Seed(AssistantEntities context)
        {
            if (context.DepoTur.Any()) return;

            context.DovizCins.AddOrUpdate(
                new DovizCins() { Id = 1, Doviz = "TRY", Sembol = "₺" },
                new DovizCins() { Id = 2, Doviz = "USD", Sembol = "$" },
                new DovizCins() { Id = 3, Doviz = "EUR", Sembol = "€" },
                new DovizCins() { Id = 4, Doviz = "GBP", Sembol = "£" });

            context.DovizKur.AddOrUpdate(
                new DovizKur() { Id = 1, DovizCinsId = 1, Alis = (decimal)1.0, Satis = (decimal)1.0, Tarih = DateTime.Now },
                new DovizKur() { Id = 2, DovizCinsId = 2, Alis = (decimal)3.5, Satis = (decimal)3.6, Tarih = DateTime.Now },
                new DovizKur() { Id = 3, DovizCinsId = 3, Alis = (decimal)4.1, Satis = (decimal)4.2, Tarih = DateTime.Now },
                new DovizKur() { Id = 4, DovizCinsId = 4, Alis = (decimal)4.5, Satis = (decimal)4.6, Tarih = DateTime.Now });

            context.DepoTur.AddOrUpdate(
                new DepoTur { Id = 1, DepoTuru = "Kimyasal" },
                new DepoTur { Id = 2, DepoTuru = "İplik" },
                new DepoTur { Id = 3, DepoTuru = "Kumaş" },
                new DepoTur { Id = 4, DepoTuru = "Yakacak" });

            context.Depo.AddOrUpdate(
                new Depo() { Id = 1, DepoAd = "Boyar Madde", DepoTurId = 1 },
                new Depo() { Id = 2, DepoAd = "Kimyevi Madde", DepoTurId = 1 },
                new Depo() { Id = 3, DepoAd = "Ambar", DepoTurId = 3 },
                new Depo() { Id = 4, DepoAd = "Ham İplik", DepoTurId = 2 },
                new Depo() { Id = 5, DepoAd = "Boyalı İplik", DepoTurId = 2 },
                new Depo() { Id = 6, DepoAd = "Artık İplik", DepoTurId = 4 },
                new Depo() { Id = 7, DepoAd = "Iskarta", DepoTurId = 3 });

            context.StokBirim.AddOrUpdate(
                new StokBirim() { Id = 1, Birim = "Kilogram", Sembol = "kg" },
                new StokBirim() { Id = 2, Birim = "Ton", Sembol = "tn" },
                new StokBirim() { Id = 3, Birim = "Metre", Sembol = "mt" },
                new StokBirim() { Id = 4, Birim = "Litre", Sembol = "lt" });

            context.StokGrup.AddOrUpdate(
                new StokGrup() { Id = 1, Grup = "Boyalar", GrupKod = "BY" },
                new StokGrup() { Id = 2, Grup = "Sarf Malzemeleri", GrupKod = "SM" },
                new StokGrup() { Id = 3, Grup = "Gıda", GrupKod = "GD" },
                new StokGrup() { Id = 4, Grup = "Bakım ve Onarım", GrupKod = "BO" });

            context.Stok.AddOrUpdate(
                new Stok() { Id = 1, StokKod = "LVY", StokAd = "Levafix Yellow Ca", StokGrupId = 1, Aciklama = "Fiyatı yüksek, sorgulansın" },
                new Stok() { Id = 2, StokKod = "BLC", StokAd = "Levafix Black Ca", StokGrupId = 1, Aciklama = "" },
                new Stok() { Id = 3, StokKod = "KST", StokAd = "Kostik", StokGrupId = 1, Aciklama = "Kostik bitmeden alınacak" },
                new Stok() { Id = 4, StokKod = "KMR", StokAd = "Kömür", StokGrupId = 2, Aciklama = "Manisadan alıyoruz" },
                new Stok() { Id = 5, StokKod = "INB", StokAd = "Indanthren Royal Blue", StokGrupId = 1, Aciklama = "Navy yerine alındı" });

            context.StokDepo.AddOrUpdate(
                new StokDepo() { Id = 1, StokId = 1, BirimId = 1, DepoId = 1, KritikStok = 500, Aciklama = "500 altına düşmeden alınsın." },
                new StokDepo() { Id = 2, StokId = 2, BirimId = 1, DepoId = 1, KritikStok = 500, Aciklama = "" },
                new StokDepo() { Id = 3, StokId = 3, BirimId = 2, DepoId = 2, KritikStok = 75, Aciklama = "" },
                new StokDepo() { Id = 4, StokId = 4, BirimId = 2, DepoId = 3, KritikStok = 30, Aciklama = "" },
                new StokDepo() { Id = 5, StokId = 5, BirimId = 4, DepoId = 1, KritikStok = 10, Aciklama = "" });

            context.StokTalep.AddOrUpdate(
                new StokTalep() { Id = 1, StokId = 1, Miktar = 1500, TedarikciId = 3, StokBirimId = 1, DepartmanId = 5, SarfYeri = "A Salonu", Uretici = "HGF Chemicals", Marka = "HGF", Lot = "1212", Aciklama = "Acil", Termin = DateTime.Now },
                new StokTalep() { Id = 2, StokId = 2, Miktar = 800, TedarikciId = 3, StokBirimId = 1, DepartmanId = 1, SarfYeri = "B Salonu", Uretici = "HGF Chemicals", Marka = "HGF", Lot = "1212", Aciklama = "Acil", Termin = DateTime.Now },
                new StokTalep() { Id = 3, StokId = 3, Miktar = 350, TedarikciId = 3, StokBirimId = 1, DepartmanId = 2, SarfYeri = "C Salonu", Uretici = "HGF Chemicals", Marka = "HGF", Lot = "1212", Aciklama = "Acil", Termin = DateTime.Now },
                new StokTalep() { Id = 4, StokId = 4, Miktar = 250, TedarikciId = 3, StokBirimId = 1, DepartmanId = 2, SarfYeri = "D Salonu", Uretici = "HGF Chemicals", Marka = "HGF", Lot = "1212", Aciklama = "Acil", Termin = DateTime.Now },
                new StokTalep() { Id = 5, StokId = 5, Miktar = 1000, TedarikciId = 3, StokBirimId = 1, DepartmanId = 3, SarfYeri = "E Salonu", Uretici = "HGF Chemicals", Marka = "HGF", Lot = "1212", Aciklama = "Acil", Termin = DateTime.Now },
                new StokTalep() { Id = 6, StokId = 1, Miktar = 50, TedarikciId = 3, StokBirimId = 1, DepartmanId = 4, SarfYeri = "A Salonu", Uretici = "HGF Chemicals", Marka = "HGF", Lot = "1212", Aciklama = "Acil", Termin = DateTime.Now });

            context.HareketYon.AddOrUpdate(
                new HareketYon() { Id = 1, HareketYonu = "Giriş" },
                new HareketYon() { Id = 2, HareketYonu = "Çıkış" });

            context.HareketTip.AddOrUpdate(
                new HareketTip() { Id = 1, HareketYonId = 1, HareketTipi = "Satın Alma" },
                new HareketTip() { Id = 2, HareketYonId = 1, HareketTipi = "İade" },
                new HareketTip() { Id = 3, HareketYonId = 1, HareketTipi = "Bedelsiz" },
                new HareketTip() { Id = 4, HareketYonId = 2, HareketTipi = "Sarf" },
                new HareketTip() { Id = 5, HareketYonId = 2, HareketTipi = "Satış" },
                new HareketTip() { Id = 6, HareketYonId = 2, HareketTipi = "Hurdaya Ayırma" },
                new HareketTip() { Id = 7, HareketYonId = 2, HareketTipi = "Firmaya İade" });

            context.StokHareket.AddOrUpdate(
                new StokHareket() { Id = 1, HareketTipId = 3, StokDepoId = 1, Miktar = 1500 },
                new StokHareket() { Id = 2, HareketTipId = 4, StokDepoId = 1, Miktar = -60 },
                new StokHareket() { Id = 3, HareketTipId = 4, StokDepoId = 1, Miktar = -45 },
                new StokHareket() { Id = 4, HareketTipId = 4, StokDepoId = 1, Miktar = -100 },
                new StokHareket() { Id = 5, HareketTipId = 6, StokDepoId = 1, Miktar = -25 },
                new StokHareket() { Id = 6, HareketTipId = 7, StokDepoId = 1, Miktar = -50 },
                new StokHareket() { Id = 7, HareketTipId = 1, StokDepoId = 1, Miktar = 400 },
                new StokHareket() { Id = 8, HareketTipId = 4, StokDepoId = 1, Miktar = -75 },
                new StokHareket() { Id = 9, HareketTipId = 4, StokDepoId = 1, Miktar = -15 },
                new StokHareket() { Id = 10, HareketTipId = 4, StokDepoId = 1, Miktar = -30 },
                new StokHareket() { Id = 11, HareketTipId = 4, StokDepoId = 1, Miktar = -5 });

            context.CariGrup.AddOrUpdate(
                new CariGrup() { Id = 1, Grup = "Personel" },
                new CariGrup() { Id = 2, Grup = "Müşteri" },
                new CariGrup() { Id = 3, Grup = "Toptancı" },
                new CariGrup() { Id = 4, Grup = "Alıcı" },
                new CariGrup() { Id = 5, Grup = "Satıcı" });

            context.Cari.AddOrUpdate(
                new Cari() { Id = 1, CariKod = "120-00-0266", CariGrupId = 2, FirmaAd = "Bal Tekstil", Yetkili = "Ahmet Bey", Telefon = "02581234567", Email = "ahmet@baltekstil.com", InternetAdres = "-", Adres = "125. Kısım Organize Denizli", SevkAdresi = "125. Kısım Organize Denizli", Aciklama = "Bu Firma İle daha dikkatli çalışmak lazım." },
                new Cari() { Id = 2, CariKod = "120-00-1255", CariGrupId = 2, FirmaAd = "Çelik Dokuma", Yetkili = "Nuri Bey", Telefon = "02581234567", Email = "nuri@celikdokuma.com", InternetAdres = "-", Adres = "452. Sokak No:5  Bozburun", SevkAdresi = "452. Sokak No:5  Bozburun", Aciklama = "" },
                new Cari() { Id = 3, CariKod = "120-00-6542", CariGrupId = 3, FirmaAd = "Bulut Kimya", Yetkili = "Bulut Bey", Telefon = "02581234567", Email = "", InternetAdres = "-", Adres = "OSB Denizli", SevkAdresi = "125. Kısım Organize Denizli", Aciklama = "" },
                new Cari() { Id = 4, CariKod = "120-00-1284", CariGrupId = 3, FirmaAd = "GHT Boya", Yetkili = "Ayşe Hanım", Telefon = "02581234567", Email = "", InternetAdres = "-", Adres = "Nazif Zorlu Sitesi No :45", SevkAdresi = "125. Kısım Organize Denizli", Aciklama = "" },
                new Cari() { Id = 5, CariKod = "120-00-9754", CariGrupId = 4, FirmaAd = "Turlu Konfeksiyon.", Yetkili = "Musa Turlu", Telefon = "02581234567", Email = "", InternetAdres = "-", Adres = "125. Kısım Organize Denizli", SevkAdresi = "125. Kısım Organize Denizli", Aciklama = "" },
                new Cari() { Id = 6, CariKod = "120-00-6654", CariGrupId = 5, FirmaAd = "Acenta Asf.", Yetkili = "Asaf Dönmez", Telefon = "02581234567", Email = "asafdonmez@gmail.com", InternetAdres = "-", Adres = "Tuzla İstanbul", SevkAdresi = "125. Kısım Organize Denizli", Aciklama = "" });

            context.Departman.AddOrUpdate(
                new Departman() { Id = 0, DepartmanAd = "Genel" },
                new Departman() { Id = 1, DepartmanAd = "Bilgi İşlem" },
                new Departman() { Id = 2, DepartmanAd = "Pazarlama" },
                new Departman() { Id = 3, DepartmanAd = "Planlama" },
                new Departman() { Id = 4, DepartmanAd = "Muhasebe" },
                new Departman() { Id = 5, DepartmanAd = "Satın Alma" },
                new Departman() { Id = 6, DepartmanAd = "Depo" });

            context.Personel.AddOrUpdate(
                new Personel() { Id = 1, Kullanici = "Admin", Sifre = "1", DepartmanId = 1, AdSoyad = "Assistant Admin", DahiliNo = "0", EmailAdres = "dincermansur@gmail.com", Aciklama = "", Aktiflik = true },
                new Personel() { Id = 2, Kullanici = "Mansur", Sifre = "1", DepartmanId = 1, AdSoyad = "Emrah Tolu", DahiliNo = "100", EmailAdres = "dincermansur@gmail.com", Aciklama = "", Aktiflik = true },
                new Personel() { Id = 3, Kullanici = "Akif", Sifre = "1", DepartmanId = 1, AdSoyad = "Akif Hamarat", DahiliNo = "200", EmailAdres = "akifhamarat@gmail.com", Aciklama = "", Aktiflik = false });

            context.Modul.AddOrUpdate(
                new Modul() { Id = 1, Aktiflik = true, FormAd = "CariFormu" },
                new Modul() { Id = 2, Aktiflik = true, FormAd = "CariGrupFormu" },
                new Modul() { Id = 3, Aktiflik = true, FormAd = "DepoFormu" },
                new Modul() { Id = 4, Aktiflik = true, FormAd = "DepoTurFormu" },
                new Modul() { Id = 5, Aktiflik = true, FormAd = "DovizKurFormu" },
                new Modul() { Id = 6, Aktiflik = true, FormAd = "HareketTipFormu" },
                new Modul() { Id = 7, Aktiflik = true, FormAd = "SatinAlmaFormu" },
                new Modul() { Id = 8, Aktiflik = true, FormAd = "StokBirimFormu" },
                new Modul() { Id = 9, Aktiflik = true, FormAd = "StokDepoFormu" },
                new Modul() { Id = 10, Aktiflik = true, FormAd = "StokFormu" },
                new Modul() { Id = 11, Aktiflik = true, FormAd = "StokGrupFormu" },
                new Modul() { Id = 12, Aktiflik = true, FormAd = "StokHareketFormu" },
                new Modul() { Id = 13, Aktiflik = true, FormAd = "StokTalepFormu" });

            context.Yetki.AddOrUpdate(
                new Yetki() { Id = 1, YetkiAd = "Kaydet" },
                new Yetki() { Id = 3, YetkiAd = "Güncelle" },
                new Yetki() { Id = 2, YetkiAd = "Sil" });

            context.PersonelModulYetki.AddOrUpdate(
                new PersonelModulYetki() { Id = 1, PersonelId = 1, ModulId = 1, YetkiId = 1 },
                new PersonelModulYetki() { Id = 2, PersonelId = 1, ModulId = 1, YetkiId = 2 },
                new PersonelModulYetki() { Id = 3, PersonelId = 2, ModulId = 2, YetkiId = 1 },
                new PersonelModulYetki() { Id = 4, PersonelId = 3, ModulId = 2, YetkiId = 3 });

            context.SatinAlma.AddOrUpdate(
                new SatinAlma() { Id = 1, StokId = 1, Miktar = 250, BirimId = 1, BirimFiyat = (decimal)2.56, DovizId = 2, TedarikciId = 4, Uretici = "Urban", Lot = "10001", Aciklama = "Stok alım", StokTalepId = 2 },
                new SatinAlma() { Id = 2, StokId = 2, Miktar = 750, BirimId = 3, BirimFiyat = (decimal)3.64, DovizId = 2, TedarikciId = 4, Uretici = "Turing", Lot = "201252", Aciklama = "Stok alım", StokTalepId = 2 },
                new SatinAlma() { Id = 3, StokId = 3, Miktar = 425, BirimId = 1, BirimFiyat = (decimal)1.24, DovizId = 2, TedarikciId = 4, Uretici = "Intel", Lot = "112542", Aciklama = "Stok alım", StokTalepId = 5 },
                new SatinAlma() { Id = 4, StokId = 4, Miktar = 175, BirimId = 4, BirimFiyat = (decimal)7.58, DovizId = 2, TedarikciId = 4, Uretici = "Torku", Lot = "348432", Aciklama = "Stok alım", StokTalepId = 1 },
                new SatinAlma() { Id = 5, StokId = 1, Miktar = 670, BirimId = 2, BirimFiyat = (decimal)6.11, DovizId = 2, TedarikciId = 4, Uretici = "Ulker", Lot = "3874654", Aciklama = "Stok alım", StokTalepId = 3 });

            base.Seed(context);
        }
    }
}
