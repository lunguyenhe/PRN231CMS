using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN231APICMS.Models
{
    public partial class PRN231CMS1Context : DbContext
    {
        public PRN231CMS1Context()
        {
        }

        public PRN231CMS1Context(DbContextOptions<PRN231CMS1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<Option> Options { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<SubmittedAssignment> SubmittedAssignments { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<TestQuestion> TestQuestions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserQuestion> UserQuestions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =localhost; database =PRN231CMS111;uid=sa;pwd=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Assignmen__Subje__34C8D9D1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Assignmen__UserI__403A8C7D");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.Content).IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Options)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Options__Questio__35BCFE0A");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.AssignedDate).HasColumnType("date");

                entity.Property(e => e.Content).IsUnicode(false);

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Subjects__UserID__4316F928");
            });

            modelBuilder.Entity<SubmittedAssignment>(entity =>
            {
                entity.HasKey(e => e.SubmissionId)
                    .HasName("PK__Submitte__449EE1054937458F");

                entity.Property(e => e.SubmissionId).HasColumnName("SubmissionID");

                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");

                entity.Property(e => e.SubmissionDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.SubmittedAssignments)
                    .HasForeignKey(d => d.AssignmentId)
                    .HasConstraintName("FK__Submitted__Assig__36B12243");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SubmittedAssignments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Submitted__UserI__37A5467C");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.TestId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TestID");

                entity.Property(e => e.TestName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Subject)
                    .WithMany()
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Tests__SubjectID__3A81B327");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Tests__UserID__4222D4EF");
            });

            modelBuilder.Entity<TestQuestion>(entity =>
            {
                entity.Property(e => e.TestQuestionId).HasColumnName("TestQuestionID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TestQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__TestQuest__Quest__38996AB5");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleID__3F466844");
            });

            modelBuilder.Entity<UserQuestion>(entity =>
            {
                entity.HasKey(e => e.UserQuestions)
                    .HasName("PK__UserQues__7B1AE41062785E91");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.UserQuestions)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK__UserQuest__Optio__3B75D760");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.UserQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__UserQuest__Quest__3C69FB99");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserQuestions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserQuest__UserI__3E52440B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
