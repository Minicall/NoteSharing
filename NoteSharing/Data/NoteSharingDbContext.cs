using Microsoft.EntityFrameworkCore;
using NoteSharing.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NoteSharing.Data
{
    public class NoteSharingDbContext : DbContext
    {
        public NoteSharingDbContext(DbContextOptions<NoteSharingDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasKey(p => p.Id);
            modelBuilder.Entity<Comment>().HasKey(p => p.Id);
        }
    }
}
