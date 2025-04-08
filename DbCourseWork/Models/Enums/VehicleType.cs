namespace DbCourseWork.Models.Enums;

public enum VehicleType
{
    Троллейбус = 1,
    Автобус = 2,
    Трамвай = 3,
    Метро = 4,
}

public static class VehicleMapper
{
    private static readonly IReadOnlyDictionary<string, VehicleType> VehicleAbbreviation = new Dictionary<string, VehicleType>()
    {
        {"Т", VehicleType.Трамвай},
        {"А", VehicleType.Автобус},
        {"ТР", VehicleType.Троллейбус},
        {"М", VehicleType.Метро},
    };

    public static VehicleType GetFromAbbreviation(string abbreviation)
    {
        if(VehicleAbbreviation.TryGetValue(abbreviation.ToUpper(), out var vehicleType))
            return vehicleType;
        
        throw new ArgumentException($"Unknown vehicle type: {abbreviation}");
    }

    public static readonly VehicleType[] AllValues = Enum.GetValues<VehicleType>();
    
    public static bool IsPrefixValid(string prefix) => VehicleAbbreviation.ContainsKey(prefix.ToUpper());
}