using AutoMapper;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Identity;

namespace ClientUTW.Service;

public class UserService : IUserRepository
{
    private const string BaseUrl = "api/User";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly IMapper _mapper;

    public UserService(HttpClient httpClient, ILocalStorageService localStorageService, IMapper mapper)
    {
        this._httpClient = httpClient;
        this._localStorageService = localStorageService;
        _mapper = mapper;
    }

    public async Task<List<ApplicationUserDTO>> GetAllUsers()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}");


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<ApplicationUserDTO>(result)];
    }

    public async Task<ApplicationUserDTO?> GetById(string userId)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/{userId}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<ApplicationUserDTO>(result);
    }

    public async Task<ApplicationUser?> GetUserById(string userId)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/TeacherFull/{userId}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<ApplicationUser>(result);
    }

    public async Task<bool> Update(ApplicationUserDTO userDto)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync($"{BaseUrl}/{userDto.Id}",
            Generics.GenerateStringContent(
                Generics.SerializeObj(userDto)));

        return response.IsSuccessStatusCode;
    }


    public async Task<ApplicationUserDTO> Delete(string userId)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{userId}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<ApplicationUserDTO>(result);
    }
}