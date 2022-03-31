using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Data.Entity;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork<MessengerContext> unitOfWork;

        public AuthController(IUnitOfWork<MessengerContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  Авторизация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("/token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
        public IActionResult Login(string? login, string? password)
        {
            if (login == null || password == null || login.Length == 0 || password.Length == 0)
            {
                return UnprocessableEntity("Введено пустое поле");
            }

            if (password.Length < 8)
            {
                return UnprocessableEntity("Пароль должен содержать 8 символов");
            }

            var identity = GetIdentity(login, password);
            if (identity == null)
            {
                return Unauthorized("Некорректный пароль или логин");
            }

            var encodedJwt = CreateToken(identity);

            var response = new UserDTO
            {
                Token = encodedJwt,
                Username = identity.Name,
                IsAdmin = identity.Claims.Where(c => c.Type == identity.RoleClaimType)
                    .FirstOrDefault(x => x.Value == "Admin") != null
            };

            return Ok(response);
        }
        

        /// <summary>
        /// Создание пользователя. По умолчанию доступ User
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("/token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
        public IActionResult CreateUser(string login, string password)
        {
            if (password.Length < 8)
            {
                return UnprocessableEntity("Пароль должен содержать 8 символов");
            }

            if (login.Length == 0 || password.Length == 0)
            {
                return UnprocessableEntity("Введено пустое поле");
            }

            var user = unitOfWork.DbContext.Users.FirstOrDefault(x => x.Name == login);
            if (user != null)
            {
                return UnprocessableEntity("Аккаунт с таким логином уже существует");
            }

            var newUser = unitOfWork.DbContext.Users.Add(new User
            {
                Name = login,
                Password = CalcHash(password),
            });
            unitOfWork.DbContext.SaveChanges();

            var identity = GetIdentity(login, password);
            var encodedJwt = CreateToken(identity);

            var response = new UserDTO
            {
                Token = encodedJwt,
                Username = identity.Name,
                IsAdmin = identity.Claims.Where(c => c.Type == identity.RoleClaimType)
                    .FirstOrDefault(x => x.Value == "Admin") != null
            };

            return Ok(response);
        }

        private string? CalcHash(string s)
        {
            using var hash = SHA256.Create();
            return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(s)).Select(x => x.ToString("X2")));
        }

        private ClaimsIdentity? GetIdentity(string username, string password)
        {
            var hashPass = CalcHash(password);
            var person = unitOfWork.DbContext.Users.FirstOrDefault(x => x.Name == username && x.Password == hashPass);
            // если пользователь не найден
            if (person == null) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Status) //TODO
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private string CreateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
