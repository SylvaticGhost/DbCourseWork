using System.Data;
using Dapper;

namespace Data.Context;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value) =>
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);

    public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);
}