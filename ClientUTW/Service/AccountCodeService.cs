using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using Blazored.LocalStorage;

namespace ClientUTW.Service;

public class AccountCodeService(HttpClient httpClient, ILocalStorageService localStorageService)
    : IAccountCodeRepository
{
    private const string BaseUrl = "api/AccountCode";
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService = localStorageService;

    public async Task<AccountCode>? CreateCode(AccountCode accountCode)
    {
        var code = GenerateCode();

        if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(accountCode.Email))
        {
            string? token = await _localStorageService.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            accountCode.Code = code;
            var response = await _httpClient
                .PostAsync($"{BaseUrl}/Create",
                    Generics.GenerateStringContent(
                        Generics.SerializeObj(accountCode)));

            var result = await response.Content.ReadAsStringAsync();
            return Generics.DeserializeJsonString<AccountCode>(result);
        }

        return null;
    }

    public async Task<AccountCode?> VerifyCode(VerifyCodeDTO verifyCodeDto)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PostAsync($"{BaseUrl}/VerifyCode",
            Generics.GenerateStringContent(
                Generics.SerializeObj(verifyCodeDto)));

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<AccountCode>(result);
    }

    public string GenerateCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();

        string code = new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return code;
    }
}