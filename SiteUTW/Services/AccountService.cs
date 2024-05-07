using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using Blazored.LocalStorage;

namespace SiteUTW.Services;

public class AccountService : IAccountRepository, IStudentRepository
{
    public AccountService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        this.httpClient = httpClient;
        this.localStorageService = localStorageService;
    }

    private const string BaseUrl = "api/Account";
    private readonly HttpClient httpClient;
    private readonly ILocalStorageService localStorageService;

    public async Task<ServiceResponses.GeneralResponse> CreateAccount(UserDTO userDTO)
    {
        var response = await httpClient
            .PostAsync($"{BaseUrl}/registerUser",
                Generics.GenerateStringContent(
                    Generics.SerializeObj(userDTO)));
        
        if (!response.IsSuccessStatusCode)
            return new ServiceResponses.GeneralResponse(false, "Error occured. Try again later...");

        var apiResponse = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<ServiceResponses.GeneralResponse>(apiResponse);
    }
    
    public async Task<ServiceResponses.LoginResponse> LoginAccount(LoginDTO loginDTO)
    {
        var response = await httpClient
            .PostAsync($"{BaseUrl}/loginUser",
                Generics.GenerateStringContent(
                    Generics.SerializeObj(loginDTO)));
        
        if (!response.IsSuccessStatusCode)
            return new ServiceResponses.LoginResponse(false, null!, "Error occured. Try again later...");

        var apiResponse = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<ServiceResponses.LoginResponse>(apiResponse);

    }
    

    public async Task<Student[]> GetStudents()
    {
        string token = await localStorageService.GetItemAsStringAsync("token");
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.GetAsync("api/Student/teacher");

        
        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<Student>(result)];
    }
}