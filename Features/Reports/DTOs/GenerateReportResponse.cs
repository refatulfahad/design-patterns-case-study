namespace design_pattern_case_1.Features.Reports.DTOs
{
    public class GenerateReportResponse
    {
        public string ReportType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
    }
}
