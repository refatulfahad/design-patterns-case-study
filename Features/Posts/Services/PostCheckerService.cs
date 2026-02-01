using design_pattern_case_1.Features.Posts.Domain;
using design_pattern_case_1.Features.Posts.Domain.Filters;
using design_pattern_case_1.Features.Posts.Domain.Filters.GrammarCheckAdapter;

namespace design_pattern_case_1.Features.Posts.Services
{
    public class PostCheckerService
    {
        private readonly IPostFilter _firstFilter;
        public PostCheckerService()
        {
            var grammerCheckAdapter = new GrammarCheckAdapterService();
            var plagiarismService = new PlagiarismService();
            var grammerlyService = new GrammerlyService();
            var geminiService = new GeminiService();
            grammerCheckAdapter.SetNext(plagiarismService);
            plagiarismService.SetNext(grammerlyService);
            grammerlyService.SetNext(geminiService);
            _firstFilter = grammerCheckAdapter;
        }
        public bool CheckPost(Post post)
        {
            return _firstFilter.IsPassed(post);
        }
    }
}
