using Core.Enums;

namespace Core.Models.Systems;

public record SortingField(string Field, SortOrder Order = SortOrder.ASC);