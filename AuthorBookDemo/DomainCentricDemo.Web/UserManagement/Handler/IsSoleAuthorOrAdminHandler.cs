using DomainCentricDemo.Web.Infrastructure.Dto;
using DomainCentricDemo.Web.UserManagement.Requirement;
using Microsoft.AspNetCore.Authorization;

namespace DomainCentricDemo.Web.UserManagement.Handler;

public class IsSoleAuthorOrAdminHandler :
    AuthorizationHandler<IsSoleAuthorOrAdminRequirement, BookDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        IsSoleAuthorOrAdminRequirement requirement, BookDto resource)
    {
        // Admin - succes
        if (context.User.HasClaim(c => c.Type == ClaimsTypes.Admin))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        // Not Author - fail
        if (!context.User.HasClaim(c => c.Type == ClaimsTypes.Author))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        // More than one author - fail
        if (resource.Authors.Count > 1)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        // If not author of book - fail
        
        if (resource.Authors.FirstOrDefault()?.Id != context.User.Identity?.Name)
        {
            context.Fail();
            return Task.CompletedTask;
        }


        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}