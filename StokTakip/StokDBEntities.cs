using System.Data.Entity.Infrastructure;
using System.Windows.Forms;
using StokTakip.Properties;

namespace StokTakip
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public sealed class StokDbEntities : DbContext
    {
        public StokDbEntities()
            : base("name=StokDBEntities")
        {
        }

        private DbSet<ChangeLog> ChangeLog { get; set; }
        public DbSet<Depo> Depo { get; set; }
        public DbSet<DepoTur> DepoTur { get; set; }
        public DbSet<HareketYon> HareketYon { get; set; }
        public DbSet<SatinAlma> SatinAlma { get; set; }
        public DbSet<Stok> Stok { get; set; }
        public DbSet<StokBirim> StokBirim { get; set; }
        public DbSet<StokDepo> StokDepo { get; set; }
        public DbSet<StokGrup> StokGrup { get; set; }
        public DbSet<StokHareket> StokHareket { get; set; }
        public DbSet<StokTalep> StokTalep { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ChangeLog>().Property(e => e.EntityName).IsUnicode(false);

            modelBuilder.Entity<ChangeLog>().Property(e => e.PropertyName).IsUnicode(false);

            modelBuilder.Entity<ChangeLog>().Property(e => e.PrimaryKeyValue).IsUnicode(false);

            modelBuilder.Entity<ChangeLog>().Property(e => e.OldValue).IsUnicode(false);

            modelBuilder.Entity<ChangeLog>().Property(e => e.NewValue).IsUnicode(false);

            modelBuilder.Entity<ChangeLog>().Property(e => e.UserName).IsUnicode(false);

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
            var modifiedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
            var now = DateTime.UtcNow;
            //try
            //{
            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                //entityName = entityName.Substring(0, entityName.IndexOf("_"));
                var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(change.Entity);
                var primaryKey = objectStateEntry.EntityKey.EntityKeyValues[0].Value;

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.OriginalValues[prop].ToString();// == null ? string.Empty : change.OriginalValues[prop].ToString();
                    var currentValue = change.CurrentValues[prop].ToString();// == null ? string.Empty : change.CurrentValues[prop].ToString();

                    if (originalValue != currentValue)
                    {
                        ChangeLog log = new ChangeLog()
                        {
                            EntityName = entityName,
                            PrimaryKeyValue = primaryKey.ToString(),
                            PropertyName = prop,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            UserName = Settings.Default.Kullanici,
                            DateChanged = now
                        };
                        ChangeLog.Add(log);
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, @"Log Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            return base.SaveChanges();

        }
    }
}
