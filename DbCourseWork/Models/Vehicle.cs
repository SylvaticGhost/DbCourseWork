using DbCourseWork.Models.Enums;

namespace DbCourseWork.Models;

public record Vehicle
{
    public int Number { get; private set; }

    public VehicleType Type { get; private set; }

    public Vehicle(int number, VehicleType type)
    {
        Number = number;
        Type = type;
    }

    public Vehicle(decimal number, short type) : this((int)number, (VehicleType)type) { }
}