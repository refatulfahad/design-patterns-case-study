using design_pattern_case_1.Features.Reports.Domain.Templates;

namespace design_pattern_case_1.Features.Reports.Factories
{
    public class ReportFactory : IReportFactory
    {
        private readonly Dictionary<string, Func<ReportGeneration>> _reportCreators;

        public ReportFactory()
        {
            // Register report types
            _reportCreators = new Dictionary<string, Func<ReportGeneration>>(StringComparer.OrdinalIgnoreCase)
            {
                { "pdf", () => new PdfReport() },
                { "html", () => new HtmlReport() }
            };
        }

        public ReportGeneration CreateReport(string reportType)
        {
            if (!_reportCreators.ContainsKey(reportType))
            {
                throw new ArgumentException($"Unsupported report type: {reportType}. Supported types: {string.Join(", ", GetSupportedTypes())}");
            }

            return _reportCreators[reportType]();
        }

        public IEnumerable<string> GetSupportedTypes()
        {
            return _reportCreators.Keys;
        }
    }
}
