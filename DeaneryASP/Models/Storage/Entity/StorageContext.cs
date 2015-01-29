namespace DeaneryASP.Models.Storage.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StorageContext : DbContext
    {
        public StorageContext()
            : base("name=Storage")
        {
        }

        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<GradeValues> GradeValues { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Realisations> Realisations { get; set; }
        public virtual DbSet<Registrations> Registrations { get; set; }
        public virtual DbSet<Semesters> Semesters { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grades>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Grades>()
                .HasMany(e => e.GradeValues)
                .WithRequired(e => e.Grades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GradeValues>()
                .Property(e => e.Value)
                .IsFixedLength();

            modelBuilder.Entity<GradeValues>()
                .Property(e => e.Date)
                .IsFixedLength();

            modelBuilder.Entity<GradeValues>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Groups>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Groups>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Groups)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Realisations>()
                .Property(e => e.Ver)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Realisations>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Realisations>()
                .HasMany(e => e.Grades)
                .WithRequired(e => e.Realisations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Realisations>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.Realisations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registrations>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Registrations>()
                .HasMany(e => e.GradeValues)
                .WithRequired(e => e.Registrations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semesters>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Semesters>()
                .Property(e => e.Active)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Semesters>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Semesters>()
                .HasMany(e => e.Realisations)
                .WithRequired(e => e.Semesters)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Students>()
                .Property(e => e.IndexNo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Students>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Students>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.Students)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subjects>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Subjects>()
                .HasMany(e => e.Realisations)
                .WithRequired(e => e.Subjects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.Students)
                .WithRequired(e => e.Users);
        }
    }
}
