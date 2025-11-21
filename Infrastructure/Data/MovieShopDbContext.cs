using ApplicationCore.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data {
    public class MovieShopDbContext : DbContext {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options) {

        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<Cast> Casts { get; set; }

        public DbSet<MovieCast> MovieCasts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //modelBuilder.Entity<Movie>(entity => {
            //    entity.Property(e => e.Title).HasColumnType("varchar(20)");
            //    entity.HasKey(e => e.Id);
            //});

            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<User>(ConfigureUsers);
            modelBuilder.Entity<Role>(ConfigureRoles);
            modelBuilder.Entity<Favorite>(ConfigureFavorites);
            modelBuilder.Entity<Review>(ConfigureReviews);
            modelBuilder.Entity<Purchase>(ConfigurePurchases);
            modelBuilder.Entity<UserRole>(ConfigureUserRoles);
            modelBuilder.Entity<Genre>(ConfigureGenre);
        }
        
        private void ConfigureGenre(EntityTypeBuilder<Genre> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
        }
        private void ConfigureUserRoles(EntityTypeBuilder<UserRole> builder) {
            builder.HasKey(x => new { x.RoleId, x.UserId });
            builder.HasOne(x => x.User).WithMany(z => z.UserRoles).HasForeignKey(z => z.UserId);
            builder.HasOne(x => x.Role).WithMany(z => z.UserRoles).HasForeignKey(z => z.RoleId);
        }
        private void ConfigurePurchases(EntityTypeBuilder<Purchase> builder) {
            builder.HasKey(x => new { x.UserId, x.MovieId });
            builder.HasOne(x => x.User).WithMany(x => x.Purchases).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Movie).WithMany(x => x.Purchases).HasForeignKey(x => x.MovieId);
            builder.Property(x => x.PurchaseDateTime).IsRequired();
            builder.Property(x => x.PurchaseNumber).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();
        }

        private void ConfigureReviews(EntityTypeBuilder<Review> builder) {
            builder.HasKey(x => new { x.MovieId, x.UserId });
            builder.HasOne(x => x.Movie).WithMany(x => x.Reviews).HasForeignKey(x => x.MovieId);
            builder.HasOne(x => x.User).WithMany(x => x.Reviews).HasForeignKey(x => x.UserId);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.ReviewText).IsRequired();
        }
        private void ConfigureFavorites(EntityTypeBuilder<Favorite> builder) {
            builder.HasKey(x => new { x.UserId, x.MovieId });
            builder.HasOne(x => x.Movie).WithMany(x => x.Favorites).HasForeignKey(x => x.MovieId);
            builder.HasOne(x => x.User).WithMany(x => x.Favorites).HasForeignKey(x => x.UserId);
        }

        private void ConfigureRoles(EntityTypeBuilder<Role> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("nvarchar(20)").IsRequired();
        }

        private void ConfigureUsers(EntityTypeBuilder<User> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasColumnType("nvarchar(256)").IsRequired();
            builder.Property(x => x.FirstName).HasColumnType("nvarchar(128)").IsRequired();
            builder.Property(x => x.HashedPassword).HasColumnType("nvarchar(1024)").IsRequired();
            builder.Property(x => x.IsLocked).HasColumnType("bit");
            builder.Property(x => x.LastName).HasColumnType("nvarchar(128)").IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar(16)");
            builder.Property(x => x.Salt).HasColumnType("nvarchar(1024)").IsRequired();
        }
        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder) {
            builder.HasKey(x => new { x.CastId, x.MovieId });
            builder.HasOne(x => x.Cast).WithMany(x => x.MovieCasts).HasForeignKey(x => x.CastId);
            builder.HasOne(x => x.Movie).WithMany(x => x.MovieCasts).HasForeignKey(x => x.MovieId);
            builder.Property(x => x.Character).HasColumnType("nvarchar(450)").IsRequired();

        }
        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder) {
            builder.HasKey(x => new { x.MovieId, x.GenreId });
            builder.HasOne(x => x.Movie).WithMany(x => x.MovieGenres).HasForeignKey(x => x.MovieId);
            builder.HasOne(x => x.Genre).WithMany(x => x.MovieGenres).HasForeignKey(x => x.GenreId);
        }
        private void ConfigureCast(EntityTypeBuilder<Cast> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.Name).HasColumnType("nvarchar(128)").IsRequired();
            builder.Property(x => x.ProfilePath).HasColumnType("nvarchar(2084)").IsRequired();
            builder.Property(x => x.TmdbUrl).IsRequired();
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder) {
            //fluent API
            //specify all the rules for entity
            builder.ToTable("Movies");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasColumnType("nvarchar(256)");
            builder.Property(m => m.BackdropUrl).HasColumnType("nvarchar(2084)");
            builder.Property(m => m.Budget).HasColumnType("decimal(18,4)");
            builder.Property(m => m.ImdbURL).HasColumnType("nvarchar(2084)");
            builder.Property(m => m.OriginalLanguage).HasColumnType("nvarchar(64)");
            builder.Property(m => m.PosterUrl).HasColumnType("nvarchar(2084)");
            builder.Property(m => m.Price).HasColumnType("decimal(5,2)");
            builder.Property(m => m.Revenue).HasColumnType("decimal(18,4)");
            builder.Property(m => m.TagLine).HasColumnType("nvarchar(512)");
            builder.Property(m => m.TmdbUrl).HasColumnType("nvarchar(2084)");




        }




    }
}   