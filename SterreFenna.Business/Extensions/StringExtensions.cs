using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static string ReplaceSpaces(this string value)
        {
            return value.Replace(" ", "-");
        }

        public static bool IsSameAs(this string value, string otherValue)
        {
            return value.Equals(otherValue, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
