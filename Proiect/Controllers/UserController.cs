using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proiect.Models;
using Proiect.Models.DTOs;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
namespace Proiect.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		public static User user = new User();
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;

		public UserController(IConfiguration configuration, IUserService userService)
        {
			_configuration = configuration;
			_userService = userService;
		}
		[HttpGet, Authorize]
		public ActionResult<object> GetMe()
		{
			var userName = _userService.GetMyName();
			return Ok(userName);
		}

        [HttpPost("register")]
		public async Task<ActionResult<User>> Register(UserDto request)
		{
			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
			user.PasswordSalt = passwordSalt;
			user.PasswordHash = passwordHash;
			user.UserName = request.UserName;
			return Ok(user);
		}
		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(UserDto request)
		{
			if (user.UserName != request.UserName)
			{
				return BadRequest("user not found");

			}

			if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
			{
				return BadRequest("Wrong password");
			}
			string token = CreateToken(user);
			var RefreshToken = GenerateRefreshToken();
			SetRefreshToken(RefreshToken);
			return Ok(token);
		}

		[HttpPost ("refresh-token")]
		public async Task <ActionResult<string>> RefreshToken()
		{
			var refreshToken = Request.Cookies["refreshToken"];
			if (!user.Token.Equals(refreshToken))
			{
				return Unauthorized("Invalid Refresh Token");
			}
			else if(user.TokenExpires< DateTime.Now)
			{
				return Unauthorized("Token expired");
			}
			string token = CreateToken(user);
			var newRefreshToken = GenerateRefreshToken();
			SetRefreshToken(newRefreshToken);
			return Ok(token);
		}
		private RefreshToken GenerateRefreshToken()
		{
			var refreshToken = new RefreshToken
			{
				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
				TokenExpires=DateTime.Now.AddDays(7),
				TokenCreated=DateTime.Now
			};
			return refreshToken;
		}
		private void SetRefreshToken(RefreshToken newRefreshToken)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = newRefreshToken.TokenExpires
			};
			Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
			user.Token = newRefreshToken.Token;
			user.TokenCreated = newRefreshToken.TokenCreated;
			user.TokenExpires = newRefreshToken.TokenExpires;
		}
		private string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Role, "Admin"),
				new Claim(ClaimTypes.Role, "User")
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(10),
				signingCredentials: creds
				);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;

		}
		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				
			}
		}
		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using(var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}
	}
}
