using Utils.Attributes;

namespace Core.Enums;

public enum Operators
{
   KpKyivMetropoliten = 1,
   KpKyivPassTrans = 2,
}

public static class OperatorsExtensions
{
   private static readonly IReadOnlyDictionary<Operators, string> OperatorName = new Dictionary<Operators, string>()
   {
      {Operators.KpKyivMetropoliten, "КП \"Київський метрополітен\""},
      {Operators.KpKyivPassTrans, "КП \"Київпастранс\""},
   };

   [DisplayNameConversion]
   public static string ToOfficialName(this Operators op) => OperatorName.GetValueOrDefault(op, "Невідомий перевізник");
}