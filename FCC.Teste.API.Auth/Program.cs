using FCC.Teste.API.Auth.Repositories;
using FCC.Teste.API.Auth.Repository;
using FCC.Teste.Core.Repositories;
using FCC.Teste.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
// Add services to the container.

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints?.MapControllers();
});


app.Run();

