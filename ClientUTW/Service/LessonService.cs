using BaseLibrary.Contracts;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using Blazored.LocalStorage;
using Radzen;

namespace ClientUTW.Service;

public class LessonService : ILessonRepository
{
    private const string BaseUrl = "api/Account";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly NotificationService _notificationService;

    public LessonService(HttpClient httpClient, ILocalStorageService localStorageService, NotificationService notificationService)
    {
        this._httpClient = httpClient;
        this._localStorageService = localStorageService;
        _notificationService = notificationService;
    }

    public async Task<List<Lesson>> GetAll()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("api/Lessons");


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<Lesson>(result)];
    }

    public Task<Lesson?> GetById(int lessonID)
    {
        throw new NotImplementedException();
    }

    public async Task<Lesson> Insert(Lesson lesson)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient
            .PostAsync($"api/Lessons",
                Generics.GenerateStringContent(
                    Generics.SerializeObj(lesson)));

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Lesson>(result);
    }

    public Task<Lesson> Update(int id, Lesson lesson)
    {
        throw new NotImplementedException();
    }

    public async Task<Lesson> Delete(int lessonID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"api/Lessons/{lessonID}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Lesson>(result);
    }
}