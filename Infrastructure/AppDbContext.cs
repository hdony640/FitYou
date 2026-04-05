using fityou.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace fityou.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<SessionExercise> SessionExercises => Set<SessionExercise>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Session → SessionExercises
        modelBuilder.Entity<Session>()
            .HasMany(s => s.SessionExercises)
            .WithOne(se => se.Session)
            .HasForeignKey(se => se.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        // SessionExercise → Exercise
        modelBuilder.Entity<SessionExercise>()
            .HasOne(se => se.Exercise)
            .WithMany()
            .HasForeignKey(se => se.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Session → User
        modelBuilder.Entity<Session>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Constraints
        modelBuilder.Entity<Exercise>()
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Session>()
            .Property(s => s.Date)
            .IsRequired();

        modelBuilder.Entity<SessionExercise>()
            .Property(se => se.Reps)
            .IsRequired();

        modelBuilder.Entity<SessionExercise>()
            .Property(se => se.Weight)
            .IsRequired();
    }
}