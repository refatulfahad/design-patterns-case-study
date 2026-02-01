namespace design_pattern_case_1.Features.Reports.Domain.Templates
{
    public class PdfReport : ReportGeneration
    {
        protected override string GetDefaultTitle()
        {
            return "PDF Report";
        }

        public override string GetFormat()
        {
            return "application/pdf";
        }

        protected override void FormatContent(Report report)
        {
            // PDF-specific formatting
            report.Content = $"[PDF Document]\n" +
                           $"========================================\n" +
                           $"{report.Content}\n" +
                           $"========================================";
        }

        // Optional: Override hook method for PDF-specific finalization
        protected override void FinalizeReport(Report report)
        {
            base.FinalizeReport(report);
            report.Content += "\n[End of PDF Document]";
        }
    }
}
