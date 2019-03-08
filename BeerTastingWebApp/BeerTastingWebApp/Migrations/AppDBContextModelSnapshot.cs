﻿// <auto-generated />
using System;
using BeerTastingWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeerTastingWebApp.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeerTastingWebApp.Models.Beer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("Producer");

                    b.Property<int>("SystemetNumber");

                    b.Property<int?>("TastingID");

                    b.HasKey("ID");

                    b.HasIndex("TastingID");

                    b.ToTable("Beer");
                });

            modelBuilder.Entity("BeerTastingWebApp.Models.Tasting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Name");

                    b.Property<int?>("SessionMeisterID");

                    b.HasKey("ID");

                    b.HasIndex("SessionMeisterID");

                    b.ToTable("Tasting");
                });

            modelBuilder.Entity("BeerTastingWebApp.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<DateTime>("SignedUp");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BeerTastingWebApp.Models.UserTasting", b =>
                {
                    b.Property<int>("TastingId");

                    b.Property<int>("UserId");

                    b.HasKey("TastingId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTasting");
                });

            modelBuilder.Entity("BeerTastingWebApp.Models.Beer", b =>
                {
                    b.HasOne("BeerTastingWebApp.Models.Tasting")
                        .WithMany("Beers")
                        .HasForeignKey("TastingID");
                });

            modelBuilder.Entity("BeerTastingWebApp.Models.Tasting", b =>
                {
                    b.HasOne("BeerTastingWebApp.Models.User", "SessionMeister")
                        .WithMany()
                        .HasForeignKey("SessionMeisterID");
                });

            modelBuilder.Entity("BeerTastingWebApp.Models.UserTasting", b =>
                {
                    b.HasOne("BeerTastingWebApp.Models.Tasting", "Tasting")
                        .WithMany("Participants")
                        .HasForeignKey("TastingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeerTastingWebApp.Models.User", "User")
                        .WithMany("Tastings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
