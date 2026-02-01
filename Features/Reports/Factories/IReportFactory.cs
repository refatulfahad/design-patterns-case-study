using design_pattern_case_1.Features.Reports.Domain.Templates;

namespace design_pattern_case_1.Features.Reports.Factories
{
    public interface IReportFactory
    {
        ReportGeneration CreateReport(string reportType);
        IEnumerable<string> GetSupportedTypes();
    }
}
