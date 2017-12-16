﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Entity.Enumarations;
using System;

namespace SingleProductStore.Data.Sql.Migrations
{
    [DbContext(typeof(SpsContext))]
    [Migration("20171216150430_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SingleProductStore.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<decimal>("Cost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(10,2)")
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(10,2)")
                        .HasDefaultValue(0m);

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("Product");
                });

            modelBuilder.Entity("SingleProductStore.Entity.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<bool>("CalculatedOriginalPrice");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Descrition");

                    b.Property<int>("IsBestOption");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<decimal?>("OriginalPrice");

                    b.Property<decimal>("Price");

                    b.Property<int>("PromotionPricingType")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<int?>("QuantityAvaliable");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("Promotion");
                });

            modelBuilder.Entity("SingleProductStore.Entity.PromotionItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("ProductId");

                    b.Property<int>("PromotionId");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasAlternateKey("PromotionId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("PromotionItem");
                });

            modelBuilder.Entity("SingleProductStore.Entity.PromotionItem", b =>
                {
                    b.HasOne("SingleProductStore.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SingleProductStore.Entity.Promotion", "Promotion")
                        .WithMany("PromotionItems")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
