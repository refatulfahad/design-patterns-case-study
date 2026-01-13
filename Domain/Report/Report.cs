namespace design_pattern_case_1.Domain.Report
{
    public class Report
    {
        public string Title { get; set; } = string.Empty;
        public DateTime DateGenerated { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
