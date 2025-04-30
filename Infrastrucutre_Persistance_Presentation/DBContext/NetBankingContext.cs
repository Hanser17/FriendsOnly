

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre_Persistance_Presentation.DBContext
{
    public class NetBankingContext : DbContext
    {
        public NetBankingContext(DbContextOptions<NetBankingContext> options) :base(options) { }

        public DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<CashAdvance> CashAdvances { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transfer> Transfers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavingsAccount>(entity =>
            {
                entity.ToTable("SavingsAccount");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
               .HasDefaultValueSql("(ABS(CHECKSUM(NEWID())) % 900000000 + 100000000)");

                entity.Property(e => e.UserId)
                      .IsRequired();

                entity.Property(e => e.Balance)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.IsPrimary)
                      .IsRequired();

                entity.HasIndex(e => e.UserId)
                      .HasDatabaseName("IX_SavingsAccount_UserId");

                
            });

            modelBuilder.Entity<Beneficiary>(entity =>
            {
                entity.ToTable("Beneficiaries");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserId);

                entity.HasIndex(e => e.UserId)
                      .HasDatabaseName("IX_Beneficiary_UserId");

                entity.Property(e => e.AccountNumber)
                      .IsRequired();

                entity.HasIndex(e => e.AccountNumber)
                      .IsUnique()
                      .HasDatabaseName("IX_AccountNumber");

                
            });

            modelBuilder.Entity<CashAdvance>(entity =>
            {
                entity.ToTable("CashAdvance");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.CreditCardId)
                      .IsRequired();

                entity.Property(e => e.DestinationAccountId)
                      .IsRequired();

                entity.Property(e => e.Amount)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.Interest)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.Date)
                      .IsRequired();
                entity.Property(e => e.UserId);

                entity.HasIndex(e => e.UserId)
                      .HasDatabaseName("IX_CashAdvance_UserId");


            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.ToTable("CreditCard");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
               .HasDefaultValueSql("(ABS(CHECKSUM(NEWID())) % 900000000 + 100000000)");

                entity.Property(e => e.UserId)
                      .IsRequired();

                entity.Property(e => e.CreditLimit)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.Debt)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.HasIndex(e => e.UserId)
                      .HasDatabaseName("IX_CreditCard_UserId");

               
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("Loan");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
               .HasDefaultValueSql("(ABS(CHECKSUM(NEWID())) % 900000000 + 100000000)");

                entity.Property(e => e.UserId)
                      .IsRequired();

                entity.Property(e => e.Amount)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.Debt)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.HasIndex(e => e.UserId)
                      .HasDatabaseName("IX_Loan_UserId");

                
               
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserId)
                      .IsRequired();

                entity.Property(e => e.SourceAccountId)
                      .IsRequired();

                entity.Property(e => e.DestinationAccountId)
                      .IsRequired();

                entity.Property(e => e.Amount)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.Date)
                      .IsRequired();

                entity.HasIndex(e => e.UserId)
                      .HasDatabaseName("IX_Payment_UserId");

                entity.HasIndex(e => e.SourceAccountId)
                      .HasDatabaseName("IX_Payment_SourceAccountId");

                entity.HasIndex(e => e.DestinationAccountId)
                      .HasDatabaseName("IX_Payment_DestinationAccountId");

                
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("Transfer");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.SourceAccountId)
                      .IsRequired();

                entity.Property(e => e.DestinationAccountId)
                      .IsRequired();

                entity.Property(e => e.Amount)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.Date)
                      .IsRequired();

                entity.HasIndex(e => e.SourceAccountId)
                      .HasDatabaseName("IX_Transfer_SourceAccountId");

                entity.HasIndex(e => e.DestinationAccountId)
                      .HasDatabaseName("IX_Transfer_DestinationAccountId");

                entity.Property(e => e.UserId);

                entity.HasIndex(e => e.UserId)
                      .HasDatabaseName("IX_Transfer_UserId");

                // Configurar OnDelete para las cuentas
                entity.HasOne<SavingsAccount>()
                      .WithMany()
                      .HasForeignKey(e => e.SourceAccountId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<SavingsAccount>()
                      .WithMany()
                      .HasForeignKey(e => e.DestinationAccountId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
