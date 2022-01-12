using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            var key = "{F2261C67-85F6-47C7-96B0-E66132147D11}";
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: "test",
                audience: "test",
                expires: DateTime.Now.AddMinutes(10),
                notBefore: DateTime.Now,
                signingCredentials: credentials

                );

            var x = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = x });
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? userName, string? password)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@$"http://185.142.157.187:8091/api/Authentication/Login?userName={userName}&password={password}");

            var result = await httpClient.PostAsync(httpClient.BaseAddress, null);
            var h = JsonSerializer.Deserialize<ResultDto<z>>(await result.Content.ReadAsStringAsync());



            Response.Cookies.Append("token", h.data.token, new CookieOptions { Expires = DateTime.Now.AddMinutes(5) });
            return Ok(h);
        }
    }


    public class z
    {
        public string token { get; set; }
        public string fullName { get; set; }
    }
}
