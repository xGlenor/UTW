using System.Text;
using System.Text.Json.Serialization;
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

builder.Services.AddControllers(
        opt => opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/*
|--------------------------------------------------------------------------
| Database Settings
|--------------------------------------------------------------------------
*/

builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlite("Data Source=utw.db"); });

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
builder.Services.AddScoped<IFeeRepository, FeeRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IAccountCodeRepository, AccountCodeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

/*
|--------------------------------------------------------------------------
| Build App & Manage App
|--------------------------------------------------------------------------
*/


var app = builder.Build();

using (var scopeDB = app.Services.CreateScope())
{
    var dbContext = scopeDB.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

/*app.UseHttpsRedirection();*/

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();