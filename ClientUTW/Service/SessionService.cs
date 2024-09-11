using BaseLibrary.Contracts;
using BaseLibrary.enums;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using Blazored.LocalStorage;
using Radzen;

namespace ClientUTW.Service;

public class SessionService : ISessionRepository
{
    private const string BaseUrl = "api/Account";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly NotificationService _notificationService;

    public SessionService(HttpClient httpClient, ILocalStorageService localStorageService,
        NotificationService notificationService)
    {
        this._httpClient = httpClient;
        this._localStorageService = localStorageService;
        _notificationService = notificationService;
    }

    public async Task<List<Session>> GetAll()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("api/Sessions");


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<Session>(result)];
    }

    public Task<Session?> GetById(int sessionID)
    {
        throw new NotImplementedException();
    }

    public async Task<Session> Insert(Session session)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient
            .PostAsync($"api/Sessions",
                Generics.GenerateStringContent(
                    Generics.SerializeObj(session)));

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Session>(result);
    }

    public Task<Session> Update(int id, Session session)
    {
        throw new NotImplementedException();
    }

    public async Task<Session> Delete(int sessionID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"api/Sessions/{sessionID}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Session>(result);
    }

    public async Task<Session> GetCurrentSemester(DateTime currentDate)
    {
        int year = currentDate.Year;
        bool isWinter = currentDate.Month >= 10 || currentDate.Month <= 2;
        var sessions = await GetAll();

        return isWinter
            ? sessions.First(s => s.SessionType.Equals(SessionType.WINTER) && s.SessionYear.Equals(year))
            : sessions.First(s => s.SessionType.Equals(SessionType.SUMMER) && s.SessionYear.Equals(year));
    }
}