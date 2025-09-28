using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LSports.DataMapping.Abstractions.Models.DataBase;

[Table("period_mappings")]
public class PeriodMapping
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("provider_id")]
    public int ProviderId { get; set; }

    [Column("provider_name")]
    [MaxLength(255)]
    public string ProviderName { get; set; } = string.Empty;

    [Column("sport_id")]
    public int SportId { get; set; }

    [Column("sport_name")]
    [MaxLength(255)]
    public string SportName { get; set; } = string.Empty;

    [Column("provider_period")]
    [MaxLength(255)]
    public string ProviderPeriod { get; set; } = string.Empty;

    [Column("lsports_period_id")]
    public int? LsportsPeriodId { get; set; }

    [Column("lsports_period_name")]
    [MaxLength(255)]
    public string? LsportsPeriodName { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Column("updated_date")]
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    [Column("updated_by")]
    [MaxLength(255)]
    public string UpdatedBy { get; set; } = string.Empty;
}
