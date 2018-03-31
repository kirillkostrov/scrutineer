using System.Text.RegularExpressions;

namespace Core.Helpers
{
    public static class CodeParser
    {
        public static bool TryParseStandartCode(string rawString, out string standartCode)
        {
            var regexp = new Regex(@"(standard)?\s*\d{4}[\s|-]?\d{4}");
            standartCode = regexp.Match(rawString).Value;
            return regexp.IsMatch(rawString);
        }
        
        public static bool ParseHomologationCode(string rawString)
        {
            var regexp = new Regex(@"[a-zA-Z]{2,3}\.?\d{3}\.?\d{2}(-\w)?");
            return regexp.IsMatch(rawString);
        }
    }
}