using Core.Models;
using Core.Models.Systems;
using Dapper;
using Data.Abstracrtions;
using Data.Context;

namespace Data.Repositories;

public class CardOwnerRepository(DataContext dataContext) : Repository<CardOwner>(dataContext),ICardOwnerRepository
{
    private readonly DataContext _dataContext = dataContext;

    public Task<CardOwner?> Find(int id)
    {
        const string sql = @"SELECT * FROM cards_owners
                            WHERE id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        
        return _dataContext.LoadDataSingle<CardOwner?>(sql, parameters);
    }

    protected override SortingField DefaultSortingField => new("id");
    protected override string CollectionName => "cards_owners";
    protected override string[] Columns => CardOwner.Columns;
}