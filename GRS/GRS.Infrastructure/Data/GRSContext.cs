using GRS.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRS.Infrastructure.Data
{
    public class GRSContext : DbContext
    {
        public GRSContext(DbContextOptions<GRSContext> options)
            : base(options) { }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Skill>(ConfigureSkill);
            builder.Entity<Candidate>(ConfigureCandidate);
            builder.Entity<CandidateSkill>(ConfigureCandidateSkill);
        }


        void ConfigureSkill(EntityTypeBuilder<Skill> builder)
        {
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);            
        }

        void ConfigureCandidate(EntityTypeBuilder<Candidate> builder)
        {            
            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);       
        }

        void ConfigureCandidateSkill(EntityTypeBuilder<CandidateSkill> builder)
        {
            builder.Property(c => c.CandidateId)
               .IsRequired();
            builder.Property(c => c.SkillId)
               .IsRequired();
            builder.ToTable("CandidateSkills");            
        }
    }
}
