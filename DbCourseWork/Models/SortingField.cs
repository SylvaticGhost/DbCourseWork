using DbCourseWork.Models.Enums;

namespace DbCourseWork.Models;

public record SortingField<TEnum>(TEnum Field, SortOrder Order = SortOrder.Ascending) where TEnum : Enum;