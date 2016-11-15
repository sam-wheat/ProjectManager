using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectManager.Model.Domain;
using ProjectManager.Core;


namespace ProjectManager.Services
{
    public class Db : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<DefaultContact> DefaultContacts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<User> Users { get; set; }

        internal Db() // required for migrations
        {
            // read the help
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public Db(ProjectManagerDbContextOptions options) : base(options.Options)
        {
            
        }

        internal Db(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Project>()
                .HasMany(x => x.Activities)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectID)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Project>()
                .HasMany(x => x.DefaultContacts);

            mb.Entity<Project>()
                .HasMany(x => x.Reminders)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectID)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Project>()
                .HasOne(x => x.User)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Activity>()
                .HasOne(x => x.User)
                .WithMany(x => x.Activities)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Contact>()
                .HasOne(x => x.User)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Reminder>()
                .HasOne(x => x.User)
                .WithMany(x => x.Reminders)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Restrict);


            mb.Entity<DefaultContact>()
                .HasKey(x => new { x.ProjectID, x.ContactID });

            mb.Entity<DefaultContact>()
                .HasOne(x => x.Contact)
                .WithMany(x => x.DefaultContacts)
                .HasForeignKey(x => x.ContactID)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<DefaultContact>()
                .HasOne(x => x.Project)
                .WithMany(x => x.DefaultContacts)
                .HasForeignKey(x => x.ProjectID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
