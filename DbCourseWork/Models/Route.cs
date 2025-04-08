using DbCourseWork.Models.Enums;

namespace DbCourseWork.Models;

public record Route
{
    public string Number { get; init; }
    
    public string Name { get; init; }
    
    public int Operator { get; init; }

    public Route(string number, string name, short @operator)
    {
        Number = number;
        Name = name;
        Operator = @operator;
    }

    public VehicleType Vehicle => VehicleMapper.GetFromAbbreviation(GetPrefix(Number));

    public static string GetPrefix(string number) => new (number.TakeWhile(char.IsLetter).ToArray());
    
    public string OperatorFullName => ((Operators)Operator).ToOfficialName();
}