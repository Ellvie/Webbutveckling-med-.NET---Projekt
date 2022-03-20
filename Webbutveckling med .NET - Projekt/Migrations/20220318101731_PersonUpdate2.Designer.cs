﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Webbutveckling_med_.NET___Projekt.Data;

#nullable disable

namespace Webbutveckling_med_.NET___Projekt.Migrations
{
    [DbContext(typeof(DogContext))]
    [Migration("20220318101731_PersonUpdate2")]
    partial class PersonUpdate2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0-preview.1.22076.6");

            modelBuilder.Entity("Webbutveckling_med_.NET___Projekt.Models.Dog", b =>
                {
                    b.Property<int>("DogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Added")
                        .HasColumnType("TEXT");

                    b.Property<string>("Adopted")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Pic")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reserved")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DogId");

                    b.HasIndex("PersonId");

                    b.ToTable("Dog");
                });

            modelBuilder.Entity("Webbutveckling_med_.NET___Projekt.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNr")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Reserved")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Webbutveckling_med_.NET___Projekt.Models.Dog", b =>
                {
                    b.HasOne("Webbutveckling_med_.NET___Projekt.Models.Person", "Person")
                        .WithMany("Dog")
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Webbutveckling_med_.NET___Projekt.Models.Person", b =>
                {
                    b.Navigation("Dog");
                });
#pragma warning restore 612, 618
        }
    }
}
