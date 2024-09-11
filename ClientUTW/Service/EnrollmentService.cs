using AutoMapper;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Blazored.LocalStorage;
using Radzen;

namespace ClientUTW.Service;

public class EnrollmentService : IEnrollmentRepository
{
    private const string BaseUrl = "api/Enrollment";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly IMapper _mapper;

    public EnrollmentService(HttpClient httpClient, ILocalStorageService localStorageService, IMapper mapper)
    {
        this._httpClient = httpClient;
        this._localStorageService = localStorageService;
        _mapper = mapper;
    }

    public async Task<List<Enrolllment>> GetAll()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}");


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<Enrolllment>(result)];
    }

    public async Task<Enrolllment?> GetById(int enrollmentID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/{enrollmentID}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Enrolllment>(result);
    }

    public async Task<Enrolllment> Insert(Enrolllment enrollment)
    {
        var enrollmentDTO = _mapper.Map<EnrollmentDTO>(enrollment);
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient
            .PostAsync($"{BaseUrl}",
                Generics.GenerateStringContent(
                    Generics.SerializeObj(enrollmentDTO)));

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Enrolllment>(result);
    }

    public async Task<Enrolllment> Update(int id, Enrolllment enrollment)
    {
        var enrollmentDto = _mapper.Map<EnrollmentDTO>(enrollment);
        Console.WriteLine(Generics.SerializeObj(enrollmentDto));

        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync($"{BaseUrl}/{id}",
            Generics.GenerateStringContent(
                Generics.SerializeObj(enrollmentDto)));

        var readAsString = await response.Content.ReadAsStringAsync();
        var result = Generics.DeserializeJsonString<EntityResponse>(readAsString);

        if (result.flag)
            return Generics.DeserializeJsonString<Enrolllment>(result.objectJson);

        return null!;
    }

    public async Task<Enrolllment> Delete(int enrollmentID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{enrollmentID}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<Enrolllment>(result);
    }

    public async Task<List<TeacherEnrollment>> GetAllTeachers()
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/Teachers");


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<TeacherEnrollment>(result)];
    }

    public async Task<TeacherEnrollment?> GetTeacherById(int enrollmentID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/Teachers/{enrollmentID}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<TeacherEnrollment>(result);
    }

    public async Task<TeacherEnrollment> InsertTeacher(TeacherEnrollment enrollment)
    {
        var enrollmentDTO = _mapper.Map<TeacherEnrollmentDto>(enrollment);
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient
            .PostAsync($"{BaseUrl}/Teachers",
                Generics.GenerateStringContent(
                    Generics.SerializeObj(enrollmentDTO)));

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<TeacherEnrollment>(result);
    }

    public async Task<TeacherEnrollment> UpdateTeacher(int id, TeacherEnrollment enrollment)
    {
        var enrollmentDto = _mapper.Map<TeacherEnrollmentDto>(enrollment);
        Console.WriteLine(Generics.SerializeObj(enrollmentDto));

        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync($"{BaseUrl}/Teachers/{id}",
            Generics.GenerateStringContent(
                Generics.SerializeObj(enrollmentDto)));

        var readAsString = await response.Content.ReadAsStringAsync();
        var result = Generics.DeserializeJsonString<EntityResponse>(readAsString);

        if (result.flag)
            return Generics.DeserializeJsonString<TeacherEnrollment>(result.objectJson);

        return null!;
    }

    public async Task<TeacherEnrollment> DeleteTeacher(int enrollmentID)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/Teachers/{enrollmentID}");

        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return Generics.DeserializeJsonString<TeacherEnrollment>(result);
    }

    public async Task<List<TeacherEnrollment>> GetTeacherEnrollmentsById(string userId)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/TeacherEnrollemntsbyId/{userId}");


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<TeacherEnrollment>(result)];
    }

    public async Task<List<Enrolllment>> GetStudentbyLessonId(int LessonId)
    {
        string? token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BaseUrl}/ByLesson/{LessonId}");


        if (!response.IsSuccessStatusCode)
            return null!;

        var result = await response.Content.ReadAsStringAsync();
        return [.. Generics.DeserializeJsonStringList<Enrolllment>(result)];
    }
}