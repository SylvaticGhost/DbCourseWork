using Core.Enums;

namespace Core.Models.Reports;

public record AllDailyUsage : ITimeUsage
{
    public DayOfWeek DayOfWeek { get; init; }

    public long PassengersByTravelCard { get; init; }

    public long PassengersByBankCard { get; init; }

    public long Passengers => PassengersByTravelCard + PassengersByBankCard;

    public long UniquePassengersByTravelCard { get; init; }

    public long UniquePassengersByBankCard { get; init; }

    public long UniquePassengers => UniquePassengersByTravelCard + UniquePassengersByBankCard;

    private AllDailyUsage() { }

    public static AllDailyUsage Create(DayRowData[] row)
    {
        if (row.Length > 2)
            throw new ArgumentException("There should be max 2 rows");

        var travelCardRoute = row.FirstOrDefault(x => x.Source == PaymentType.TravelCard.ToString());
        var bankCardRoute = row.FirstOrDefault(x => x.Source == PaymentType.BankCard.ToString());

        if (travelCardRoute is not null && bankCardRoute is not null && travelCardRoute.Day != bankCardRoute.Day)
            throw new ArgumentException("Rows should have the same day");

        return new AllDailyUsage
        {
            DayOfWeek = travelCardRoute?.Day ??
                        bankCardRoute?.Day ?? throw new ArgumentException("Rows should have defined day"),
            PassengersByTravelCard = Convert.ToUInt32(travelCardRoute?.Passengers ?? 0),
            PassengersByBankCard = Convert.ToUInt32(bankCardRoute?.Passengers ?? 0),
            UniquePassengersByTravelCard = Convert.ToUInt32(travelCardRoute?.UniquePassengers ?? 0),
            UniquePassengersByBankCard = Convert.ToUInt32(bankCardRoute?.UniquePassengers ?? 0)
        };
    }
}