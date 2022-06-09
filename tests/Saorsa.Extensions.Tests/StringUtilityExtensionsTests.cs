namespace Saorsa.Extensions.Tests;

public class StringUtilityExtensionsTests
{
    [TestCase("user@organisation.org")]
    [TestCase("user-13618@organisation.org")]
    [TestCase("user.13618@organisation.org")]
    [TestCase("user-name-13618@organisation.org")]
    [TestCase("user-name-13618@sub.organisation.org")]
    [TestCase("13223-user-name@sub.organisation.org")]
    public void TestValidEmail(string emailString)
    {
        var isValid = emailString.IsValidEmail();
        Assert.True(isValid);
    }
    
    [TestCase("???user@organisation")]
    [TestCase("user-organisation")]
    [TestCase("user.organisation.com")]
    [TestCase("user.sub.organisation.com")]
    public void TestInvalidEmail(string emailString)
    {
        var isValid = emailString.IsValidEmail();
        Assert.False(isValid);
    }

    [TestCase("string with spaces")]
    [TestCase("query?string=124")]
    public void TestUrlEncodingAndDecoding(string origin)
    {
        var encoded = origin.UrlEncodedString();
        var decoded = encoded.UrlDecodedString();
        
        Assert.AreEqual(decoded, origin);
    }
    
    [TestCase(" ", "+")]
    [TestCase("/", "%2F")]
    [TestCase("?", "%3F")]
    [TestCase("=", "%3D")]
    public void TestUrlEncoding(string origin, string expected)
    {
        var encoded = origin.UrlEncodedString();
        Assert.AreEqual(encoded, expected);
    }
    
    [TestCase( "+", " ")]
    [TestCase("%2F", "/")]
    [TestCase("%3F", "?")]
    [TestCase("%3D", "=")]
    public void TestUrlDecoding(string origin, string expected)
    {
        var decoded = origin.UrlDecodedString();
        Assert.AreEqual(decoded, expected);
    }
    
    [TestCase("<b>bold</b>")]
    public void TestHtmlEncodingAndDecoding(string origin)
    {
        var encoded = origin.HtmlEncodedString();
        var decoded = encoded.HtmlDecodedString();
        
        Assert.AreEqual(decoded, origin);
    }
    
    [TestCase("<", "&lt;")]
    [TestCase(">", "&gt;")]
    public void TestHtmlEncoding(string origin, string expected)
    {
        var encoded = origin.HtmlEncodedString();
        Assert.AreEqual(encoded, expected);
    }
    
    [TestCase("&lt;", "<")]
    [TestCase("&gt;", ">")]
    public void TestHtmlDecoding(string origin, string expected)
    {
        var decoded = origin.HtmlDecodedString();
        Assert.AreEqual(decoded, expected);
    }

    [TestCase("", "")]
    [TestCase("A", "a")]
    [TestCase("MethodName", "method_name")]
    [TestCase("MethodName98", "method_name_98")]
    [TestCase("MethodName98Again", "method_name_98_again")]
    [TestCase("Method11Name98Again", "method_11_name_98_again")]
    [TestCase("methodName", "method_name")]
    [TestCase("AnotherMethodName", "another_method_name")]
    [TestCase("_AnotherMethodName", "_another_method_name")]
    [TestCase("__AnotherMethodName", "__another_method_name")]
    [TestCase("Method_Name", "method_name")]
    [TestCase("Method__Name", "method__name")]
    [TestCase("Method_name", "method_name")]
    [TestCase("method_name", "method_name")]
    [TestCase("method__name", "method__name")]
    [TestCase("Method__name", "method__name")]
    [TestCase("Method___name", "method___name")]
    [TestCase("_methodName", "_method_name")]
    [TestCase("_methodName", "_method_name")]
    [TestCase(" MethodName", "method_name")]
    [TestCase("methodName ", "method_name")]
    public void TestToKebabCase(string origin, string expected)
    {
        var result = origin.ToKebabCase();
        
        Assert.AreEqual(expected, result);
    }

    [TestCase("method", "method")]
    [TestCase("Method", "method")]
    [TestCase("Method123", "method123")]
    [TestCase("CaseMethod", "caseMethod")]
    [TestCase("CaseMethod123", "caseMethod123")]
    [TestCase(" method", "method")]
    [TestCase("method ", "method")]
    [TestCase("method12", "method12")]
    [TestCase("kebab_case", "kebabCase")]
    [TestCase("item_1", "item1")]
    [TestCase("a_dog", "aDog")]
    [TestCase("kebab_case_123", "kebabCase123")]
    public void TestToCamelCase(string origin, string expected)
    {
        var result = origin.ToCamelCase();
        Assert.AreEqual(expected, result);
    }

    [TestCase("method", "Method")]
    [TestCase("Method", "Method")]
    [TestCase("Method123", "Method123")]
    [TestCase("CaseMethod", "CaseMethod")]
    [TestCase("CaseMethod123", "CaseMethod123")]
    [TestCase(" method", "Method")]
    [TestCase("method ", "Method")]
    [TestCase("method12", "Method12")]
    [TestCase("kebab_case", "KebabCase")]
    [TestCase("item_1", "Item1")]
    [TestCase("a_dog", "ADog")]
    [TestCase("kebab_case_123", "KebabCase123")]
    public void TestToPascalCase(string origin, string expected)
    {
        var result = origin.ToPascalCase();
        Assert.AreEqual(expected, result);
    }
}
