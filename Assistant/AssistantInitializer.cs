using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant
{
    class AssistantInitializer : DropCreateDatabaseIfModelChanges<AssistantEntities>
    {
        protected override void Seed(AssistantEntities context)
        {
            /* Depo Tur */
            IList<DepoTur> defaultDepoTur = new List<DepoTur>();
            defaultDepoTur.Add(new DepoTur() { DepoTuru = "Kimyasal" });
            defaultDepoTur.Add(new DepoTur() { DepoTuru = "İplik" });
            defaultDepoTur.Add(new DepoTur() { DepoTuru = "Kumaş" });
            defaultDepoTur.Add(new DepoTur() { DepoTuru = "Yakacak" });

            foreach (DepoTur df in defaultDepoTur)
                context.DepoTur.Add(df);

            IList<HareketYon> defaultHareketYon = new List<HareketYon>();
            defaultHareketYon.Add(new HareketYon() { HareketYonu = "Giriş"});
            defaultHareketYon.Add(new HareketYon() { HareketYonu = "Çıkış"});

            foreach (HareketYon df in defaultHareketYon)
                context.HareketYon.Add(df);

            /* Depo  */
            //IList<Depo> defaultDepo = new List<Depo>();
            //defaultDepo.Add(new Depo() { DepoAd = "Boyar Madde", DepoTuruID = 1 });
            //defaultDepo.Add(new Depo() { DepoAd = "Kimyevi Madde", DepoTuruID = 1 });
            //defaultDepo.Add(new Depo() { DepoAd = "Ambar", DepoTuruID = 4 });
            //defaultDepo.Add(new Depo() { DepoAd = "Ham İplik", DepoTuruID = 2 });
            //defaultDepo.Add(new Depo() { DepoAd = "Boyalı İplik", DepoTuruID = 2 });
            //defaultDepo.Add(new Depo() { DepoAd = "Artık İplik", DepoTuruID = 2 });
            //defaultDepo.Add(new Depo() { DepoAd = "Iskarta", DepoTuruID = 3 });
            //foreach (Depo df in defaultDepo)
            //    context.Depo.Add(df);

            /* Stok Birim */
            IList<StokBirim> defaultStokBirim = new List<StokBirim>();
            defaultStokBirim.Add(new StokBirim() { Birim = "Kilogram", Sembol = "kg" });
            defaultStokBirim.Add(new StokBirim() { Birim = "Ton", Sembol = "tn" });
            defaultStokBirim.Add(new StokBirim() { Birim = "Metre", Sembol = "mt" });
            defaultStokBirim.Add(new StokBirim() { Birim = "Litre", Sembol = "lt" });
            foreach (StokBirim df in defaultStokBirim)
                context.StokBirim.Add(df);

            /* Stok Grup */
            IList<StokGrup> defaultStokGrup = new List<StokGrup>();
            defaultStokGrup.Add(new StokGrup() { Grup = "Boyalar", GrupKod = "BY" });
            defaultStokGrup.Add(new StokGrup() { Grup = "Sarf Malzemeleri", GrupKod = "SM" });
            defaultStokGrup.Add(new StokGrup() { Grup = "Gıda", GrupKod = "GD" });
            defaultStokGrup.Add(new StokGrup() { Grup = "Bakım ve Onarım", GrupKod = "BO" });
            foreach (StokGrup df in defaultStokGrup)
                context.StokGrup.Add(df);

            ///*Stok*/
            //IList<Stok> defaultStok = new List<Stok>();
            //defaultStok.Add(new Stok() { StokKod = "LVY", StokAd = "Levafix Yellow Ca", GrupID = 1, Aciklama = "Fiyatı yüksek, sorgulansın" });
            //defaultStok.Add(new Stok() { StokKod = "BLC", StokAd = "Levafix Black Ca", GrupID = 1, Aciklama = "" });
            //defaultStok.Add(new Stok() { StokKod = "KST", StokAd = "Kostik", GrupID = 1, Aciklama = "Kostik bitmeden alınacak" });
            //defaultStok.Add(new Stok() { StokKod = "KMR", StokAd = "Kömür", GrupID = 2, Aciklama = "Manisadan alıyoruz" });
            //defaultStok.Add(new Stok() { StokKod = "INB", StokAd = "Indanthren Royal Blue", GrupID = 1, Aciklama = "Navy yerine alındı" });
            //foreach (Stok df in defaultStok)
            //    context.Stok.Add(df);

            ///* Stok Depo */
            //IList<StokDepo> defaultStokDepo = new List<StokDepo>();
            //defaultStokDepo.Add(new StokDepo() { StokID = 1, BirimID = 1, DepoID = 1, KritikStok = 500, Aciklama = "500 altına düşmeden alınsın." });
            //defaultStokDepo.Add(new StokDepo() { StokID = 2, BirimID = 1, DepoID = 1, KritikStok = 500, Aciklama = "" });
            //defaultStokDepo.Add(new StokDepo() { StokID = 3, BirimID = 2, DepoID = 2, KritikStok = 75, Aciklama = "" });
            //defaultStokDepo.Add(new StokDepo() { StokID = 4, BirimID = 2, DepoID = 3, KritikStok = 30, Aciklama = "" });
            //defaultStokDepo.Add(new StokDepo() { StokID = 5, BirimID = 4, DepoID = 1, KritikStok = 10, Aciklama = "" });
            //foreach (StokDepo df in defaultStokDepo)
            //    context.StokDepo.Add(df);


            base.Seed(context);
        }
    }
}
