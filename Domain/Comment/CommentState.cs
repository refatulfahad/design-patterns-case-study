using design_pattern_case_1.Data;
using design_pattern_case_1.DTO;

namespace design_pattern_case_1.Domain.Comment
{
    public abstract class CommentState
    {
        public abstract bool CanEdit();
       
        public abstract bool CanDelete();
       
        public virtual CommentOperationResult Edit(int commentId, AppDbContext dbContext)
        {
            if (!CanEdit())
            {
                return new CommentOperationResult
                {
                    IsAllowed = false,
                    Message = $"Edit is not allowed for comments in {GetStateName()} state.",
                    OperationPerformed = false
                };
            }

            return new CommentOperationResult
            {
                IsAllowed = true,
                Message = $"Edit is allowed for comments in {GetStateName()} state.",
                OperationPerformed = false 
            };
        }

        public virtual async Task<CommentOperationResult> DeleteAsync(int commentId, AppDbContext dbContext)
        {
            if (!CanDelete())
            {
                return new CommentOperationResult
                {
                    IsAllowed = false,
                    Message = $"Delete is not allowed for comments in {GetStateName()} state.",
                    OperationPerformed = false
                };
            }

            var comment = await dbContext.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return new CommentOperationResult
                {
                    IsAllowed = true,
                    Message = "Comment not found.",
                    OperationPerformed = false
                };
            }

            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();

            return new CommentOperationResult
            {
                IsAllowed = true,
                Message = $"Comment deleted successfully from {GetStateName()} state.",
                OperationPerformed = true
            };
        }

        protected abstract string GetStateName();
    }
}

