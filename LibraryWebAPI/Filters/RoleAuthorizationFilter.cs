using LibraryWebAPI.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Security.Claims;

namespace LibraryWebAPI.Filters
{
    public class RoleAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool tokenFlag = context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues outValue);

            if (tokenFlag)
            {
                var identity = context.HttpContext.User.Identity as ClaimsIdentity;

                //角色權限
                var role = identity.FindFirst(ClaimTypes.Role)?.Value;
                var id = identity.FindFirst("id")?.Value;
                var account = identity.FindFirst("account")?.Value;
                var email = identity.FindFirst(ClaimTypes.Email)?.Value;

                if(role == "admin")
                {
                    context.Result = new JsonResult(new CommonResponse()
                    {
                       
                    });
                }
            }
            else
            {

            }
        }
    }
}
