using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApplication6.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WordController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(@"http://api.eramlearn75.ir/api/Word/Index");
            var x = await client.GetStringAsync(client.BaseAddress);

            var z = JsonSerializer.Deserialize<ResultDto<items>>(x);

            return Ok();
        }



    }

    public class items
    {
        public int wordId { get; set; }
        public string name { get; set; }
        public string mean { get; set; }
        public bool isActive { get; set; }
    }


    public class Result
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        //public List<item> data { get; set;}
    }

    public class ResultDto<T>
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
