using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace ServiceTracker.DAL.Models
{
    public partial class STContext : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory
          = new LoggerFactory(new[] { new DebugLoggerProvider()});

        public STContext()
        {
        }

        public STContext(DbContextOptions<STContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<ChangePointTask> ChangePointTask { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CostType> CostType { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Domain> Domain { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<ExchangeRates> ExchangeRates { get; set; }
        public virtual DbSet<Field> Field { get; set; }
        public virtual DbSet<MasterCode> MasterCode { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<TechnicalLead> TechnicalLead { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Well> Well { get; set; }
        public virtual DbSet<Portfolio> Portfolio { get; set; }
        public virtual DbSet<SubPortfolio> SubPortfolio { get; set; }

        // Unable to generate entity type for table 'dbo.AccountUnit'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ActivityCost'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.AccountTechicalLead'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //MyLoggerFactory.AddDebug(LogLevel.Information | LogLevel.Error | LogLevel.Debug);
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            if (!optionsBuilder.IsConfigured)
            {
                //Configure stuff here.
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.CompanyId)
                    .HasName("NonClusteredIndex-20180723-161245");

                entity.HasIndex(e => new { e.ServiceDeliveryManagerId, e.AccountManagerId })
                    .HasName("NonClusteredIndex-20180723-161222");

                entity.Property(e => e.CountryCode).HasMaxLength(5);

                entity.Property(e => e.CurrencyCode).HasMaxLength(5);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.PurchaseOrder).HasMaxLength(50);

                entity.Property(e => e.QuoteFtl)
                    .HasColumnName("QuoteFTL")
                    .HasMaxLength(50);

                entity.HasOne(d => d.AccountManager)
                    .WithMany(p => p.AccountAccountManager)
                    .HasForeignKey(d => d.AccountManagerId)
                    .HasConstraintName("FK_Account_Employee_AM");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Account_Company");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.CountryCode)
                    .HasConstraintName("FK_Country_Account");

                entity.HasOne(d => d.CurrencyCodeNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.CurrencyCode)
                    .HasConstraintName("FK_Currency_Account");

                entity.HasOne(d => d.ServiceDeliveryManager)
                    .WithMany(p => p.AccountServiceDeliveryManager)
                    .HasForeignKey(d => d.ServiceDeliveryManagerId)
                    .HasConstraintName("FK_Employee_Account_SDL");
            });

            modelBuilder.Entity<ChangePointTask>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.Name })
                    .HasName("NonClusteredIndex-20180724-132422")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<CostType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.CurrencyId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_Currency_1");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Alias).HasMaxLength(5);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(49)
                    .HasComputedColumnSql("((((('['+isnull([Alias],''))+'] ')+isnull([Name],''))+' ')+isnull([LastName],''))");

                entity.Property(e => e.FullName1).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.Ldap)
                    .HasColumnName("LDAP")
                    .HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<ExchangeRates>(entity =>
            {
                entity.HasIndex(e => new { e.FromCurrencyId, e.ToCurrencyId, e.Date })
                    .HasName("IX_ExchangeRates")
                    .IsUnique();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FromCurrencyId)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Rate).HasColumnType("numeric(7, 4)");

                entity.Property(e => e.ToCurrencyId)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MasterCode>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.MasterCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Ac)
                    .HasColumnName("AC")
                    .HasMaxLength(255);

                entity.Property(e => e.Am)
                    .HasColumnName("AM")
                    .HasMaxLength(255);

                entity.Property(e => e.Au)
                    .HasColumnName("AU")
                    .HasMaxLength(255);

                entity.Property(e => e.ChangePointTask).HasMaxLength(255);

                entity.Property(e => e.Client).HasMaxLength(255);

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.CostDescription).HasMaxLength(255);

                entity.Property(e => e.CostReceived).HasColumnType("money");

                entity.Property(e => e.CostType).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.Currency).HasMaxLength(255);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Field).HasMaxLength(255);

                entity.Property(e => e.Fxrate)
                    .HasColumnName("FXRate")
                    .HasMaxLength(255);

                entity.Property(e => e.Glaccount)
                    .HasColumnName("GLAccount")
                    .HasMaxLength(255);

                entity.Property(e => e.IMf)
                    .HasColumnName("iMF")
                    .HasColumnType("money");

                entity.Property(e => e.InvocieNumber).HasMaxLength(255);

                entity.Property(e => e.MasterCode).HasMaxLength(255);

                entity.Property(e => e.Mmf)
                    .HasColumnName("MMF")
                    .HasColumnType("money");

                entity.Property(e => e.Po)
                    .HasColumnName("PO")
                    .HasMaxLength(255);

                entity.Property(e => e.Portfolio).HasMaxLength(255);

                entity.Property(e => e.QuoteFtl)
                    .HasColumnName("QuoteFTL")
                    .HasMaxLength(255);

                entity.Property(e => e.Revenue).HasColumnType("money");

                entity.Property(e => e.Rofo)
                    .HasColumnName("ROFO")
                    .HasColumnType("money");

                entity.Property(e => e.Sdl)
                    .HasColumnName("SDL")
                    .HasMaxLength(255);

                entity.Property(e => e.SentToInvoice).HasColumnType("money");

                entity.Property(e => e.SubPortfolio).HasMaxLength(255);

                entity.Property(e => e.TechnicalLead).HasMaxLength(255);

                entity.Property(e => e.Well).HasMaxLength(255);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Account_Service");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.DomainId)
                    .HasConstraintName("FK_Domain_Service");
            });

            modelBuilder.Entity<SubPortfolio>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.SubPortfolios)
                    .HasForeignKey(d => d.PortfolioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubPortfolio_SubPortfolio");
            });

            modelBuilder.Entity<TechnicalLead>(entity =>
            {
                entity.HasKey(e => new { e.ServiceTrackerId, e.UerId });
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Well>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.Well)
                    .HasForeignKey(d => d.FieldId)
                    .HasConstraintName("FK_Fields_Wells");
            });
        }
    }
}
