using System.ComponentModel.DataAnnotations;

namespace design_pattern_case_1.Features.Reports.DTOs
{
    public class GenerateReportRequest
    {
        [Required]
        public string ReportType { get; set; } = string.Empty;
        
        public string? Title { get; set; }
        
        public Dictionary<string, string>? Data { get; set; }
    }
}
