using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Entities.Event;
using Project.Domain.Entities.Product;
using Project.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        // Event-related entities
        public DbSet<Event> Events { get; set; }
        public DbSet<EventMetadata> EventMetadata { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<EventSchedule> EventSchedules { get; set; }

        // Product-related entities
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }

        // User-related entities
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        // Other domain entities
        public DbSet<Redemption> Redemptions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS; Initial Catalog=Project_EfCore; Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserAccount)
                .WithOne(a => a.User)
                .HasForeignKey<UserAccount>(a => a.UserId);

            modelBuilder.Entity<Product>()
                .HasOne(u => u.ProductPrice)
                .WithOne(a => a.Product)
                .HasForeignKey<ProductPrice>(a => a.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(u => u.ProductStock)
                .WithOne(a => a.Product)
                .HasForeignKey<ProductStock>(a => a.ProductId);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Participants)
                .WithOne(p => p.Event)
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Events and participants
            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => ep.Id);

            // 🔹 Each participant belongs to one Event
            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Each participant belongs to one User
            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.User)
                .WithMany()
                .HasForeignKey(ep => ep.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // unique (EventId, UserId) pair
            modelBuilder.Entity<EventParticipant>()
                .HasIndex(ep => new { ep.EventId, ep.UserId })
                .IsUnique();

            // Event (1) <-> (1) EventMetadata
            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventMetadata)
                .WithOne(m => m.Event)
                .HasForeignKey<EventMetadata>(m => m.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // EventMetadata (many) -> (1) User (Organizer)
            modelBuilder.Entity<EventMetadata>()
                .HasOne(m => m.Organizer)
                .WithMany()
                .HasForeignKey(m => m.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraint on (EventId, OrganizerId)
            modelBuilder.Entity<EventMetadata>()
                .HasIndex(m => new { m.EventId, m.OrganizerId })
                .IsUnique();

            // Enforce uniqueness for (UserId, ProductId)
            modelBuilder.Entity<Redemption>()
                .HasIndex(r => new { r.UserId, r.ProductId })
                .IsUnique();

            
            modelBuilder.Entity<Redemption>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Redemption>()
                .HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
