using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Assistant.Properties;

namespace Assistant.Entities
{
    public class AssistantEntities : DbContext
    {
        //string conn = ConfigurationManager.ConnectionStrings["AssistantConnectionString"].ConnectionString;

        public AssistantEntities() : base("name=AssistantConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AssistantEntities, Migrations.Configuration>("AssistantConnectionString"));
        }

        public virtual DbSet<Cari> Cari { get; set; }
        public virtual DbSet<CariGrup> CariGrup { get; set; }
        public virtual DbSet<DegisimLog> DegisimLog { get; set; }
        public virtual DbSet<Departman> Departman { get; set; }
        public virtual DbSet<DovizCins> DovizCins { get; set; }
        public virtual DbSet<DovizKur> DovizKur { get; set; }
        public virtual DbSet<Depo> Depo { get; set; }
        public virtual DbSet<DepoTur> DepoTur { get; set; }
        public virtual DbSet<HareketTip> HareketTip { get; set; }
        public virtual DbSet<HareketYon> HareketYon { get; set; }
        public virtual DbSet<LayoutSetting> LayoutSetting { get; set; }
        public virtual DbSet<SatinAlma> SatinAlma { get; set; }
        public virtual DbSet<Stok> Stok { get; set; }
        public virtual DbSet<StokBirim> StokBirim { get; set; }
        public virtual DbSet<StokDepo> StokDepo { get; set; }
        public virtual DbSet<StokGrup> StokGrup { get; set; }
        public virtual DbSet<StokHareket> StokHareket { get; set; }
        public virtual DbSet<StokTalep> StokTalep { get; set; }

        public virtual DbSet<Modul> Modul { get; set; }
        public virtual DbSet<Personel> Personel { get; set; }
        public virtual DbSet<PersonelModulYetki> PersonelModulYetki { get; set; }
        public virtual DbSet<Yetki> Yetki { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //nvarchar ile iþimiz yok, tüm string deðerleri tek tek yapmak [modelBuilder.Entity<Depo>().Property(e => e.DepoAd).IsUnicode(false);] yerine tek seferde isunicode = false yapýyoruz 
            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("VARCHAR"));
            modelBuilder.Properties<decimal>().Configure(x => x.HasPrecision(18, 3));

            modelBuilder.Entity<Cari>().HasMany(e => e.StokTalep).WithRequired(e => e.Cari).HasForeignKey(e => e.TedarikciId).WillCascadeOnDelete(false);

            modelBuilder.Entity<CariGrup>().HasMany(e => e.Cari).WithRequired(e => e.CariGrup).WillCascadeOnDelete(false);

            modelBuilder.Entity<Depo>().HasMany(e => e.StokDepo).WithRequired(e => e.Depo).WillCascadeOnDelete(false);

            modelBuilder.Entity<Stok>().HasMany(e => e.StokDepo).WithRequired(e => e.Stok).WillCascadeOnDelete(false);
        }

        public class NonZeroAttribute : ValidationAttribute
        {
            public override string FormatErrorMessage(string name)
            {
                return $"{name} deðeri sýfýr olamaz";
            }

            public override bool IsValid(object value)
            {
                var zero = Convert.ChangeType(0, value.GetType());
                var returnValue = !zero.Equals(value);
                return returnValue;
            }

            //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            //{
            //    if (IsValid(value)) return new ValidationResult(null);
            //    return new ValidationResult(FormatErrorMessage(validationContext.MemberName));
            //}
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

                    if (entityName == "LayoutSetting")
                    {
                        continue;
                    }

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
                                AnahtarId = primaryKey.ToString(),
                                AlanAdi = prop,
                                EskiDegeri = originalValue,
                                YeniDegeri = currentValue,
                                Kullanici = Settings.Default.Kullanici,
                                KayitTarihi = now
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
            {
                try
                {
                    return base.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    // just to ease debugging
                    foreach (var error in e.EntityValidationErrors)
                    {
                        foreach (var errorMsg in error.ValidationErrors)
                        {
                            // logging service based on NLog

                            throw new DbEntityValidationException(
                                $"Error trying to save EF changes - {errorMsg.ErrorMessage}");
                        }
                    }

                    throw;
                }
                catch (DbUpdateException e)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"DbUpdateException error details - {e.InnerException?.InnerException?.Message}");

                    foreach (var eve in e.Entries)
                    {
                        sb.AppendLine(
                            $"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated");
                    }

                    throw new DbUpdateException(sb.ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    throw;
                }
                //catch (DbEntityValidationException ex)
                //{
                //    var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                //    var fullErrorMessage = string.Join("; ", errorMessages);

                //    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                //    //MessageBox.Show($"{exceptionMessage} {ex.EntityValidationErrors}","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                //}
            }
            else
            {
                return 0;
            }

        }
    }
}
