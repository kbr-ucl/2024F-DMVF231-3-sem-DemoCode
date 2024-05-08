using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DomainCentricDemo.Web.UserManagement.Extension
{
    public static class UserManagementExtensions
    {
        public static async Task<IdentityResult> AddOrUpdateClaimAsync(this ClaimsPrincipal principal,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, string claimType,
            string claimValue)
        {
            var user = await userManager.GetUserAsync(principal);

            IdentityResult result;

            var oldClaim = principal.Claims.FirstOrDefault(a => a.Type == claimType);
            if (oldClaim != null)
            {
                result = await userManager.RemoveClaimAsync(user, oldClaim);
                if (result != IdentityResult.Success) return result;

                principal.Identities.FirstOrDefault()?.RemoveClaim(oldClaim);
            }

            var claim = new Claim(claimType, claimValue, ClaimValueTypes.String);

            result = await userManager.AddClaimAsync(user, claim);
            if (result != IdentityResult.Success) return result;

            principal.Identities.FirstOrDefault()?.AddClaim(claim);
            if (result.Succeeded) await signInManager.SignInAsync(user, false);

            return result;
        }

        public static async Task<IdentityResult> DeleteClaimAsync(this ClaimsPrincipal principal,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, string claimType)
        {
            var user = await userManager.GetUserAsync(principal);

            IdentityResult result;

            var oldClaim = principal.Claims.FirstOrDefault(a => a.Type == claimType);
            if (oldClaim == null) throw new ArgumentException("Claim findes ikke");

            result = await userManager.RemoveClaimAsync(user, oldClaim);
            if (result != IdentityResult.Success) return result;

            principal.Identities.FirstOrDefault()?.RemoveClaim(oldClaim);
            await signInManager.SignInAsync(user, false);

            return result;
        }
    }
}

