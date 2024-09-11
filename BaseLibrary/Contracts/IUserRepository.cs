using BaseLibrary.DTOs;
using BaseLibrary.Models;
using Microsoft.AspNetCore.Identity;

namespace BaseLibrary.Contracts;

public interface IUserRepository
{
    Task<List<ApplicationUserDTO>> GetAllUsers();
    Task<ApplicationUserDTO?> GetById(string userId);
    Task<ApplicationUser?> GetUserById(string userId);
    Task<bool> Update(ApplicationUserDTO user);
    Task<ApplicationUserDTO> Delete(string userId);
}