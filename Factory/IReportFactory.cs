using design_pattern_case_1.Domain.Report;

namespace design_pattern_case_1.Factory
{
    public interface IReportFactory
    {
        ReportGeneration CreateReport(string reportType);
        IEnumerable<string> GetSupportedTypes();
    }
}
