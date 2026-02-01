using design_pattern_case_1.Features.Comments.Domain;

namespace design_pattern_case_1.Features.Comments.Domain.Observers
{
    /// <summary>
    /// Subject (Publisher): Manages observers and notifies them of comment events
    /// </summary>
    public class CommentSubject
    {
        private readonly List<ICommentObserver> _observers = new();

        public void Attach(ICommentObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"[CommentSubject] Observer attached: {observer.GetType().Name}");
            }
        }

        public void Detach(ICommentObserver observer)
        {
            _observers.Remove(observer);
            Console.WriteLine($"[CommentSubject] Observer detached: {observer.GetType().Name}");
        }

        public async Task NotifyCommentDisabledAsync(Comment comment, string reason)
        {
            Console.WriteLine($"[CommentSubject] Notifying {_observers.Count} observers about disabled comment {comment.CommentId}");
            
            foreach (var observer in _observers)
            {
                try
                {
                    await observer.OnCommentDisabledAsync(comment, reason);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CommentSubject] Observer {observer.GetType().Name} failed: {ex.Message}");
                    // Continue notifying other observers even if one fails
                }
            }
        }
    }
}
