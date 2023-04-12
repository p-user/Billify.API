using Billify.API.Common.Authentication;
using Billify.API.Common.Models;
using Billify.API.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Billify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager= userManager;
            _roleManager= roleManager;
            _configuration= configuration;

        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser model)
        {
            var userExists=await _userManager.FindByNameAsync(model.UserName);
            if(userExists !=null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status="Error", Message="This user is already registered!" });
            ApplicationUser user=new ApplicationUser() {
                Email=model.Email,
                SecurityStamp=Guid.NewGuid().ToString(), 
                UserName=model.UserName,
                Id=Guid.NewGuid().ToString(),
            };
            var result=await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status="Error", Message="Some internal error occured!" });
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Supervisor))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Supervisor));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Operator))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Operator));
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user,model.RoleSelected);
            return Ok(new Response { Status="Success", Message="User created successfully!" });
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser model)
        {
            var user=await _userManager.FindByNameAsync(model.UserName);
            if(user !=null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles= await _userManager.GetRolesAsync(user);
            
            var authClaims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
            foreach(var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token=new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience:_configuration["JWT:Audience"],
                expires:DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256));
            return Ok(new { token=new JwtSecurityTokenHandler().WriteToken(token), expiration=token.ValidTo, User=user.UserName });
            }
            return Unauthorized();
        }


    }
}
