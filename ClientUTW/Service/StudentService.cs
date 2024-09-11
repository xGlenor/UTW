using System.Diagnostics;
using AutoMapper;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace ClientUTW.Service;

public class StudentService : IStudentRepository
{
    private const string BaseUrl = "api/Students";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly NotificationService _notificationService;
    private readonly IMapper _mapper;

    public StudentService(HttpClient httpClient, ILocalStorageService localStorageService, IMapper _mapper)
    {
        this._httpClient = httpClient;
        this._localStorageService = localStorageService;
        this._mapper = _mapper;
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

    public async Task<List<Student>> GetStudents()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/Enrolled");


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<Student>(result)];
    }

    public async Task<List<Student>> GetCandidates()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/Candidates");


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
        var studentDto = _mapper.Map<StudentDTO>(student);
        Debug.WriteLine(Generics.SerializeObj(studentDto));
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient
            .PostAsync($"{BaseUrl}",
                Generics.GenerateStringContent(
                    Generics.SerializeObj(studentDto)));

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Student>(result);
    }

    public async Task<Student> Update(int studentID, Student student)
    {
        var studentDto = _mapper.Map<StudentDTO>(student);
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync($"{BaseUrl}/{studentID}",
            Generics.GenerateStringContent(
                Generics.SerializeObj(studentDto)));

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