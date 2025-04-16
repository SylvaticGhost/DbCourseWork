using Core.Enums;

namespace Core.Models.Reports;

public record AllHourlyUsage : ITimeUsage
{
    public int Hour { get; init; }

    public long PassengersByTravelCard { get; init; }

    public long PassengersByBankCard { get; init; }

    public long Passengers => PassengersByTravelCard + PassengersByBankCard;
    
    private AllHourlyUsage() { }

    public static AllHourlyUsage Create(HourRowData[] row)
    {
        if (row.Length > 2)
            throw new ArgumentException("There should be max 2 rows");

        var travelCardRoute = row.FirstOrDefault(x => x.Source == PaymentType.TravelCard.ToString());
        var bankCardRoute = row.FirstOrDefault(x => x.Source == PaymentType.BankCard.ToString());
        
        if(travelCardRoute is not null && bankCardRoute is not null && travelCardRoute.Hour != bankCardRoute.Hour)
            throw new ArgumentException("Rows should have the same hour");

        return new AllHourlyUsage
        {
            Hour = travelCardRoute?.Hour ??
                   bankCardRoute?.Hour ?? throw new ArgumentException("Rows should have the same hour"),
            PassengersByTravelCard = Convert.ToUInt32(travelCardRoute?.Passengers),
            PassengersByBankCard = Convert.ToUInt32(bankCardRoute?.Passengers)
        };
    }
}