using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using TulaHack.API.Contracts;
using TulaHack.Application.Authentification;
using TulaHack.Application.Services;

namespace TulaHack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UsersService _userService;
        private readonly PasswordHasher _passwordHasher;

        public UserController(UsersService userService, PasswordHasher passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        [HttpGet("{id=guid}")]
        public async Task<ActionResult<UserResponse>> GetUser(Guid id)
        {
            var user = await _userService.GetById(id);

            if (user == null) return BadRequest("User not found");

            var response = new UserResponse(user.Id, user.Login, user.Role, user.FirstName, user.LastName, user.MiddleName, user.Phone);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateUser([FromBody] UserRequest request)
        {
            var user = Core.Models.User.Create(
                Guid.NewGuid(), 
                request.Login,
                request.Password,
                request.Role,
                request.FirstName, 
                request.LastName, 
                request.MiddleName, 
                request.Phone
                );

            if (user.IsFailure) return BadRequest(user.Error);

            if (await _userService.GetByLogin(user.Value.Login) != null) return BadRequest("User already exist");

            await _userService.RegisterUser(Core.Models.User.Create(
                user.Value.Id,
                user.Value.Login,
                _passwordHasher.Generate(user.Value.Password),
                user.Value.Role,
                user.Value.FirstName,
                user.Value.LastName,
                user.Value.MiddleName,
                user.Value.Phone
                ).Value);

            var token = await _userService.LoginUser(user.Value.Login, user.Value.Password);

            if (token.IsFailure) return BadRequest(token.Error);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(1),
            };

            Response.Cookies.Append("TulaHack", token.Value, cookieOptions);

            return Ok("Login successful");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequest request)
        {
            var token = await _userService.LoginUser(request.Login, request.Password);

            if (token.IsFailure) return BadRequest(token.Error);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(1),
            };

            Response.Cookies.Append("TulaHack", token.Value, cookieOptions);

            return Ok("Login successful");
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> AuthUser()
        {
            if (!Request.Cookies.TryGetValue("TulaHack", out var stringToken)) return BadRequest("Invalid token");

            var token = new JwtSecurityToken(stringToken);
            var isTokenVerified = await _userService.AuthUser(stringToken);

            if (isTokenVerified == null) return BadRequest("Invalid token");

            var newToken = new JwtSecurityToken(
            claims: token.Claims,
            signingCredentials: token.SigningCredentials,
            expires: DateTime.UtcNow.AddHours(120));

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(1),
            };

            Response.Cookies.Append("TulaHack", new JwtSecurityTokenHandler().WriteToken(newToken), cookieOptions);

            return Ok("Login successful");
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] UserRequest request)
        {
            var user = Core.Models.User.Create(
                id, 
                request.Login,
                request.Password,
                request.Role,
                request.FirstName,
                request.LastName,
                request.MiddleName,
                request.Phone
                );

            if (user.IsFailure)
            {
                return BadRequest(user.Error);
            }

            await _userService.UpdateUser(
                user.Value.Id,
                user.Value.FirstName,
                user.Value.LastName,
                user.Value.MiddleName,
                user.Value.Phone);

            return Ok(user.Value.Id);
        }
    }
}
