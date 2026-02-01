using design_pattern_case_1.Features.Posts.Domain;

namespace design_pattern_case_1.Features.Posts.Domain.Filters.GrammarCheckAdapter
{
    public class GrammerCheckService
    {
        public string ContentLength(Post post)
        {
            //GrammerCheckService: Content length is less or equal to 600 characters.=> passed
            //Content length is greater to 600 characters. => failed
            return "passed";
        }
    }
}
