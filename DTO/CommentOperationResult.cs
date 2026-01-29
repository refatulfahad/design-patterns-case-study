namespace design_pattern_case_1.DTO
{
    public class CommentOperationResult
    {
        public bool IsAllowed { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool OperationPerformed { get; set; }
    }
}
