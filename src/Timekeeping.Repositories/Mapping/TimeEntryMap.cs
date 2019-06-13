using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timekeeping.Repositories.Abstractions.Models;

namespace Timekeeping.Repositories.Mapping
{
    public class TimeEntryMap : IEntityTypeConfiguration<TimeEntryModel>
    {
        public void Configure(EntityTypeBuilder<TimeEntryModel> builder)
        {
            builder
                .ToTable("time_entries");

            builder
                .Property(m => m.Id)
                .HasColumnName("time_entry_id")
                .HasColumnType("INTEGER")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(m => m.ProjectId)
                .HasColumnName("project_id")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder
                .Property(m => m.UserId)
                .HasColumnName("user_id")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder
                .Property(m => m.Date)
                .HasColumnName("time_entry_date")
                .HasColumnType("DATETIME")
                .IsRequired();

            builder
                .Property(m => m.Amount)
                .HasColumnName("time_entry_amount")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder
                .HasKey(m => m.Id)
                .HasName("pk_time_entries_time_entry_id");

            builder
                .HasIndex(m => m.Date)
                .HasName("ix_time_entries_time_entry_date");
        }
    }
}
