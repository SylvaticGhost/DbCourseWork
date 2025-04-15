using Core.Models.Systems;

namespace Core.Models.Reports;

public record RouteReportParam(RouteNumber Number, DateOnly Start, DateOnly End);