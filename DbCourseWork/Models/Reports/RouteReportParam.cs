namespace DbCourseWork.Models.Reports;

public record RouteReportParam(RouteNumber Number, DateOnly Start, DateOnly End)
{
    public string StartAsString => Start.ToString("yyyy-MM-dd");
    
    public string EndAsString => End.ToString("yyyy-MM-dd");
}