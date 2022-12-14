using LabSchoolAPI.Enums;
using LabSchoolAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LabSchoolAPI.LabSchool
{
    public class LabSchoolContext: DbContext
    {
        public LabSchoolContext(DbContextOptions<LabSchoolContext> options): base (options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Pedagogo> Pedagogos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Professor>()
                .Property(x => x.Formacao)
                .HasConversion<string>()
                .HasMaxLength(100);

            modelBuilder.Entity<Professor>()
                .Property(x => x.Estado)
                .HasConversion<string>()
                .HasMaxLength(100);

            modelBuilder.Entity<Professor>()
                .Property(x => x.Experiencia)
                .HasConversion<string>()
                .HasMaxLength(100);

            modelBuilder.Entity<Aluno>()
                .Property(x => x.Situacao)
                .HasConversion<string>()
                .HasMaxLength(80);

            modelBuilder.Entity<Aluno>()
               .Property(x => x.Situacao)
               .HasConversion<string>()
               .HasMaxLength(80);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }


    }

    

}


