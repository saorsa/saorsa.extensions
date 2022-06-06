using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace Saorsa.Extensions.Tests;

public class ClaimsPrincipalExtensionsTests
{
    [Test]
    public void TestClaimsFilterByType()
    {
        var random = new Random();
        var max = random.Next(5, 50);
        var expectedFilterClaimTypes =
            (int)Math.Ceiling(max * 1.0 / 4);
            
        var claims = new List<Claim>();
        var filterClaimType = GetRandomString();
        for (var i = 0; i < max; i++)
        {
            claims.Add(
                i % 4 == 0 ?
                    new Claim(filterClaimType, GetRandomString()) :
                    new Claim(GetRandomString(), GetRandomString()));
        }

        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(claims));

        var matches = claimsPrincipal
            .FilterClaims(filterClaimType)
            .ToList();
        
        Assert.IsNotEmpty(matches);
        Assert.AreEqual(
            matches.Count,
            expectedFilterClaimTypes);
    }

    [Test]
    public void TestClaimsFilterByTypeNoMatches()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        var matches = claimsPrincipal.FilterClaims(
            "non-existent-claim-type");
        Assert.IsEmpty(matches);
    }
    
    [Test]
    public void TestClaimsFilterByTypeAndValue()
    {
        var random = new Random();
        var max = random.Next(5, 50);
        var expectedFilterClaimTypes =
            (int)Math.Ceiling(max * 1.0 / 4);
            
        var claims = new List<Claim>();
        var filterClaimType = GetRandomString();
        var filterClaimValue = GetRandomString();
        for (var i = 0; i < max; i++)
        {
            claims.Add(
                i % 4 == 0 ?
                    new Claim(filterClaimType, filterClaimValue) :
                    new Claim(GetRandomString(), GetRandomString()));
        }

        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(claims));

        var matches = claimsPrincipal
            .FilterClaims(filterClaimType, filterClaimValue)
            .ToList();
        
        Assert.IsNotEmpty(matches);
        Assert.AreEqual(
            matches.Count,
            expectedFilterClaimTypes);
    }

    [Test]
    public void TestClaimsFilterByTypeAndValueNoMatches()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        var matches = claimsPrincipal.FilterClaims(
            "non-existent-claim-type",
            "non-existent-claim-value");
        Assert.IsEmpty(matches);
    }

    [Test]
    public void TestFirstClaimOrDefaultByType()
    {
        var random = new Random();
        var max = random.Next(5, 50);
            
        var claims = new List<Claim>();
        var filterClaimType = GetRandomString();
        for (var i = 0; i < max; i++)
        {
            claims.Add(
                i % 4 == 0 ?
                    new Claim(filterClaimType, GetRandomString()) :
                    new Claim(GetRandomString(), GetRandomString()));
        }
        
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(claims));
        var match = claimsPrincipal
            .FirstClaimOrDefault(filterClaimType);

        Assert.IsNotNull(match);
        Assert.AreEqual(
            match!.Type,
            filterClaimType);
    }
    
    [Test]
    public void TestFirstClaimOrDefaultByTypeNoMatch()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        var match = claimsPrincipal.FirstClaimOrDefault(
            "non-existent-claim-type");
        Assert.IsNull(match);
    }
    
    [Test]
    public void TestFirstClaimOrDefaultByTypeAndValue()
    {
        var random = new Random();
        var max = random.Next(5, 50);
            
        var claims = new List<Claim>();
        var filterClaimType = GetRandomString();
        var filterClaimValue = GetRandomString();
        for (var i = 0; i < max; i++)
        {
            claims.Add(
                i % 4 == 0 ?
                    new Claim(filterClaimType, filterClaimValue) :
                    new Claim(GetRandomString(), GetRandomString()));
        }
        
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(claims));
        var match = claimsPrincipal
            .FirstClaimOrDefault(filterClaimType, filterClaimValue);

        Assert.IsNotNull(match);
        Assert.AreEqual(
            match!.Type,
            filterClaimType);
        Assert.AreEqual(
            match.Value,
            filterClaimValue);
    }
    
    [Test]
    public void TestFirstClaimOrDefaultByTypeAndValueNoMatch()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        var match = claimsPrincipal.FirstClaimOrDefault(
            "non-existent-claim-type",
            "non-existent-claim-value");
        Assert.IsNull(match);
    }
    
    private string GetRandomString()
    {
        return Guid.NewGuid().ToString("N");
    }
}
