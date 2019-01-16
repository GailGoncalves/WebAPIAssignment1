using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAssignment1.DatabaseHelper
{
    public class AccountType
    {
        [Key]
        public int ID { get; set; }
        public string AccountDescription { get; set; }

        // Navigation properties.
        // Child.        
        public virtual ICollection<ClientAccount>
            ClientAccounts
        { get; set; }
    }

    public class ClientProfile
    {
        [Key]
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        // Navigation properties.
        // Child.
        public virtual ICollection<ClientAccount>
            ClientAccounts
        { get; set; }
    }

    public class ClientAccount
    {
        [Key, Column(Order = 0)]
        public int AccountID { get; set; }
        [Key, Column(Order = 1)]
        public int ClientID { get; set; }
        public decimal Balance { get; set; }

        // Navigation properties.
        // Parents.
        public virtual AccountType AccountType { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }
    }

    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options)
            : base(options) { }

        // Define entity collections.
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<ClientAccount> ClientAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //---------------------------------------------------------------
            // Define composite primary keys.
            modelBuilder.Entity<ClientAccount>()
                .HasKey(ps => new { ps.AccountID, ps.ClientID });

            //---------------------------------------------------------------
            // Define foreign keys here. Do not use foreign key annotations.
            modelBuilder.Entity<ClientAccount>()
                .HasOne(p => p.AccountType) // Parent
                .WithMany(p => p.ClientAccounts) // Child
                .HasForeignKey(fk => new { fk.AccountID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<ClientAccount>()
                .HasOne(p => p.ClientProfile) // Parent
                .WithMany(p => p.ClientAccounts) // Child
                .HasForeignKey(fk => new { fk.ClientID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            Seed(modelBuilder);
        }

        void Seed(ModelBuilder builder)
        {
            // Seed parents first and then children since child FK's point to parents.
            builder.Entity<AccountType>().HasData(
                new { ID = 1, AccountDescription = "Checking" },
                new { ID = 2, AccountDescription = "Savings" }
            );
            builder.Entity<ClientProfile>().HasData(
                new { ID = 1, LastName = "Jones", FirstName = "Barb" },
                new { ID = 2, LastName = "Applewood", FirstName = "Bob" }
            );
            builder.Entity<ClientAccount>().HasData(
                new { AccountID = 1, ClientID = 2, Balance = 101.51m },
                new { AccountID = 2, ClientID = 1, Balance = 455.53m }
            );

        }
    }
}






