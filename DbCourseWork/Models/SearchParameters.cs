using Ardalis.GuardClauses;
using DbCourseWork.Models.Enums;

namespace DbCourseWork.Models;

public record SearchParameters
{
    public List<SortingField>? OrderFields { get; private set; }
    public string? WhereClause { get; }
    public int Page { get; init; }
    public int PageSize { get; init; }

    public SearchParameters(int page,
        int pageSize,
        List<SortingField>? orderFields = null,
        string? whereClause = null)
    {
        Guard.Against.NegativeOrZero(page, nameof(page));
        Guard.Against.NegativeOrZero(pageSize, nameof(pageSize));
        Page = page;
        PageSize = pageSize;
        OrderFields = orderFields;
        WhereClause = whereClause;
    }

    public bool HasWhereClause => !string.IsNullOrWhiteSpace(WhereClause);
       
    public void IfEmptyApplySorting(SortingField sortingField)
    {
        if (OrderFields is null || OrderFields.Count == 0)
            OrderFields = [sortingField];
    }
}