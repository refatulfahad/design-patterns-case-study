using DotnetBadWordDetector;

namespace design_pattern_case_1.Services
{
    public class BadWordCheckerService
    {
        public bool ContainsBadWords(string text)
        {
            var detector = new ProfanityDetector();
            if (detector.IsPhraseProfane(text)) { return true; }
            return false;
        }
    }
}
