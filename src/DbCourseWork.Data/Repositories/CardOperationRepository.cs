using Core.Models;
using Core.Models.Systems;
using Data.Abstracrtions;
using Data.Context;
using Data.Utils;

namespace Data.Repositories;

public class CardOperationRepository(DataContext dataContext)
    : Repository<CardOperation>(dataContext), ICardOperationRepository
{
    private readonly DataContext _dataContext = dataContext;

    protected override SortingField DefaultSortingField { get; } = new("card");
    protected override string CollectionName => "card_operation";

    public Task<IEnumerable<CardOperation>> GetForCard(long card)
    {
        const string sql = "SELECT * FROM card_operation WHERE card = @Card";
        var parameters = DynamicParametersExtensions.WithSingleParameter("Card", card);
        return _dataContext.LoadData<CardOperation>(sql, parameters);
    }

    public Task<IEnumerable<RideCardOperation>> GetRidesForCard(long card)
    {
        const string sql = """
                           SELECT o.card, o.date, o.change, r.id, r.vehicle, r.route FROM card_operation o
                                    JOIN public.rides r on o.ride = r.id
                                    WHERE o.card = @Card
                           """;

        var parameters = DynamicParametersExtensions.WithSingleParameter("Card", card);
        return _dataContext.LoadData<RideCardOperation>(sql, parameters);
    }
}