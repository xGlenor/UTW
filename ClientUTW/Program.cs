using System.Net.Http.Headers;
using System.Text;
using BaseLibrary.Contracts;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientUTW;
using ClientUTW.Auth;
using ClientUTW.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

HttpClient client = new HttpClient();

client.BaseAddress = new Uri("https://apiutw.gduraj.pl");
client.DefaultRequestHeaders
    .Accept
    .Add(new MediaTypeWithQualityHeaderValue("application/json")); //ACCEPT header


builder.Services.AddScoped(sp => client);
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IAccountRepository, AccountService>();
builder.Services.AddScoped<IStudentRepository, StudentService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentService>();
builder.Services.AddScoped<IFeeRepository, FeeService>();
builder.Services.AddScoped<ILessonRepository, LessonService>();
builder.Services.AddScoped<ISessionRepository, SessionService>();
builder.Services.AddScoped<IAccountCodeRepository, AccountCodeService>();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


await builder.Build().RunAsync();