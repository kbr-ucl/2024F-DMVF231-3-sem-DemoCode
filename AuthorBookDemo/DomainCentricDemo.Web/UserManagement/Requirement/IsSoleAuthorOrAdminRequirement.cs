using Microsoft.AspNetCore.Authorization;

namespace DomainCentricDemo.Web.UserManagement.Requirement;

public class IsSoleAuthorOrAdminRequirement
    : IAuthorizationRequirement
{
}