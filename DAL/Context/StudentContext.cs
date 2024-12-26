using System;
using System.Collections.Generic;
using System.Threading;
using DAL.UserModels;
using Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context;

public partial class StudentContext : IdentityDbContext<ApplicationUser>
{
    public StudentContext()
    {
    }

    public StudentContext(DbContextOptions<StudentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbStudent> TbStudents { get; set; }
    public virtual DbSet<TbSubject> TbSubjects { get; set; }
    public virtual DbSet<TbStudentSubjects> TbStudentSubjects { get; set; }
    public virtual DbSet<VwStudenSubjects> VwStudenSubjects { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<TbStudent>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<TbSubject>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });


        modelBuilder.Entity<TbStudentSubjects>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Student).
            WithMany(p => p.StudentSubjects)
            .HasForeignKey(d => d.StudentId);

            entity.HasOne(d => d.Subject).
            WithMany(p => p.StudentSubjects).
            HasForeignKey(d => d.SubjectId);
;
        });

        modelBuilder.Entity<VwStudenSubjects>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VwStudenSubjects");

        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<string>().HaveMaxLength(500);
        configurationBuilder.Properties<DateTime>().HaveColumnType("DateTime");
    }
}
