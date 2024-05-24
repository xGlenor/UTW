using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
namespace ServerUTW.Repositories;

public class AccountRepository(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration config)
    : IAccountRepository
{
    public async Task<GeneralResponse> CreateAccount(UserDTO? userDto)
    {
        if (userDto == null)
            return new GeneralResponse(false, "Dane są puste");

        var newUser = new ApplicationUser()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            PasswordHash = userDto.Password,
            Email = userDto.Email,
            UserName = userDto.Email
        };
        
        var user = await userManager.FindByEmailAsync(newUser.Email);

        if (user != null)
            return new GeneralResponse(false, "Użytkownik jest już zarejestrowany");

        var createUser = await userManager.CreateAsync(newUser!, userDto.Password);
        if (!createUser.Succeeded)
            return new GeneralResponse(false, "Wystąpił problem, spróbuj ponownie później");

        var checkAdmin = await roleManager.FindByNameAsync("Admin");
        if (checkAdmin == null)
        {
            await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await userManager.AddToRoleAsync(newUser, "Admin");
            return new GeneralResponse(true, "Konto utworzone");
        }

        var checkTeacher = await roleManager.FindByNameAsync("Teacher");
        if (checkTeacher == null)
            await roleManager.CreateAsync(new IdentityRole() { Name = "Teacher" });

        await userManager.AddToRoleAsync(newUser, "Teacher");
        return new GeneralResponse(true, "Konto utworzone");

    }

    public async Task<LoginResponse> LoginAccount(LoginDTO? loginDto)
    {
        if (loginDto == null)
            return new LoginResponse(false, null!, "Dane logowania są puste");

        var getUser = await userManager.FindByEmailAsync(loginDto.Email);
        if (getUser == null)
            return new LoginResponse(false, null!, "Użytkownik nie istnieje");

        bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDto.Password);
        if (!checkUserPasswords)
            return new LoginResponse(false, null!, "Niepoprawny login lub hasło");

        var getUserRole = await userManager.GetRolesAsync(getUser);
        var userSession = new UserSession(getUser.Id, getUser.UserName, getUser.Email, getUserRole.First());
        string token = GenerateToken(userSession);
        return new LoginResponse(true, token!, "Logowanie pomyślne");
    }

    private string GenerateToken(UserSession? user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}