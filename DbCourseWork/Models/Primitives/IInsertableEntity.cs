namespace DbCourseWork.Models.Primitives;

public interface IInsertableEntity 
{
    public string AsSqlRow();
    
    public static string CollectionName { get; }
}