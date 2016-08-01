using System.Text.RegularExpressions;

namespace SterreFenna.Business.Naming
{
    public class InvalidCharsRemover
    {
        public static string RemoveInvalidChars(string name)
        {
            var result = Regex.Replace(name, @"[^\w]*", "-");

            return result;
        }
    }
}