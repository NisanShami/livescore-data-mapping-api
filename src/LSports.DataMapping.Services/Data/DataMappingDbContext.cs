using LSports.DataMapping.Abstractions.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace LSports.DataMapping.Services.Data;

public class DataMappingDbContext : DbContext
{
    public DataMappingDbContext(DbContextOptions<DataMappingDbContext> options) : base(options)
    {
    }

    public DbSet<PeriodMapping> PeriodMappings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure PeriodMapping entity
        modelBuilder.Entity<PeriodMapping>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.ProviderId, e.SportId, e.ProviderPeriod }).IsUnique();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        });
    }
}
