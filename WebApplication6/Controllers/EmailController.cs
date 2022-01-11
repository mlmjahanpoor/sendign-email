using MailKit.Net.Pop3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenPop.Mime;
using System.Text;
using WebApplication6.Model;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailService mailService;
        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            string email = "profastammar@gmail.com";
            string pass = "Q_1234567";

            var pop3 = new Pop3Client();
            pop3.Connect("pop.gmail.com", 995, true);
            pop3.Authenticate(email, pass);

            //MessagePart messagePart = message.MessagePart.MessageParts[0];

            pop3.DeleteMessage(1);

            var messages = new List<getMessageDto>();

            for (int i = pop3.GetMessageCount() - 1; i > 0; i--)
            {
                var x = pop3.GetMessage(i);

                messages.Add(new getMessageDto
                {
                    subject = x.Subject,
                    textBody = x.TextBody,
                });

            }



            return Ok(messages);

        }
    }

    public class getMessageDto
    {
        public string subject { get; set; }
        public string textBody { get; set; }
    }
}
