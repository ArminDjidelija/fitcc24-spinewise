using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Email_sender;
using SpineWise.Web.Helpers.Loggers;
using SpineWise.Web.Services;
using SpineWise.Web.Services.SignalR;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db")));


//za angular 1. dio
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins(new string[] { "https://spinewise.p2361.app.fit.ba", "https://backend.spinewise.p2361.app.fit.ba", "http://localhost:4200" })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.OperationFilter<AuthorizationSwaggerHeader>());
builder.Services.AddTransient<MyAuthService>();
builder.Services.AddTransient<MyActionLogService>();
builder.Services.AddTransient<EmailSenderService>();
//builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISignInLogger, SignInLogger>();
builder.Services.AddScoped<ISignOutLogger, SignOutLogger>();
//builder.Services.AddHostedService<KeepAliveService>();
//builder.Services.AddSwaggerGen(x=>x.ope)

builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors();

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<SignalrHub>("/signalr");

app.Run();
