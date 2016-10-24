using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectManager.Services;

namespace ProjectManager.Services.Migrations
{
    [DbContext(typeof(Db))]
    [Migration("20161023181204_createdb")]
    partial class createdb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectManager.Model.Domain.Activity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContactID");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Media");

                    b.Property<string>("Notes");

                    b.Property<int?>("ProjectID");

                    b.Property<string>("Tags");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("ContactID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("UserID");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.DefaultContact", b =>
                {
                    b.Property<int>("ProjectID");

                    b.Property<int>("ContactID");

                    b.HasKey("ProjectID", "ContactID");

                    b.HasIndex("ContactID");

                    b.HasIndex("ProjectID");

                    b.ToTable("DefaultContacts");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CompletionDate");

                    b.Property<DateTime>("DueDate");

                    b.Property<bool>("IsComplete");

                    b.Property<string>("Name");

                    b.Property<DateTime>("ProjectDate");

                    b.Property<string>("Tags");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.Reminder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsComplete");

                    b.Property<string>("Notes");

                    b.Property<int?>("ProjectID");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("UserID");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.Activity", b =>
                {
                    b.HasOne("ProjectManager.Model.Domain.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactID");

                    b.HasOne("ProjectManager.Model.Domain.Project", "Project")
                        .WithMany("Activities")
                        .HasForeignKey("ProjectID");

                    b.HasOne("ProjectManager.Model.Domain.User", "User")
                        .WithMany("Activities")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.Contact", b =>
                {
                    b.HasOne("ProjectManager.Model.Domain.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.DefaultContact", b =>
                {
                    b.HasOne("ProjectManager.Model.Domain.Contact", "Contact")
                        .WithMany("DefaultContacts")
                        .HasForeignKey("ContactID");

                    b.HasOne("ProjectManager.Model.Domain.Project", "Project")
                        .WithMany("DefaultContacts")
                        .HasForeignKey("ProjectID");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.Project", b =>
                {
                    b.HasOne("ProjectManager.Model.Domain.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("ProjectManager.Model.Domain.Reminder", b =>
                {
                    b.HasOne("ProjectManager.Model.Domain.Project", "Project")
                        .WithMany("Reminders")
                        .HasForeignKey("ProjectID");

                    b.HasOne("ProjectManager.Model.Domain.User", "User")
                        .WithMany("Reminders")
                        .HasForeignKey("UserID");
                });
        }
    }
}
