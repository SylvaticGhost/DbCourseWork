using System.Text;
using Ardalis.GuardClauses;
using DbCourseWork.Data;
using DbCourseWork.Models;
using DbCourseWork.Models.Primitives;
using DbCourseWork.Utils;

namespace DbCourseWork.Repositories;

public abstract class ReadOnlyRepository<TEntity>(DataContext dataContext) where TEntity : IReadOnlyDbEntity
{
    protected abstract SortingField DefaultSortingField { get; }
    
    protected abstract string CollectionName { get; }
    
    public Task<IEnumerable<TEntity>> Get(SearchParameters parameters)
    {
        var sb = new StringBuilder().AppendLine($"SELECT * FROM {CollectionName}");
        
        parameters.IfEmptyApplySorting(DefaultSortingField);
        sb.AppendLine("ORDER BY");
        
        Guard.Against.Null(parameters.OrderFields);
        foreach (var field in parameters.OrderFields)
        {
            sb.AppendLine($"{field.Field} {field.Order}");
            if (field != parameters.OrderFields.Last())
                sb.AppendLine(",");
        }
        
        sb.AppendLine("LIMIT @PageSize OFFSET @Offset");
        var sqlParams = DynamicParametersExtensions.Pagination(parameters.Page, parameters.PageSize);

        if (parameters.HasWhereClause)
            sb.AppendLine($"WHERE {parameters.WhereClause}");

        var sql = sb.ToString();

        return dataContext.LoadData<TEntity>(sql, sqlParams);
    }
}