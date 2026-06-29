using Microsoft.IdentityModel.Tokens;
using Project.Api.Interfaces;
using Project.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Api.Services
{
    public class GenerateToken:IGenerateToken
    {
        public readonly IConfiguration _config;
        public  GenerateToken(IConfiguration config)
        {
            _config = config;
        }
        public string GetToken(User user)
        {
            var jwtsetting = _config.GetSection("Jwt");
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Uid.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("SampleClaim","SampleClaimValue")
            };

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting["key"]));
            var signingcredential = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            //var securitykey2 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1111111111111111"));
            //var enciptingcredential = new EncryptingCredentials(securitykey2, SecurityAlgorithms.Aes128KW,SecurityAlgorithms.Aes128CbcHmacSha256);
            var tokendescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingcredential,
                Issuer = jwtsetting["Issuer"],
                Audience = jwtsetting["Audience"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.Now.AddHours(7),
                NotBefore = DateTime.UtcNow,
                //EncryptingCredentials = enciptingcredential,
                CompressionAlgorithm = CompressionAlgorithms.Deflate
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var securitytoken = tokenhandler.CreateToken(tokendescription);
            return tokenhandler.WriteToken(securitytoken);

        }
    }
}
