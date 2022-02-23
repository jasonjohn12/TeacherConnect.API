using Npgsql;
using TeacherConnect.API.Extensions;
using TeacherConnect.Data;
using TeacherConnect.Data.Interfaces;
using TeacherConnect.Helpers;
using TeacherConnect.Interface;
using TeacherConnect.Service;
using TeacherConnectAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors()
        .AddHealthChecks();
builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile("appsettings.Local.json", optional: true);
    if (context.HostingEnvironment.EnvironmentName != "local")
    {
        // get appsettings configurations from aws
        config.AddJsonSecretsManager($"teacherconnect-api/{context.HostingEnvironment.EnvironmentName}/appsettings.json");
    }
    config.AddEnvironmentVariables();
});
builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEntryRepository, EntryRepository>();
builder.Services.AddScoped<LogUserActivity>();
builder.Services.AddTransient(_ => new NpgsqlConnection(builder.Configuration.GetConnectionString("TeacherConnectDb")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
