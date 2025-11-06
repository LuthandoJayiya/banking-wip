using log4net;
using MELLBankRestAPI.AuthModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MELLBankRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("ApplicationUserController");

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("Login")] //api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserID", user.Id.ToString())
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SigningKey));
                    var tempIssuer = _appSettings.JWT_Site_URL;
                    int expiryInMinutes = Convert.ToInt32(_appSettings.ExpiryInMinutes);

                    var userToken = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                        );
                    var roles = await _userManager.GetRolesAsync(user);

                    logger.InfoFormat("Post : /api/ApplicationUser/Login model.UserName: {0} - {1} ", model.UserName, roles.First());

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(userToken),
                        expiration = userToken.ValidTo,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        userName = user.UserName,
                        roles
                    });
                }
                else
                {
                    return BadRequest(new { message = "Invalid username or password." });
                }
            }
            catch (Exception ex)
            {
                logger.Error("Post : /api/ApplicationUser/Login: " + ex);
                return BadRequest(new { message = "An error occurred while trying to login. Please try again later.", error = ex.Message });
            }
        }
    }
}
