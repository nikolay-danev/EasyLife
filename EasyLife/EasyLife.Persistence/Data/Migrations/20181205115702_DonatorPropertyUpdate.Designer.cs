﻿// <auto-generated />
using System;
using EasyLife.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyLife.Persistence.Data.Migrations
{
    [DbContext(typeof(EasyLifeDbContext))]
    [Migration("20181205115702_DonatorPropertyUpdate")]
    partial class DonatorPropertyUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyLife.Domain.Models.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("CreatorId");

                    b.Property<string>("CreatorId1");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BusinessName")
                        .IsUnique();

                    b.HasIndex("CreatorId1");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("EasyLife.Domain.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("OfficeId");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("EasyLife.Domain.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("OfficeId");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("EasyLife.Domain.Models.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CCVNumber")
                        .HasMaxLength(3);

                    b.Property<decimal>("CardNumber")
                        .HasMaxLength(19);

                    b.Property<string>("CardOwnerName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("DonatorId");

                    b.Property<DateTime>("ExpiryDate");

                    b.HasKey("Id");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("EasyLife.Domain.Models.Donator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AmountDonated");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("CreditCardId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsContinuousDonation");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId")
                        .IsUnique();

                    b.ToTable("Donators");
                });

            modelBuilder.Entity("EasyLife.Domain.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("EasyLife.Domain.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalDescription");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("ServiceTitle")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("ServiceType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("EasyLife.Domain.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<double>("Discount");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EasyLife.Domain.Models.Advertisement", b =>
                {
                    b.HasOne("EasyLife.Domain.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId1");
                });

            modelBuilder.Entity("EasyLife.Domain.Models.City", b =>
                {
                    b.HasOne("EasyLife.Domain.Models.Office", "Office")
                        .WithMany("Cities")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyLife.Domain.Models.Country", b =>
                {
                    b.HasOne("EasyLife.Domain.Models.Office", "Office")
                        .WithMany("Countries")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyLife.Domain.Models.Donator", b =>
                {
                    b.HasOne("EasyLife.Domain.Models.CreditCard", "CreditCard")
                        .WithOne("Donator")
                        .HasForeignKey("EasyLife.Domain.Models.Donator", "CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EasyLife.Domain.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EasyLife.Domain.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EasyLife.Domain.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EasyLife.Domain.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
