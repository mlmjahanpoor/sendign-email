using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            string token = string.Empty;

            token = Request.Cookies["token"];

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@$"http://185.142.157.187:8091/api/Bank/Index");

            HttpContext.Request.Headers.Add("Authorization", @"bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjkiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoi2K3Ys9uM2YYg2YHYp9i32YXbjCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJVc2VyIiwiQWRtaW4iXSwibmJmIjoxNjQxODc4ODgzLCJleHAiOjE2NDQ0NzA4ODMsImlzcyI6InRlc3QuY29tIiwiYXVkIjoidGVzdC5jb20ifQ.hqjCtF-xURB_uYpFc9zHTkJ-1uK-lUJq64oDSeE4lEs");

            httpClient.DefaultRequestHeaders.Authorization
               = new AuthenticationHeaderValue("Bearer", token);


            var result = await httpClient.GetStringAsync(httpClient.BaseAddress);

            var x = JsonSerializer.Deserialize<ResultDto<y>>(result);

            return Ok(x);
        }
    }

    public class item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cardNumber { get; set; }
    }

    public class y
    {
        public List<item> items { get; set; }
    }
}
