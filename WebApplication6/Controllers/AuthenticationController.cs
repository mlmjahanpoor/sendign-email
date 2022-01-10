using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Login(string? userName,string? password)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@$"http://185.142.157.187:8091/api/Authentication/Login?userName={userName}&password={password}");

            var result = await httpClient.PostAsync(httpClient.BaseAddress, null);
            var h = JsonSerializer.Deserialize<ResultDto<z>>(await result.Content.ReadAsStringAsync());



            Response.Cookies.Append("token", h.data.token,new CookieOptions { Expires=DateTime.Now.AddMinutes(5)});
            return Ok(h);
        }
    }


    public class z
    {
        public string token { get; set; }
        public string fullName { get; set; }
    }
}
