namespace DbCourseWork.Models;

public record DataImportParams
{
    public List<Ride> Rides { get; set; } = [];
}