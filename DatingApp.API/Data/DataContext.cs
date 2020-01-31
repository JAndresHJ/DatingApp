using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Since the Like model does not contain an id property we need to
            // explicitly stablish what will be the PK
            // The PK is a combination of the LikerId and the LikeeId to make impossible 
            // for a user to like another user more than once 
            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikeeId});
            
            // This is where the Fluent API to define the relationship and configure EF so
            // it knows what it's doing when it creates the new table in our DB
            builder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict); // When you remove a like you don't want the user to be deleted

            // Also One Liker can have many Likees
            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict); // When you remove a like you don't want the user to be deleted

            builder.Entity<Message>()
              .HasOne(u => u.Sender)
              .WithMany(m => m.MessagesSent)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
              .HasOne(u => u.Recipient)
              .WithMany(m => m.MessagesReceived)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}