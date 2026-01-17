using design_pattern_case_1.Entity;

namespace design_pattern_case_1.Services
{
    public class PostCheckerService
    {
        private readonly Domain.Post_Filter.IPostFilter _firstFilter;
        public PostCheckerService()
        {
            var grammerCheckAdapter = new Domain.Post_Filter.GrammarCheckAdapterService();
            var plagiarismService = new Domain.Post_Filter.PlagiarismService();
            var grammerlyService = new Domain.Post_Filter.GrammerlyService();
            var geminiService = new Domain.Post_Filter.GeminiService();
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
