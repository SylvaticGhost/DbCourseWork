using Core.Enums;

namespace Core.Models.Reports;

public record PassengersForSource
{
    public long Passengers { get; init; }

    public long UniquePassengers { get; init; }

    public decimal Amount { get; init; }

    public PaymentType Source { get; init; }

    public PassengersForSource(long passengers, long uniq, decimal amount, string source)
    {
        var paymentType = Enum.TryParse<PaymentType>(source, out var parsedSource)
            ? parsedSource
            : throw new InvalidDataException("Invalid payment type");
        
        Passengers = passengers;
        UniquePassengers = uniq;
        Amount = amount;
        Source = paymentType;
    }
}