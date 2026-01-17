using design_pattern_case_1.Entity;

namespace design_pattern_case_1.Domain.Post_Filter
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
