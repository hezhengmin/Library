﻿using Microsoft.AspNetCore.Mvc.Filters;
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
                var role = identity.FindFirst("Role").Value;

            }
            else
            {

            }
        }
    }
}
