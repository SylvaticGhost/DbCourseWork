using System.Text;

namespace DbCourseWork.Models;

public record ImportDataDto
{
    public IEnumerable<CardOperation>? CardOperations { get; set; }

    public IEnumerable<BankTransaction>? BankTransactions { get; set; }
    
    public IEnumerable<Ride>? Rides { get; set; }
    
    public bool HasRides => Rides != null && Rides.Any();
    public bool HasCards => CardOperations != null && CardOperations.Any();
    public bool HasBankTransactions => BankTransactions != null && BankTransactions.Any();
    
    public void Clear()
    {
        CardOperations = null;
        BankTransactions = null;
        Rides = null;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Rides: {Rides?.Count() ?? 0}");
        sb.AppendLine($"Cards: {CardOperations?.Count() ?? 0}");
        sb.AppendLine($"BankTransactions: {BankTransactions?.Count() ?? 0}");
        return sb.ToString();
    }
}