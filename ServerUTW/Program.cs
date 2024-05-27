using System.Text;
using BaseLibrary.Contracts;
using BaseLibrary.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using ServerUTW.Data;
using ServerUTW.Repositories;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);

/*
|--------------------------------------------------------------------------
| General Settings
|--------------------------------------------------------------------------
*/

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();

/*
|--------------------------------------------------------------------------
| Database Settings
|--------------------------------------------------------------------------
*/
var connectionString = builder.Configuration.GetConnectionString("DBString");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=utw.db");
});
/*
|--------------------------------------------------------------------------
| Identity Settings & JWT Settings
|--------------------------------------------------------------------------
*/
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddRoles<IdentityRole>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});
/*
|--------------------------------------------------------------------------
| Swagger Settings
|--------------------------------------------------------------------------
*/
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
/*
|--------------------------------------------------------------------------
| Swagger Settings
|--------------------------------------------------------------------------
*/

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

/*
|--------------------------------------------------------------------------
| Build App & Manage App
|--------------------------------------------------------------------------
*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy =>
    {
        policy.WithOrigins("http://localhost:7132", "https://localhost:7132")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithHeaders(HeaderNames.ContentType);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();