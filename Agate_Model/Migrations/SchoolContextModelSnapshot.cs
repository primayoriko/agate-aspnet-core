﻿// <auto-generated />
using Agate_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Agate_Model.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Agate_Model.Class", b =>
                {
                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("ClassNumber")
                        .HasColumnType("int");

                    b.HasKey("Grade", "ClassNumber");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("Agate_Model.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("ClassNumber")
                        .HasColumnType("int");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("StudentId");

                    b.HasIndex("Grade", "ClassNumber");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Agate_Model.Student", b =>
                {
                    b.HasOne("Agate_Model.Class", "CurrentClass")
                        .WithMany("Students")
                        .HasForeignKey("Grade", "ClassNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
