using design_pattern_case_1.Features.Posts.Domain;

namespace design_pattern_case_1.Features.Posts.Domain.Filters
{
    public interface IPostFilter
    {
        bool IsPassed(Post post);
        void SetNext(IPostFilter nextFilter);
    }
}
