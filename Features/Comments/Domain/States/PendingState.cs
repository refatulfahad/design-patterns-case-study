namespace design_pattern_case_1.Features.Comments.Domain.States
{
    public class PendingState : CommentState
    {
        public override bool CanEdit()
        {
            return true; 
        }

        public override bool CanDelete()
        {
            return true; 
        }

        protected override string GetStateName()
        {
            return "Pending";
        }
    }
}

