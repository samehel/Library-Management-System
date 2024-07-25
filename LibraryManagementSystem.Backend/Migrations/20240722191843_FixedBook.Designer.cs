﻿// <auto-generated />
using System;
using LibraryManagementSystem.Backend.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagementSystem.Backend.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20240722191843_FixedBook")]
    partial class FixedBook
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.Audit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ActionDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ActionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Details")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.Book", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CartID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeweyDecimalNumber")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Genre")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ISBN")
                        .HasColumnType("TEXT");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CartID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.Borrowing", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("BorrowDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("LateFee")
                        .HasColumnType("REAL");

                    b.Property<int>("RenewalCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Borrowing");
                });

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.Cart", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.Fine", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Fines");
                });

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.Token", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("TEXT");

                    b.Property<string>("TokenValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.TransactionHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("TransactionHistory");
                });

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.Book", b =>
                {
                    b.HasOne("LibraryManagementSystem.Backend.Models.Cart", null)
                        .WithMany("Books")
                        .HasForeignKey("CartID");
                });

            modelBuilder.Entity("LibraryManagementSystem.Backend.Models.Cart", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
