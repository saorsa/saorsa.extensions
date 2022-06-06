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
}
