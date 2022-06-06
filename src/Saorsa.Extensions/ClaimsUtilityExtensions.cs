using System.Security.Claims;

namespace Saorsa;

public static class ClaimsUtilityExtensions
{
    public static IEnumerable<Claim> Filter(
        this IEnumerable<Claim> claims,
        string claimType)
    {
        return claims
            .Where(c => c.Type.Equals(claimType));
    }
    
    public static IEnumerable<Claim> Filter(
        this IEnumerable<Claim> claims,
        string claimType,
        string claimValue)
    {
        return claims
            .Where(c => c.Type.Equals(claimType))
            .Where(c => c.Value.Equals(claimValue));
    }
    
    public static Claim? FirstOrDefault(
        this IEnumerable<Claim> claims,
        string claimType)
    {
        return claims
            .Filter(claimType)
            .FirstOrDefault();
    }
    
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
