using DbCourseWork.Models.Enums;

namespace DbCourseWork.Models;

public record CardSearchParam(int Page, int PageSize, List<SortingField>? OrderFields = null) : SearchParameters(Page, PageSize);