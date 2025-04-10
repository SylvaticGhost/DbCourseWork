using DbCourseWork.Models.Enums;

namespace DbCourseWork.Models;

public record SortingField(string Field, SortOrder Order = SortOrder.ASC);