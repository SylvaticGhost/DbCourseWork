using System.Text;

namespace Core.Models.DTOs;

public record ImportDataDto
{
    public IEnumerable<CardOperation>? CardOperations { get; set; }

    public IEnumerable<BankTransaction>? BankTransactions { get; set; }
    
    public IEnumerable<Ride>? Rides { get; set; }
    
    public IEnumerable<CardOwner>? CardOwners { get; set; }
    
    public IEnumerable<TravelCard>? TravelCards { get; set; }
    
    public bool HasRides => Rides != null && Rides.Any();
    public bool HasCardOperations => CardOperations != null && CardOperations.Any();
    public bool HasBankTransactions => BankTransactions != null && BankTransactions.Any();
    public bool HasCardOwners => CardOwners != null && CardOwners.Any();
    public bool HasTravelCards => TravelCards != null && TravelCards.Any();
    
    public void Clear()
    {
        CardOperations = null;
        BankTransactions = null;
        Rides = null;
        CardOwners = null;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Rides: {Rides?.Count() ?? 0}");
        sb.AppendLine($"Cards: {CardOperations?.Count() ?? 0}");
        sb.AppendLine($"BankTransactions: {BankTransactions?.Count() ?? 0}");
        sb.AppendLine($"CardOwners: {CardOwners?.Count() ?? 0}");
        sb.AppendLine($"TravelCards: {TravelCards?.Count() ?? 0}");
        return sb.ToString();
    }
}