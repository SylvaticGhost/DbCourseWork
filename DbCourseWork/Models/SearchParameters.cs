using DbCourseWork.Models.Enums;

namespace DbCourseWork.Models;

public record SearchParameters(int Page, int PageSize, SortOrder SortOrder = SortOrder.Descending);