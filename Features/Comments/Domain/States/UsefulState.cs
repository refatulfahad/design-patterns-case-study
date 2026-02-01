namespace design_pattern_case_1.Features.Comments.Domain.States
{
    public class UsefulState : CommentState
    {
        public override bool CanEdit()
        {
            return false;
        }

        public override bool CanDelete()
        {
            return false;
        }

        protected override string GetStateName()
        {
            return "Useful";
        }
    }
}

