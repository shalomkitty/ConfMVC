﻿// <auto-generated />
using System;
using ConfDomain.Model;
using ConfInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConfInfrastructure.Migrations
{
    [DbContext(typeof(DbconappContext))]
    [Migration("20250407170539_AddImageToConf")]
    partial class AddImageToConf
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConfDomain.Model.Conference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("ntext")
                        .HasColumnName("description");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizatorId")
                        .HasColumnType("int")
                        .HasColumnName("organizator_id");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("place");

                    b.Property<int?>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.Property<int>("PublicationId")
                        .HasColumnType("int")
                        .HasColumnName("publication_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("OrganizatorId");

                    b.HasIndex("PublicationId");

                    b.ToTable("Conference", (string)null);
                });

            modelBuilder.Entity("ConfDomain.Model.Organizator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("ntext")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Party")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("party");

                    b.HasKey("Id");

                    b.ToTable("Organizator", (string)null);
                });

            modelBuilder.Entity("ConfDomain.Model.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("title");

                    b.Property<DateOnly>("UploadDate")
                        .HasColumnType("date")
                        .HasColumnName("upload_date");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Publication", (string)null);
                });

            modelBuilder.Entity("ConfDomain.Model.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConferenceId")
                        .HasColumnType("int")
                        .HasColumnName("conference_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("UserId");

                    b.ToTable("Ticket", (string)null);
                });

            modelBuilder.Entity("ConfDomain.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("full_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("ConfDomain.Model.Conference", b =>
                {
                    b.HasOne("ConfDomain.Model.Organizator", "Organizator")
                        .WithMany("Conferences")
                        .HasForeignKey("OrganizatorId")
                        .IsRequired()
                        .HasConstraintName("FK_Conference_Organizator");

                    b.HasOne("ConfDomain.Model.Publication", "Publication")
                        .WithMany("Conferences")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizator");

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("ConfDomain.Model.Publication", b =>
                {
                    b.HasOne("ConfDomain.Model.User", "User")
                        .WithMany("Publications")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Publication_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConfDomain.Model.Ticket", b =>
                {
                    b.HasOne("ConfDomain.Model.Conference", "Conference")
                        .WithMany("Tickets")
                        .HasForeignKey("ConferenceId")
                        .IsRequired()
                        .HasConstraintName("FK_Ticket_User");

                    b.HasOne("ConfDomain.Model.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Ticket_User1");

                    b.Navigation("Conference");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConfDomain.Model.Conference", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("ConfDomain.Model.Organizator", b =>
                {
                    b.Navigation("Conferences");
                });

            modelBuilder.Entity("ConfDomain.Model.Publication", b =>
                {
                    b.Navigation("Conferences");
                });

            modelBuilder.Entity("ConfDomain.Model.User", b =>
                {
                    b.Navigation("Publications");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
