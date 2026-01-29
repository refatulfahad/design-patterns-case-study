namespace design_pattern_case_1.Domain.Comment
{
    public class LiveState : CommentState
    {
        public override bool CanEdit()
        {
            return false; 
        }

        public override bool CanDelete()
        {
            return true;
        }

        protected override string GetStateName()
        {
            return "Live";
        }
    }
}

