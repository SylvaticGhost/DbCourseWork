namespace Core.Interfaces;

public interface IUpsertParam<out TCreateDto>
{
    public TCreateDto ToCreateDto();
    
    public void Clear();
}