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

            var z = JsonSerializer.Deserialize<ResultDto<x>>(x);

            return Ok(z);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string? name,string? mean)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@"http://api.eramlearn75.ir/api/Word/Create");


            var result = await httpClient.PostAsJsonAsync<wordItem>(@"http://api.eramlearn75.ir/api/Word/Create", new wordItem { name = name, mean = mean });

            return Ok(JsonSerializer.Deserialize<ResultDto>(await result.Content.ReadAsStringAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int wordId)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@$"http://api.eramlearn75.ir/api/Word/Delete?wordId={wordId}");

            var result=await httpClient.PostAsync(httpClient.BaseAddress,null);

            return Ok(JsonSerializer.Deserialize<ResultDto>(await result.Content.ReadAsStringAsync()));
        }

    }

    public class wordItem
    {
        public int wordId { get; set; }
        public string name { get; set; }
        public string mean { get; set; }
        public bool isActive { get; set; }
    }


    public class ResultDto
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

    public class x
    {
        public List<wordItem> items { get; set; }
    }
}
