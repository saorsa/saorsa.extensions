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

    public static string ToCamelCase(this string source)
    {
        var kebab = source.ToKebabCase();
        var split = kebab.Split('_', StringSplitOptions.RemoveEmptyEntries);
        var chunks = split.Select((chunk, idx) =>
        {
            if (idx == 0)
            {
                if(chunk.Length > 1)
                {
                    return char.ToLowerInvariant(chunk[0]) + chunk[1..];
                }
                return chunk;
            }
            if(chunk.Length > 1)
            {
                return char.ToUpperInvariant(chunk[0]) + chunk[1..];
            }
            return chunk;
        });
        return string.Join(string.Empty, chunks);
    }

    public static string ToPascalCase(this string source)
    {
        var kebab = source.ToKebabCase();
        var split = kebab.Split('_', StringSplitOptions.RemoveEmptyEntries);
        var chunks = split.Select((chunk) =>
        {
            if(chunk.Length > 1)
            {
                return char.ToUpperInvariant(chunk[0]) + chunk[1..];
            }
            return chunk.ToUpperInvariant();
        });
        return string.Join(string.Empty, chunks);
    }
    
    public static string ToKebabCase(this string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return source;   
        }

        source = source.Trim();

        var trimmed = source.TrimStart('_');
        var trimmedCount = source.Length - trimmed.Length;

        var result = Regex.Replace(
                trimmed,
                "(?<!^)(_?[A-Z][a-z]|(?<=_?[a-z])[A-Z0-9])",
                //"_$1",
                m => m.Value.StartsWith('_') ? m.Value : $"_{m.Value}",
                RegexOptions.Compiled)
            .Trim()
            .ToLower();
        
        return trimmedCount == 0
            ? result
            : $"{new string('_', trimmedCount)}{result}";
    }
}
