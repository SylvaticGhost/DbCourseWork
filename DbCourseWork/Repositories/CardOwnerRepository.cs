using Dapper;
using DbCourseWork.Data;
using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public class CardOwnerRepository(DataContext dataContext) : Repository<CardOwner>(dataContext),ICardOwnerRepository
{
    private readonly DataContext _dataContext = dataContext;

    public Task<IEnumerable<CardOwner>> Search(int page = 1, int pageSize = 30, string search = "")
    {
        const string sql = @"SELECT * FROM cards_owners
                            ORDER BY id
                            LIMIT @PageSize OFFSET @Offset";

        var parameters = new DynamicParameters();
        parameters.Add("@PageSize", pageSize);
        parameters.Add("@Offset", (page - 1) * pageSize);

        return _dataContext.LoadData<CardOwner>(sql, parameters);
    }

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