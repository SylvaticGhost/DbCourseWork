using DbCourseWork.Models.Enums;

namespace DbCourseWork.Models;

public record CardSearchParam(int Page, int PageSize, List<SortingField<CardOrderField>>? OrderFields = null) : SearchParameters(Page, PageSize);