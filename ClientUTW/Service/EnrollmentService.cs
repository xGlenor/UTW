using BaseLibrary.Contracts;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using Blazored.LocalStorage;
using Radzen;

namespace ClientUTW.Service;

public class EnrollmentService : IEnrollmentRepository
{
    private const string BaseUrl = "api/Account";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly NotificationService _notificationService;

    public EnrollmentService(HttpClient httpClient, ILocalStorageService localStorageService, NotificationService notificationService)
    {
        this._httpClient = httpClient;
        this._localStorageService = localStorageService;
        _notificationService = notificationService;
    }

    public async Task<List<Enrolllment>> GetAll()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("api/Enrollment");


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<Student>(result)];
    }

    public Task<Student?> GetById(int studentID)
    {
        throw new NotImplementedException();
    }

    public async Task<Student> Insert(Student student)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient
            .PostAsync($"api/Students",
                Generics.GenerateStringContent(
                    Generics.SerializeObj(student)));

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Student>(result);
    }

    public Task<Student> Update(int id, Student student)
    {
        throw new NotImplementedException();
    }

    public async Task<Student> Delete(int studentID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"api/Students/{studentID}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Student>(result);
    }
}