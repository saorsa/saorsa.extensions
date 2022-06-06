using System.Security.Claims;

namespace Saorsa;

public static class ClaimsPrincipalExtensions
{
    public static IEnumerable<Claim> FilterClaims(
        this ClaimsPrincipal principal,
        string claimType)
    {
        return principal.Claims
            .Where(c => c.Type.Equals(claimType));
    }
    
    public static IEnumerable<Claim> FilterClaims(
        this ClaimsPrincipal principal,
        string claimType,
        string claimValue)
    {
        return principal.Claims
            .Where(c => c.Type.Equals(claimType))
            .Where(c => c.Value.Equals(claimValue));
    }
    
    public static Claim? FirstClaimOrDefault(
        this ClaimsPrincipal principal,
        string claimType)
    {
        return principal
            .FilterClaims(claimType)
            .FirstOrDefault();
    }
    
    public static Claim? FirstClaimOrDefault(
        this ClaimsPrincipal principal,
        string claimType,
        string claimValue)
    {
        return principal
            .FilterClaims(claimType, claimValue)
            .FirstOrDefault();
    }
}
