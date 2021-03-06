﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPIAssignment1.DatabaseHelper;

namespace WebAPIAssignment1.Migrations
{
    [DbContext(typeof(BankDbContext))]
    partial class BankDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPIAssignment1.DatabaseHelper.AccountType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("accountDescription");

                    b.HasKey("ID");

                    b.ToTable("AccountTypes");

                    b.HasData(
                        new { ID = 1 },
                        new { ID = 2 }
                    );
                });

            modelBuilder.Entity("WebAPIAssignment1.DatabaseHelper.ClientAccount", b =>
                {
                    b.Property<int>("AccountID");

                    b.Property<int>("ClientID");

                    b.Property<decimal>("Balance");

                    b.HasKey("AccountID", "ClientID");

                    b.HasIndex("ClientID");

                    b.ToTable("ClientAccounts");

                    b.HasData(
                        new { AccountID = 1, ClientID = 2, Balance = 101.51m },
                        new { AccountID = 2, ClientID = 1, Balance = 455.53m }
                    );
                });

            modelBuilder.Entity("WebAPIAssignment1.DatabaseHelper.ClientProfile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ID");

                    b.ToTable("AccountProfiles");

                    b.HasData(
                        new { ID = 1, FirstName = "Barb", LastName = "Jones" },
                        new { ID = 2, FirstName = "Bob", LastName = "Applewood" }
                    );
                });

            modelBuilder.Entity("WebAPIAssignment1.DatabaseHelper.ClientAccount", b =>
                {
                    b.HasOne("WebAPIAssignment1.DatabaseHelper.AccountType", "AccountType")
                        .WithMany("ClientAccounts")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAPIAssignment1.DatabaseHelper.ClientProfile", "ClientProfile")
                        .WithMany("ClientAccounts")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
