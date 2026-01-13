using design_pattern_case_1.Data;
using design_pattern_case_1.Domain.Pricing;
using design_pattern_case_1.Entity.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace design_pattern_case_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BundlesController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public BundlesController(AppDbContext applicationDbContext)
        {
            this.appDbContext = applicationDbContext;
        }

        //Composite Design Pattern
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBundleById(int id)
        {
            var bundles = await appDbContext.Bundles
                 .Include(b => b.Books)
                 .ToListAsync();

            var bundle = BuildTreeInMemory(id, bundles);
            if (bundle == null)
            {
                return NotFound();
            }

            BundleDetailsService bundleDetailsService = new BundleDetailsService();
            var BookDetails = bundleDetailsService.GetDetails(bundle);

            return Ok(BookDetails);
        }


        private Bundle? BuildTreeInMemory(int rootId, List<Bundle> allBundles)
        {
            var root = allBundles.FirstOrDefault(b => b.Id == rootId);
            if (root == null)
                return null;

            root.ChildBundles = allBundles
                                .Where(b => b.BundleId == root.Id)
                                .ToList();

            foreach (var child in root.ChildBundles)
            {
                BuildTreeInMemory(child.Id, allBundles);
            }
            return root;
        }
    }
}
