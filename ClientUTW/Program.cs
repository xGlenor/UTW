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

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7217/") });
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
    

await builder.Build().RunAsync();