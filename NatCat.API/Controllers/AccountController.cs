using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NatCat.API.Service;
using NatCat.Application.Queries.Users;
using NatCat.DAL.Entity;
using NatCat.Model.Auth;
using NatCat.Model.StaticData;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NatCat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _apiSettings;

        public AccountController(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> apiSettings) : base(httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _apiSettings = apiSettings.Value;
        }

        [Authorize]
        [HttpGet("GetUserByProfileName")]
        public ActionResult<IEnumerable<UserDto>> GetUserByProfileName(string qry)
        {
            if (qry == null || qry.Length < 3) return BadRequest("Search query must be at least three characters long.");

            var users = _userManager.Users
                .Where(x => x.ProfileName.ToUpper().Contains(qry.ToUpper()));

            if (users == null) return NoContent();

            var retUsers = users.Select(x => new UserDto
            {
                Id = x.Id,
                Email = x.Email,
                ProfileName = x.ProfileName
            });

            return Ok(retUsers);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            
            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                ProfileName = user.ProfileName
            };
        }

        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] SignUpRequestDTO signUpRequestDTO)
        {
            if (signUpRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            ApplicationUser user = new()
            {
                //TODO integrate and use automapper
                UserName = signUpRequestDTO.Email,
                Email = signUpRequestDTO.Email,
                ProfileName = signUpRequestDTO.ProfileName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, signUpRequestDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegistrationSuccessful = false,
                    ErrorMessages = result.Errors.Select(s => s.Description)
                });
            }

            var roleToAdd = signUpRequestDTO.AdminOverride ? StaticData.ROLE_ADMIN : StaticData.ROLE_USER;
            var roleResult = await _userManager.AddToRoleAsync(user, roleToAdd);
            if (!roleResult.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegistrationSuccessful = false,
                    ErrorMessages = roleResult.Errors.Select(s => s.Description)
                });
            }

            return StatusCode(201);
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
        {
            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.Email, signInRequestDTO.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(signInRequestDTO.Email);
                if (user == null)
                {
                    return Unauthorized(new SignInResponseDTO()
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Invalid authorisation"
                    });
                }
                else
                {
                    var signingCredentials = GetSigningCredentials();
                    var claims = await GetClaimsAsync(user);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: _apiSettings.ValidIssuer,
                        audience: _apiSettings.ValidAudience,
                        claims: claims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: signingCredentials);

                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    return Ok(new SignInResponseDTO()
                    {
                        IsAuthSuccessful = true,
                        Token = token,
                        userDTO = new UserDto()
                        {
                            Id = user.Id,
                            Email = user.Email,
                            ProfileName = user.ProfileName,
                        }
                    });
                }
            }
            else
            {
                return Unauthorized(new SignInResponseDTO()
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid authorisation"
                });
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.ProfileName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id)
            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}


