using System.Security.Claims;

namespace Saorsa;


/// <summary>
/// Extensions for the System.Security.Claims class.
/// </summary>
public static class ClaimsUtilityExtensions
{
    /// <summary>
    /// Filters a source collection of claims.
    /// </summary>
    /// <param name="claims">The source claims collection. Required.</param>
    /// <param name="claimType">The type of the claims to be filtered out. Required.</param>
    public static IEnumerable<Claim> Filter(
        this IEnumerable<Claim> claims,
        string claimType)
    {
        return claims
            .Where(c => c.Type.Equals(claimType));
    }

    /// <summary>
    /// Filters a source collection of claims.
    /// </summary>
    /// <param name="claims">The source claims collection. Required.</param>
    /// <param name="claimType">The type of the claims to be filtered out. Required.</param>
    /// <param name="claimValue">The value for the claim type to be matched. Required.</param>
    public static IEnumerable<Claim> Filter(
        this IEnumerable<Claim> claims,
        string claimType,
        string claimValue)
    {
        return claims
            .Where(c => c.Type.Equals(claimType))
            .Where(c => c.Value.Equals(claimValue));
    }

    /// <summary>
    /// Returns the first element from a source collection of claims that matches the specified filter criteria,
    /// NULL if no matches.
    /// </summary>
    /// <param name="claims">The source claims collection. Required.</param>
    /// <param name="claimType">The type of the claims to be filtered out. Required.</param>
    public static Claim? FirstOrDefault(
        this IEnumerable<Claim> claims,
        string claimType)
    {
        return claims
            .Filter(claimType)
            .FirstOrDefault();
    }

    /// <summary>
    /// Returns the first element from a source collection of claims that matches the specified filter criteria,
    /// NULL if no matches.
    /// </summary>
    /// <param name="claims">The source claims collection. Required.</param>
    /// <param name="claimType">The type of the claims to be filtered out. Required.</param>
    /// <param name="claimValue">The value for the claim type to be matched. Required.</param>
    public static Claim? FirstOrDefault(
        this IEnumerable<Claim> claims,
        string claimType,
        string claimValue)
    {
        return claims
            .Filter(claimType, claimValue)
            .FirstOrDefault();
    }
}
