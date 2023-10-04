﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Use_Wheels.Data;

#nullable disable

namespace Use_Wheels.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230920090433_AddIsBlackedField")]
    partial class AddIsBlackedField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Use_Wheels.Models.DTO.Car", b =>
                {
                    b.Property<string>("Vehicle_No")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Availability")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Img_URL")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<int>("Pre_Owner_Count")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<string>("RC_No")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Updated_Date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Vehicle_No");

                    b.HasIndex("Category_Id");

                    b.HasIndex("RC_No");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Vehicle_No = "DL 89 JU 9921",
                            Availability = true,
                            Category_Id = 1,
                            Created_Date = new DateTime(2023, 9, 20, 14, 34, 33, 872, DateTimeKind.Local).AddTicks(850),
                            Description = "Some description",
                            Img_URL = "D://car1.jpg",
                            Likes = 0,
                            Pre_Owner_Count = 2,
                            Price = 2500000f,
                            RC_No = "635289",
                            Updated_Date = new DateTime(2023, 9, 20, 14, 34, 33, 872, DateTimeKind.Local).AddTicks(850)
                        });
                });

            modelBuilder.Entity("Use_Wheels.Models.DTO.Category", b =>
                {
                    b.Property<int>("Category_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Category_Names")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Category_Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Category_Id = 1,
                            Category_Names = "SUV"
                        },
                        new
                        {
                            Category_Id = 2,
                            Category_Names = "Hatchback"
                        },
                        new
                        {
                            Category_Id = 3,
                            Category_Names = "Sedan"
                        });
                });

            modelBuilder.Entity("Use_Wheels.Models.DTO.Orders", b =>
                {
                    b.Property<int>("Order_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Gross_Price")
                        .HasColumnType("float");

                    b.Property<float>("Net_Price")
                        .HasColumnType("float");

                    b.Property<string>("Payment_Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Vehicle_No")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Order_ID");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Use_Wheels.Models.DTO.RC", b =>
                {
                    b.Property<string>("RC_No")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Board_Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Car_Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Colour")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateOnly>("Date_Of_Reg")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FC_Validity")
                        .HasColumnType("date");

                    b.Property<string>("Fuel_Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Insurance_Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Manufactured_Year")
                        .HasColumnType("int");

                    b.Property<string>("Owner_Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Owner_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("Reg_Valid_Upto")
                        .HasColumnType("date");

                    b.Property<DateTime>("Updated_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Vehicle_No")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RC_No");

                    b.ToTable("RC");

                    b.HasData(
                        new
                        {
                            RC_No = "635289",
                            Board_Type = "Own board",
                            Car_Model = "Honda CR-V",
                            Colour = "Red",
                            Created_Date = new DateTime(2023, 9, 20, 14, 34, 33, 872, DateTimeKind.Local).AddTicks(840),
                            Date_Of_Reg = new DateOnly(2001, 3, 1),
                            FC_Validity = new DateOnly(2025, 3, 1),
                            Fuel_Type = "Diesel",
                            Insurance_Type = "Third party",
                            Manufactured_Year = 2004,
                            Owner_Address = "Vasanth Vihar",
                            Owner_Name = "Ram",
                            Reg_Valid_Upto = new DateOnly(2031, 3, 1),
                            Updated_Date = new DateTime(2023, 9, 20, 14, 34, 33, 872, DateTimeKind.Local).AddTicks(840),
                            Vehicle_No = "DL 89 JU 9921"
                        });
                });

            modelBuilder.Entity("Use_Wheels.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Last_Login")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("isBlacked")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Use_Wheels.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Use_Wheels.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Use_Wheels.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Use_Wheels.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Use_Wheels.Models.DTO.Car", b =>
                {
                    b.HasOne("Use_Wheels.Models.DTO.Category", "Category")
                        .WithMany()
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Use_Wheels.Models.DTO.RC", "Rc_Details")
                        .WithMany()
                        .HasForeignKey("RC_No")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Rc_Details");
                });

            modelBuilder.Entity("Use_Wheels.Models.DTO.Orders", b =>
                {
                    b.HasOne("Use_Wheels.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
