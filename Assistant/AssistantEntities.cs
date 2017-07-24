using System.Data.Entity.Infrastructure;
using System.Windows.Forms;
using Assistant.Properties;

namespace Assistant
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public sealed class AssistantEntities : DbContext
    {
        public AssistantEntities() : base("name=AssistantEntities")
        {
            Database.SetInitializer(new AssistantInitializer());
        }

        public DbSet<DegisimLog> DegisimLog { get; set; }
        public DbSet<Depo> Depo { get; set; }
        public DbSet<DepoTur> DepoTur { get; set; }
        public DbSet<HareketYon> HareketYon { get; set; }
        public DbSet<HareketTip> HareketTip { get; set; }
        public DbSet<SatinAlma> SatinAlma { get; set; }
        public DbSet<Stok> Stok { get; set; }
        public DbSet<StokBirim> StokBirim { get; set; }
        public DbSet<StokDepo> StokDepo { get; set; }
        public DbSet<StokGrup> StokGrup { get; set; }
        public DbSet<StokHareket> StokHareket { get; set; }
        public DbSet<StokTalep> StokTalep { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DegisimLog>().Property(e => e.TabloAdi).IsUnicode(false);

            modelBuilder.Entity<DegisimLog>().Property(e => e.AlanAdi).IsUnicode(false);

            modelBuilder.Entity<DegisimLog>().Property(e => e.AnahtarID).IsUnicode(false);

            modelBuilder.Entity<DegisimLog>().Property(e => e.EskiDegeri).IsUnicode(false);

            modelBuilder.Entity<DegisimLog>().Property(e => e.YeniDegeri).IsUnicode(false);

            modelBuilder.Entity<DegisimLog>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<Depo>().Property(e => e.DepoAd).IsUnicode(false);

            modelBuilder.Entity<Depo>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<Depo>().HasMany(e => e.StokDepo).WithRequired(e => e.Depo).WillCascadeOnDelete(false);

            modelBuilder.Entity<DepoTur>().Property(e => e.DepoTuru).IsUnicode(false);

            modelBuilder.Entity<DepoTur>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<HareketYon>().Property(e => e.HareketYonu).IsUnicode(false);

            modelBuilder.Entity<SatinAlma>().Property(e => e.Miktar).HasPrecision(18, 3);

            modelBuilder.Entity<SatinAlma>().Property(e => e.BirimFiyat).HasPrecision(18, 3);

            modelBuilder.Entity<SatinAlma>().Property(e => e.Lot).IsFixedLength();

            modelBuilder.Entity<SatinAlma>().Property(e => e.Aciklama).IsUnicode(false);

            modelBuilder.Entity<SatinAlma>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<Stok>().Property(e => e.StokKod).IsUnicode(false);

            modelBuilder.Entity<Stok>().Property(e => e.StokAd).IsUnicode(false);

            modelBuilder.Entity<Stok>().Property(e => e.Aciklama).IsUnicode(false);

            modelBuilder.Entity<Stok>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<Stok>().HasMany(e => e.StokDepo).WithRequired(e => e.Stok).WillCascadeOnDelete(false);

            modelBuilder.Entity<StokBirim>().Property(e => e.Birim).IsUnicode(false);

            modelBuilder.Entity<StokBirim>().Property(e => e.Sembol).IsUnicode(false);

            modelBuilder.Entity<StokBirim>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<StokDepo>().Property(e => e.KritikStok).HasPrecision(18, 4);

            modelBuilder.Entity<StokDepo>().Property(e => e.Aciklama).IsUnicode(false);

            modelBuilder.Entity<StokDepo>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<StokGrup>().Property(e => e.GrupKod).IsUnicode(false);

            modelBuilder.Entity<StokGrup>().Property(e => e.Grup).IsUnicode(false);

            modelBuilder.Entity<StokGrup>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<StokHareket>().Property(e => e.Miktar).HasPrecision(18, 4);

            modelBuilder.Entity<StokHareket>().Property(e => e.Aciklama).IsUnicode(false);

            modelBuilder.Entity<StokHareket>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<StokTalep>().Property(e => e.Miktar).HasPrecision(18, 3);

            modelBuilder.Entity<StokTalep>().Property(e => e.Lot).IsFixedLength();

            modelBuilder.Entity<StokTalep>().Property(e => e.Aciklama).IsUnicode(false);

            modelBuilder.Entity<StokTalep>().Property(e => e.Kullanici).IsUnicode(false);

            modelBuilder.Entity<StokTalep>().HasMany(e => e.SatinAlma).WithRequired(e => e.StokTalep).WillCascadeOnDelete(false);
        }

        public override int SaveChanges()
        {
            var changedPropCount = 0;
            var propCount = 0;

            try
            {
                var modifiedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
                var now = DateTime.UtcNow;

                foreach (var change in modifiedEntities)
                {
                    var entityName = change.Entity.GetType().Name;
                    if (entityName.Contains("_")) entityName = entityName.Substring(0, entityName.IndexOf("_", StringComparison.Ordinal));
                    var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(change.Entity);
                    var primaryKey = objectStateEntry.EntityKey.EntityKeyValues[0].Value;

                    foreach (var prop in change.OriginalValues.PropertyNames)
                    {
                        if (prop == "Guncelleme") continue;
                        propCount++;

                        var originalValue = (change.OriginalValues[prop] ?? string.Empty).ToString();
                        var currentValue = (change.CurrentValues[prop] ?? string.Empty).ToString();

                        if (originalValue != currentValue)
                        {
                            changedPropCount++;
                            DegisimLog log = new DegisimLog
                            {
                                TabloAdi = entityName,
                                AnahtarID = primaryKey.ToString(),
                                AlanAdi = prop,
                                EskiDegeri = originalValue,
                                YeniDegeri = currentValue,
                                Kullanici = Settings.Default.Kullanici,
                                DegisimTarihi = now
                            };
                            DegisimLog.Add(log);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Log Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (propCount == 0 || (propCount > 0 && changedPropCount > 0))
                return base.SaveChanges();
            return 0;
        }
    }
}
