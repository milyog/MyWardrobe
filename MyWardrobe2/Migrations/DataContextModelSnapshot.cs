﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWardrobe.Data;

#nullable disable

namespace MyWardrobe.Migrations
{
    [DbContext(typeof(MyWardrobeDbContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyWardrobe.Models.WardrobeItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Material")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subcategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WardrobeItems");
                });

            modelBuilder.Entity("MyWardrobe.Models.WardrobeItemUsage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WardrobeItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WardrobeItemUsageCounter")
                        .HasColumnType("int");

                    b.Property<DateTime>("WardrobeItemUsageDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("WardrobeItemId");

                    b.ToTable("WardrobeItemsUsage");
                });

            modelBuilder.Entity("MyWardrobe.Models.WardrobeItemUsage", b =>
                {
                    b.HasOne("MyWardrobe.Models.WardrobeItem", "WardrobeItem")
                        .WithMany("WardrobeItemUsages")
                        .HasForeignKey("WardrobeItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WardrobeItem");
                });

            modelBuilder.Entity("MyWardrobe.Models.WardrobeItem", b =>
                {
                    b.Navigation("WardrobeItemUsages");
                });
#pragma warning restore 612, 618
        }
    }
}
