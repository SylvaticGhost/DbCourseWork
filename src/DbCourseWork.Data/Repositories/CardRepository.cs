using Core.Models;
using Core.Models.Systems;
using Dapper;
using Data.Abstracrtions;
using Data.Context;

namespace Data.Repositories;

public class CardRepository(DataContext dataContext) : Repository<TravelCard>(dataContext),ICardRepository
{
    private readonly DataContext _dataContext1 = dataContext;

    public Task<TravelCard?> Find(long code)
    {
        const string sql = "SELECT * FROM travel_cards WHERE code = @code";
        var parameters = new DynamicParameters();
        parameters.Add("code", code);
        return _dataContext1.LoadDataSingle<TravelCard?>(sql, parameters);
    }

    public Task<IEnumerable<TravelCard>> GetCardForUser(int userId)
    {
        const string sql = "SELECT * FROM travel_cards WHERE owner = @userId";
        var parameters = new DynamicParameters();
        parameters.Add("userId", userId);
        return _dataContext1.LoadData<TravelCard>(sql, parameters);
    }

    protected override SortingField DefaultSortingField { get; } = new("code");
    protected override string CollectionName => "travel_cards";
    protected override string[] Columns => TravelCard.Columns;
}