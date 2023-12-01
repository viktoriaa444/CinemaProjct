using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CinemaProject.Infrastructure
{
    public partial class Context : DbContext
    {
        private static Context context;
        public Context()
            : base("name=Context")
        {
        }
        public static Context GetContext()
        {
            if (context == null)
                context = new Context();
            return context;
        }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>()
                .Property(e => e.IdCinema)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Cinema>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Cinema>()
                .Property(e => e.NameCinema)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.IdClient)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Client>()
                .Property(e => e.LastNameClient)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.NameClient)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.MiddleNameClient)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Session)
                .WithMany(e => e.Client)
                .Map(m => m.ToTable("ClientSession").MapLeftKey("IDClient").MapRightKey("IDSession"));

            modelBuilder.Entity<Employee>()
                .Property(e => e.IdEmployee)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastNameEmployee)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.NameEmployee)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.MiddleNameEmployee)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Floor)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.IDPost)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Hall>()
                .Property(e => e.IdHall)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Hall>()
                .Property(e => e.NumberOfSeats)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Session)
                .WithRequired(e => e.Hall)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.IdMovie)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Movie>()
                .Property(e => e.NameMovie)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.Rating)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Session)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.IdPost)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Post>()
                .Property(e => e.NamePost)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Session>()
                .Property(e => e.IdSession)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Session>()
                .Property(e => e.Cost)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Session>()
                .Property(e => e.Session_start_time)
                .HasPrecision(0);

            modelBuilder.Entity<Session>()
                .Property(e => e.Session_end_time)
                .HasPrecision(0);

            modelBuilder.Entity<Session>()
                .Property(e => e.IDMovie)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Session>()
                .Property(e => e.IDHall)
                .HasPrecision(18, 0);
        }
    }
}
