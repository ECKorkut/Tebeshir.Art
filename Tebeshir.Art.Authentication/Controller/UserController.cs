
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Tebeshir.Art.Authentication.Business.Services.Interface;
using Tebeshir.Art.Authentication.DataLayer;
using Tebeshir.Art.Authentication.Domain.Models;
using Tebeshir.Art.Authentication.Model;


namespace Tebeshir.Art.Authentication.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IClaimsService _claimsService;
        private readonly IJwtTokenService _jwtTokenService;
        public UserController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager, IClaimsService claimsService,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _claimsService = claimsService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                var userClaims = await _claimsService.GetUserClaimsAsync(user);

                var token = _jwtTokenService.GetJwtToken(userClaims);

                return Ok(new UserLoginResultDTO
                {
                    Succeeded = true,
                    Token = new TokenDTO
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo
                    }
                });
            }

            return Unauthorized(new UserLoginResultDTO
            {
                Succeeded = false,
                Message = "The email and password combination was invalid."
            });
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            IdentityResult result;

            User newUser = new()
            {
                Email = userRegisterDTO.Email,
                UserName = userRegisterDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            result = await _userManager.CreateAsync(newUser, userRegisterDTO.Password);

            if (!result.Succeeded)
                return Conflict(new UserRegisterResultDTO
                {
                    Succeeded = result.Succeeded,
                    Errors = result.Errors.Select(e => e.Description)
                });
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin)) {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            result = await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return CreatedAtAction(nameof(Register), new UserRegisterResultDTO { Succeeded = true });
        }

    }
}
