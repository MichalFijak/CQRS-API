using Api.Config;
using Api.Providers;
using Api.State;
using Application.Commands.User;
using Application.Queries.Employee;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Interceptor;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7092");
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetEmployeeQuerry>();
    cfg.RegisterServicesFromAssemblyContaining<RegisterUserCommand>();
});

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    var cfg = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer = cfg["Issuer"],
        ValidAudience= cfg["Audience"],
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Key"]!))
    };
});



builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeInfoRepository, EmployeeInfoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<ITokenService,TokenService>();
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthStateProvider>();
builder.Services.AddScoped<IAuthStateService, AuthStateService>();
builder.Services.AddTransient<AuthTokenHandler>();
builder.Services.AddSingleton<SoftDeleteInterceptor>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7092");
})
.AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddDbContext<AppDbContext>((sp,options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>());
});

builder.Services.AddRateLimiter(options =>
{
    options.AddEmployeeRateLimiting();
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.MapScalarApiReference();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

Api.Endpoints.Modules.EmployeesModule.Map(app);
Api.Endpoints.Auth.Register.Map(app);
Api.Endpoints.Auth.Login.Map(app);
app.Run();
