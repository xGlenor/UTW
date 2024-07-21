using BaseLibrary.Contracts;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Blazored.LocalStorage;
using Radzen;

namespace ClientUTW.Service;

public class StudentService : IStudentRepository
{
    private const string BaseUrl = "api/Students";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly NotificationService _notificationService;
    
    public StudentService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        this._httpClient = httpClient;
        this._localStorageService = localStorageService;
    }
    
    public async Task<List<Student>> GetAll()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}");

        
        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<Student>(result)];
    }

    public async Task<Student?> GetById(int studentID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/{studentID}");
        
        if (!response.IsSuccessStatusCode)
            return null!;
        
        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Student>(result);
    }

    public async Task<Student> Insert(Student student)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient
            .PostAsync($"{BaseUrl}",
                Generics.GenerateStringContent(
                    Generics.SerializeObj(student)));

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Student>(result);
    }

    public async Task<Student> Update(int studentID, Student student)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync($"{BaseUrl}/{studentID}",
            Generics.GenerateStringContent(
                Generics.SerializeObj(student)));

        var readAsString = await response.Content.ReadAsStringAsync();
        var result = Generics.DeserializeJsonString<EntityResponse>(readAsString);

        if (result.flag)
            return Generics.DeserializeJsonString<Student>(result.objectJson);
        
        return null!;
    }

    public async Task<Student> Delete(int studentID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{studentID}");
        
        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Student>(result);
    }
}