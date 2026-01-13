namespace design_pattern_case_1.Domain.Report
{
    public class HtmlReport : ReportGeneration
    {
        protected override string GetDefaultTitle()
        {
            return "HTML Report";
        }

        public override string GetFormat()
        {
            return "text/html";
        }

        protected override void FormatContent(Report report)
        {
            // HTML-specific formatting
            report.Content = $@"
                            <!DOCTYPE html>
                            <html>
                            <head>
                                <title>{report.Title}</title>
                                <style>
                                    body {{ font-family: Arial, sans-serif; margin: 20px; }}
                                    h1 {{ color: #333; }}
                                    .content {{ border: 1px solid #ddd; padding: 15px; }}
                                </style>
                            </head>
                            <body>
                                <h1>{report.Title}</h1>
                                <div class='content'>
                                    {report.Content.Replace("\n", "<br/>")}
                                </div>
                            </body>
                            </html>";
        }

        // Optional: Override hook method for HTML-specific finalization
        protected override void FinalizeReport(Report report)
        {
            // HTML doesn't need footer in content since it's in the HTML structure
            // Skip base implementation
        }
    }
}
