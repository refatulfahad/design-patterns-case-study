using design_pattern_case_1.Features.Posts.Domain;

namespace design_pattern_case_1.Features.Posts.Domain.Filters
{
    public class GeminiService: IPostFilter
    {
        private IPostFilter? _nextFilter;

        public void SetNext(IPostFilter nextFilter)
        {
            _nextFilter = nextFilter;
        }

        public bool IsPassed(Post post)
        {
            // Implement brevity checker
            bool passed = true;
            if (!passed)
            {
                return false;
            }
            return _nextFilter?.IsPassed(post) ?? true;
        }
    }
}
