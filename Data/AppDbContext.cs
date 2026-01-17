using design_pattern_case_1.Entity;
using Microsoft.EntityFrameworkCore;

namespace design_pattern_case_1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        //Blog
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostVote> PostVotes { get; set; }

        //Book
        public DbSet<Entity.Book.Book> Books { get; set; }
        public DbSet<Entity.Book.Bundle> Bundles { get; set; }

        //App Config
        public DbSet<AppConfig> AppConfigs { get; set; }

        // OPTION: You can remove OnModelCreating if you want EF Core conventions
        // OR keep it for explicit control over relationships and delete behavior
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicit relationship configuration (RECOMMENDED for production)
            // Prevents accidental cascade deletes and makes intent clear

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Can't delete user if they have posts

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Delete comments when post is deleted

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Can't delete user if they have comments

            // PostVote relationships
            modelBuilder.Entity<PostVote>()
                .HasOne(pv => pv.Post)
                .WithMany()
                .HasForeignKey(pv => pv.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostVote>()
                .HasOne(pv => pv.VotedBy)
                .WithMany()
                .HasForeignKey(pv => pv.VotedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate votes: one user can only vote once per post
            modelBuilder.Entity<PostVote>()
                .HasIndex(pv => new { pv.PostId, pv.VotedByUserId })
                .IsUnique();

            // Bundle self-referencing relationship
            modelBuilder.Entity<Entity.Book.Bundle>()
                .HasOne(b => b.ParentBundle)
                .WithMany(b => b.ChildBundles)
                .HasForeignKey(b => b.BundleId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if bundle has children

            // Book to Bundle relationship
            modelBuilder.Entity<Entity.Book.Book>()
                .HasOne(b => b.Bundle)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.BundleId)
                .OnDelete(DeleteBehavior.SetNull); // Books become unbundled when bundle is deleted
        }
    }
}
