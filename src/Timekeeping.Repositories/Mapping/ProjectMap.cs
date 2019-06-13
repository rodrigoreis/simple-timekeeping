using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timekeeping.Repositories.Abstractions.Models;

namespace Timekeeping.Repositories.Mapping
{
    public class ProjectMap : IEntityTypeConfiguration<ProjectModel>
    {
        public void Configure(EntityTypeBuilder<ProjectModel> builder)
        {
            builder
                .ToTable("projects");

            builder
                .Property(m => m.Id)
                .HasColumnName("project_id")
                .HasColumnType("INTEGER")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(m => m.Name)
                .HasColumnName("project_name")
                .HasColumnType("TEXT")
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(m => m.JobJourneyCharge)
                .HasColumnName("project_job_journey_charge")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder
                .HasMany(m => m.TimeEntries)
                .WithOne(n => n.Project)
                .HasForeignKey(m => m.ProjectId)
                .HasConstraintName("fk_projects_time_entries_project_id");

            builder
                .HasMany(m => m.Users)
                .WithOne(n => n.Project)
                .HasForeignKey(m => m.ProjectId)
                .HasConstraintName("fk_projects_users_project_id");

            builder
                .HasKey(m => m.Id)
                .HasName("pk_projects_project_id");

            builder
                .HasIndex(m => m.Name)
                .HasName("ix_projects_project_name");
        }
    }
}
