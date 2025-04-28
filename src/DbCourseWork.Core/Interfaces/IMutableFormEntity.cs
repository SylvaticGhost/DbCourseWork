namespace Core.Interfaces;

public interface IMutableFormEntity<TCreateDto, out TParam> : IFormTableEntity
    where TParam : IUpsertParam<TCreateDto>
{
    public TParam ToUpsertParam();
}