using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;
using WebApplication6.Jobs;
using WebApplication6.Services;
using WebApplication6.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(ops =>
{
    ops.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = "test",
        ValidAudience = "test",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("{F2261C67-85F6-47C7-96B0-E66132147D11}")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
    };

    ops.SaveToken = true;



});

#region Quartz

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    JobKey jobKey = new JobKey("sendemailJob");
    q.AddJob<SendEmailJob>(opt => opt.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey) // link to the HelloWorldJob
        .WithIdentity("HelloWorldJob-trigger")
        .WithSimpleSchedule(x =>
        {
            x.WithIntervalInHours(12);
            x.RepeatForever();
            x.WithRepeatCount(1);

        }));


    // give the trigger a unique name
    /*.WithCronSchedule("* * * * * ?"));*/ // run every 5 seconds

});


builder.Services.AddQuartzServer(options =>
{
    options.WaitForJobsToComplete = true;
});


//services.AddSingleton<IJobFactory, SingletonJobFactory>();
//services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

//services.AddSingleton<LogoutJob>();
//services.AddSingleton(new JobSchedule(jobType: typeof(LogoutJob), cronExpression: "0/59 59 23 * * ?"));

//services.AddHostedService<QuartzHostedService>();

#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseDeveloperExceptionPage();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
