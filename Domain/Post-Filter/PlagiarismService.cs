namespace design_pattern_case_1.Domain.Post_Filter
{
    public class PlagiarismService: IPostFilter
    {
        private IPostFilter? _nextFilter;

        public void SetNext(IPostFilter nextFilter)
        {
            _nextFilter = nextFilter;
        }

        public bool IsPassed(Entity.Post post)
        {
            // Implement plagiarism checker
            bool passed = true;
            if (!passed)
            {
                return false;
            }
            return _nextFilter?.IsPassed(post) ?? true;
        }
    }
}
