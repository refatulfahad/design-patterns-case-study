using design_pattern_case_1.DTO;
using design_pattern_case_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace design_pattern_case_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Generate a report based on the specified type (PDF, HTML, etc.)
        /// </summary>
        [HttpPost]
        public IActionResult GenerateReport([FromBody] GenerateReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = _reportService.GenerateReport(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while generating the report", details = ex.Message });
            }
        }

        /// <summary>
        /// Get list of supported report types
        /// </summary>
        [HttpGet("types")]
        public IActionResult GetSupportedTypes()
        {
            var types = _reportService.GetSupportedReportTypes();
            return Ok(new { supportedTypes = types });
        }
    }
}
