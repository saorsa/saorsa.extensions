using System.Net;
using System.Text.RegularExpressions;

namespace Saorsa;

public static class StingUtilityExtensions
{
    const string EmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    static readonly Regex EmailRegex = new (EmailPattern, RegexOptions.IgnoreCase);
    
    public static bool IsValidEmail(this string email)
    {
        return EmailRegex.IsMatch(email);
    }

    public static string UrlEncodedString(this string source)
    {
        return WebUtility.UrlEncode(source);
    }
    
    public static string UrlDecodedString(this string source)
    {
        return WebUtility.UrlDecode(source);
    }
    
    public static string HtmlEncodedString(this string source)
    {
        return WebUtility.HtmlEncode(source);
    }
    
    public static string HtmlDecodedString(this string source)
    {
        return WebUtility.HtmlDecode(source);
    }
}
