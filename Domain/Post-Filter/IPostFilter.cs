namespace design_pattern_case_1.Domain.Post_Filter
{
    public interface IPostFilter
    {
        bool IsPassed(Entity.Post post);
        void SetNext(IPostFilter nextFilter);
    }
}
