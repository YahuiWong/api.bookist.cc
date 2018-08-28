using Anet;
using Bookist.Core;
using Bookist.Core.Dtos;
using Bookist.Core.Models;
using Bookist.Core.Repositories;
using Bookist.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bookist.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserService _userService;
        private readonly UserRepository _userRepository;
        public AccountController(
            IOptions<JwtOptions> jwtOptionsAccessor,
            UserService userService,
            UserRepository userRepository)
        {
            _jwtOptions = jwtOptionsAccessor.Value;
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPost("[action]")]
        [AdminAuthorize]
        public async Task<JwtResult> Login(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByUserName(dto.UserName);

            if (user == null || !PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            {
                throw new BadRequestException("户名或密码错误！");
            }

            return GenerateToken(user.Id, user.UserName, Role.Admin.ToString());
        }

        #region Private Methods

        private JwtResult GenerateToken(long userid, string username, string role)
        {
            var userIdentity = new ClaimsIdentity(new GenericIdentity(userid.ToString()),
                new Claim[] { new Claim(ClaimTypes.Role, role) });

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(userIdentity.Claims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(_jwtOptions.Expiration),
                signingCredentials: signingCredentials
            );

            return new JwtResult
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresIn = _jwtOptions.Expiration
            };
        }

        #endregion
    }
}
