
using Quartz;
using WebApplication6.Model;
using WebApplication6.Services;
using static Quartz.Logging.OperationName;

namespace WebApplication6.Jobs
{
    [DisallowConcurrentExecution]
    public class SendEmailJob : IJob
    {
        private readonly IMailService _mailService;
        public SendEmailJob(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {



            try
            {
                 //await _mailService.SendEmailAsync(new MailRequest 
                 //{ 
                 //    Subject=$"333333332new HI {Guid.NewGuid()}",
                 //    Body=$"{Guid.NewGuid()}",
                 //    ToEmail="mlmjahanpoor1996@gmail.com"
                 //});
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
