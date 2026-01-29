using design_pattern_case_1.Entity;

namespace design_pattern_case_1.Observer
{
    /// <summary>
    /// Observer Pattern: Observers react to comment state changes
    /// </summary>
    public interface ICommentObserver
    {
        Task OnCommentDisabledAsync(Comment comment, string reason);
    }
}
