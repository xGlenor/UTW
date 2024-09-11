using BaseLibrary.Contracts;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Blazored.LocalStorage;
using Radzen;

namespace ClientUTW.Service;

public class FeeService : IFeeRepository
{
    private const string BaseUrl = "api/Fees";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly NotificationService _notificationService;

    public FeeService(HttpClient httpClient, ILocalStorageService localStorageService,
        NotificationService notificationService)
    {
        this._httpClient = httpClient;
        this._localStorageService = localStorageService;
        _notificationService = notificationService;
    }

    public async Task<List<Fee>> GetAll()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync(BaseUrl);


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<Fee>(result)];
    }

    public Task<Fee?> GetById(int feeID)
    {
        throw new NotImplementedException();
    }

    public async Task<Fee> Insert(Fee fee)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PostAsync(BaseUrl,
            Generics.GenerateStringContent(
                Generics.SerializeObj(fee)));

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Fee>(result);
    }

    public async Task<Fee> Update(int id, Fee fee)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync($"{BaseUrl}/{id}",
            Generics.GenerateStringContent(
                Generics.SerializeObj(fee)));

        var readAsString = await response.Content.ReadAsStringAsync();
        var result = Generics.DeserializeJsonString<EntityResponse>(readAsString);

        if (result.flag)
            return Generics.DeserializeJsonString<Fee>(result.objectJson);

        return null!;
    }

    public async Task<Fee> Delete(int feeID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{feeID}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Fee>(result);
    }
}