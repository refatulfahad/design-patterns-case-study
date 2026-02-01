namespace design_pattern_case_1.Features.Posts.Domain.Filters.GrammarCheckAdapter
{
    public class GrammarCheckAdapterService: IPostFilter
    {
        private readonly GrammerCheckService _grammerCheckService = new GrammerCheckService();
        private IPostFilter? _nextFilter;

        public void SetNext(IPostFilter nextFilter)
        {
            _nextFilter = nextFilter;
        }

        public bool IsPassed(Post post)
        {
            if (_grammerCheckService.ContentLength(post) == "passed")
            {
                return _nextFilter?.IsPassed(post) ?? true;
            }
            return false;
        }
    }
}
