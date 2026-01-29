using design_pattern_case_1.Domain.Comment;

namespace design_pattern_case_1.Factory
{
    public class CommentStateFactory
    {
        public static CommentState CreateState(Enum.CommentState state)
        {
            return state switch
            {
                Enum.CommentState.Pending => new PendingState(),
                Enum.CommentState.Live => new LiveState(),
                Enum.CommentState.Userful => new UsefulState(),
                _ => throw new ArgumentOutOfRangeException(nameof(state), $"Invalid comment state: {state}")
            };
        }
    }
}
