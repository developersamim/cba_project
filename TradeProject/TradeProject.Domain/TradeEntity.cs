using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeProject.Domain;

public class TradeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    [StringLength(3, MinimumLength = 3)]
    public string SecurityCode { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal Amount { get; set; }
    public TradeType Type { get; set; }
    public TradeStatus Status { get; set; }

    public AccountEntity Account { get; set; }

    public TradeEntity()
    {
        Timestamp = DateTime.UtcNow;
        Status = TradeStatus.Placed;
    }
}
