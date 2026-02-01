using design_pattern_case_1.Features.Reports.DTOs;
using design_pattern_case_1.Features.Reports.Factories;

namespace design_pattern_case_1.Features.Reports.Services
{
    public class ReportService
    {
        private readonly IReportFactory _reportFactory;

        public ReportService(IReportFactory reportFactory)
        {
            _reportFactory = reportFactory;
        }

        public GenerateReportResponse GenerateReport(GenerateReportRequest request)
        {
            // Use factory to create appropriate report instance
            var reportGenerator = _reportFactory.CreateReport(request.ReportType);

            // Use Template Method to generate report
            var report = reportGenerator.GenerateReport(request.Title, request.Data);

            // Map to response DTO
            return new GenerateReportResponse
            {
                ReportType = request.ReportType,
                Title = report.Title,
                GeneratedAt = report.DateGenerated,
                Content = report.Content,
                Format = reportGenerator.GetFormat()
            };
        }

        public IEnumerable<string> GetSupportedReportTypes()
        {
            return _reportFactory.GetSupportedTypes();
        }
    }
}
