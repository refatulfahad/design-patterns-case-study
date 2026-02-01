using design_pattern_case_1.Features.Comments.Domain;

namespace design_pattern_case_1.Features.Comments.Domain.Observers
{
    /// <summary>
    /// Observer Pattern: Observers react to comment state changes
    /// </summary>
    public interface ICommentObserver
    {
        Task OnCommentDisabledAsync(Comment comment, string reason);
    }
}
