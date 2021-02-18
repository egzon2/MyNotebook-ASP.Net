using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyNotebook.Models;

namespace MyNotebook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Note>()
            .HasOne<ApplicationUser>(n => n.User)
            .WithMany(au => au.Notes)
            .HasForeignKey(n => n.UserId);

            builder.Entity<Reminder>()
            .HasOne<ApplicationUser>(r => r.User)
            .WithMany(au => au.Reminders)
            .HasForeignKey(r => r.UserId);

            builder.Entity<ToDo>()
            .HasOne<ApplicationUser>(td => td.User)
            .WithMany(au => au.ToDos)
            .HasForeignKey(td => td.UserId);

            
        }

        public DbSet<MyNotebook.Models.Note> Note { get; set; }

        public DbSet<MyNotebook.Models.ToDo> ToDo { get; set; }

        public DbSet<MyNotebook.Models.Reminder> Reminder { get; set; }


    }
}
