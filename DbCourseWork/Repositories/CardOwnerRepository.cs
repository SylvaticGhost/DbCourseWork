using Dapper;
using DbCourseWork.Data;
using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public class CardOwnerRepository(DataContext dataContext) : ICardOwnerRepository
{
    public Task<IEnumerable<CardOwner>> Search(int page = 1, int pageSize = 30, string search = "")
    {
        const string sql = @"SELECT * FROM cards_owners
                            ORDER BY id
                            LIMIT @PageSize OFFSET @Offset";

        var parameters = new DynamicParameters();
        parameters.Add("@PageSize", pageSize);
        parameters.Add("@Offset", (page - 1) * pageSize);

        return dataContext.LoadData<CardOwner>(sql, parameters);
    }
}