using System.Text;
using Core.Enums;
using Core.Models.Systems;

namespace Core.Models.Reports;

public record RouteReport
{
    public RouteNumber Number { get; init; }

    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }

    public DateTime GeneratedAt { get; init; }

    public long AmountOfPassengerByBankCard { get; init; }

    public long AmountOfPassengerByTravelCard { get; init; }

    public long TotalAmountOfPassenger => AmountOfPassengerByBankCard + AmountOfPassengerByTravelCard;

    public decimal SumOfBankTransactions { get; init; }

    public long UniqueByBankCard { get; init; }

    public long UniqueByTravelCard { get; init; }

    public long AllUniquePassenger => UniqueByBankCard + UniqueByTravelCard;

    public PerDayReport? PerDay { get; init; }

    public PerHourReport? PerHour { get; init; }

    private RouteReport() { }

    public static RouteReport Create(RouteReportParam reportParam, IEnumerable<PassengersForSource> passengersForSources,
        PerDayReport dayReport, PerHourReport hourReport)
    {
        IEnumerable<PassengersForSource> sources =
            passengersForSources as PassengersForSource[] ?? passengersForSources.ToArray();
        
        if (sources.Count() > 2)
            
            throw new ArgumentException("There should be max 2 sources");

        var bankCardPassengers = sources.FirstOrDefault(x => x.Source == PaymentType.BankCard);
        var travelCardPassengers = sources.FirstOrDefault(x => x.Source == PaymentType.TravelCard);

        if (bankCardPassengers is null && travelCardPassengers is null)
            throw new ArgumentException("There should be at least one source");

        return new RouteReport
        {
            Number = reportParam.Number,
            StartDate = reportParam.Start,
            EndDate = reportParam.End,
            GeneratedAt = DateTime.Now,
            AmountOfPassengerByBankCard = bankCardPassengers?.Passengers ?? 0,
            AmountOfPassengerByTravelCard = travelCardPassengers?.Passengers ?? 0,
            SumOfBankTransactions = bankCardPassengers?.Amount ?? 0,
            UniqueByBankCard = bankCardPassengers?.UniquePassengers ?? 0,
            UniqueByTravelCard = travelCardPassengers?.UniquePassengers ?? 0,
            PerDay = dayReport,
            PerHour = hourReport,
        };
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Report for route {Number}");
        sb.AppendLine($"From {StartDate} to {EndDate}");
        sb.AppendLine($"Generated at {GeneratedAt}");
        sb.AppendLine($"Total passengers: {TotalAmountOfPassenger}");
        sb.AppendLine($"Total unique passengers: {AllUniquePassenger}");
        sb.AppendLine($"Total amount of bank transactions: {SumOfBankTransactions}");
        sb.AppendLine($"Total passengers by bank card: {AmountOfPassengerByBankCard}");
        sb.AppendLine($"Total unique passengers by bank card: {UniqueByBankCard}");
        sb.AppendLine($"Total passengers by travel card: {AmountOfPassengerByTravelCard}");
        sb.AppendLine($"Total unique passengers by travel card: {UniqueByTravelCard}");
        sb.AppendLine($"Per day report: {PerDay}");
        sb.AppendLine($"Per hour report: {PerHour}");
        return sb.ToString();
    }
}