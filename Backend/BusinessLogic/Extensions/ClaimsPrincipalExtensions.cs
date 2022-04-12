using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLogic.Response
{
    public static class ClaimsIdentityExtensions
    {
        public static int GetUserId(this ClaimsPrincipal pricipal) =>
            Convert.ToInt32(pricipal?.FindFirst(JwtRegisteredClaimNames.Jti).Value);
    }
}