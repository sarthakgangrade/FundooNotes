using CommonLayer.Note;
using CommonLayer.User;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FundooDbContext:DbContext
    {
        public FundooDbContext(DbContextOptions options) : base(options) 
        {

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Label> Label { get; set; }
        public DbSet<UserAddress> Address { get; set; }
        public DbSet<Collab> Collab { get; set; }
        protected override void
        OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
            .HasIndex(u => u.email)
            .IsUnique();
            //modelBuilder.Entity<Label>()
            //    .HasKey(e => new { e.userId,e.NotesId });
            //modelBuilder.Entity<Label>()
            //   .HasOne(e => e.User)
            //    .WithMany(e => e.Label)
            //    .HasForeignKey(e => e.userId)
            //    .OnDelete(DeleteBehavior.Cascade); //Cascade behaviour
            //modelBuilder.Entity<Label>()
            //   .HasOne(e => e.Notes)
            //    .WithMany(e => e.Label)
            //    .HasForeignKey(e => e.NotesId);

            


        }
    }
}
