using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Doulingo_Api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<ChallengeArrange> ChallengeArranges { get; set; }
        public DbSet<ChallengeChoose> ChallengeChooses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User => UserProgress(1,n),  Course => UserProgress(1,n) => User => Course (n,n)
            modelBuilder.Entity<UserProgress>()
            .HasKey(e => e.Id);

            modelBuilder.Entity<UserProgress>()
            .HasOne(e => e.User)
            .WithMany(e => e.UserProgresses)
            .HasForeignKey(e => e.UserId)
            .IsRequired(true);

            modelBuilder.Entity<UserProgress>()
            .HasOne(e => e.Course)
            .WithMany(e => e.UserProgresses)
            .HasForeignKey(e => e.CourseId)
            .IsRequired(true);

            // Course => Section (1,n) 
            modelBuilder.Entity<Section>()
            .HasOne(e => e.Course)
            .WithMany(e => e.Sections)
            .HasForeignKey(e => e.CourseId)
            .IsRequired(true);

            // Section => Unit (1,n) 
            modelBuilder.Entity<Unit>()
            .HasOne(e => e.Section)
            .WithMany(e => e.Units)
            .HasForeignKey(e => e.SectionId)
            .IsRequired(true);

            // Unit => Lesson (1,n) 
            modelBuilder.Entity<Lesson>()
            .HasOne(e => e.Unit)
            .WithMany(e => e.Lessons)
            .HasForeignKey(e => e.UnitId)
            .IsRequired(true);

            // Lesson => ChallengeArrange (1,n) 
            modelBuilder.Entity<ChallengeArrange>()
            .HasOne(e => e.Lesson)
            .WithMany(e => e.ChallengeArranges)
            .HasForeignKey(e => e.LessonId)
            .IsRequired(true);

            // Lesson => ChallengeArrange (1,n) 
            modelBuilder.Entity<ChallengeChoose>()
            .HasOne(e => e.Lesson)
            .WithMany(e => e.ChallengeChooses)
            .HasForeignKey(e => e.LessonId)
            .IsRequired(true);
        }
    }
}
