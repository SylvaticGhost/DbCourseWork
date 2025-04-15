namespace Core.Interfaces;

public interface IInsertableEntity 
{
    public string AsSqlRow();
    
    public static string CollectionName { get; }
}