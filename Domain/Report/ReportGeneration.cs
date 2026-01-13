namespace design_pattern_case_1.Domain.Report
{
    /// <summary>
    /// Template Method Pattern: Defines the skeleton of the report generation algorithm
    /// </summary>
    public abstract class ReportGeneration
    {
        // Template Method - defines the algorithm structure (sealed to prevent override)
        public Report GenerateReport(string? customTitle = null, Dictionary<string, string>? customData = null)
        {
            // Step 1: Initialize base data
            var report = InitializeReport(customTitle);
            
            // Step 2: Prepare content (hook method - can be overridden)
            PrepareContent(report, customData);
            
            // Step 3: Format content (abstract - must be implemented by subclasses)
            FormatContent(report);
            
            // Step 4: Finalize (hook method - optional override)
            FinalizeReport(report);
            
            return report;
        }

        // Concrete method - same for all reports
        protected Report InitializeReport(string? customTitle)
        {
            return new Report
            {
                Title = customTitle ?? GetDefaultTitle(),
                DateGenerated = DateTime.UtcNow,
                Content = string.Empty
            };
        }

        // Hook method - can be overridden but has default implementation
        protected virtual void PrepareContent(Report report, Dictionary<string, string>? customData)
        {
            if (customData != null && customData.Count > 0)
            {
                report.Content = string.Join("; ", customData.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
            }
            else
            {
                report.Content = "This is sample report content.";
            }
        }

        // Abstract method - must be implemented by subclasses
        protected abstract void FormatContent(Report report);

        // Hook method - optional customization point
        protected virtual void FinalizeReport(Report report)
        {
            // Default: Add footer with generation timestamp
            report.Content += $"\n\nGenerated at: {report.DateGenerated:yyyy-MM-dd HH:mm:ss}";
        }

        // Abstract method - each report type has its own default title
        protected abstract string GetDefaultTitle();

        // Abstract method - each report type defines its format
        public abstract string GetFormat();
    }
}
