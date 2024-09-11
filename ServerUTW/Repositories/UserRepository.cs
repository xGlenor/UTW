using AutoMapper;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerUTW.Data;

namespace ServerUTW.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public UserRepository(UserManager<ApplicationUser> _userManager, AppDbContext _context, IMapper mapper)
    {
        this._userManager = _userManager;
        this._context = _context;
        _mapper = mapper;
    }

    public async Task<List<ApplicationUserDTO>> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        var mappedUsers = _mapper.Map<List<ApplicationUserDTO>>(await _userManager.Users.ToListAsync());
        return mappedUsers;
    }

    public async Task<ApplicationUserDTO?> GetById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var mappedUser = _mapper.Map<ApplicationUserDTO>(user);
        return mappedUser;
    }

    public async Task<ApplicationUser?> GetUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user;
    }

    public async Task<bool> Update(ApplicationUserDTO userDto)
    {
        var user = await GetUserById(userDto.Id);

        if (user == null)
            return false;


        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Address = userDto.Address;
        user.Email = userDto.Email;
        user.Birthdate = userDto.Birthdate;
        user.Skills = userDto.Skills;
        user.Code = userDto.Code;

        var appuser = await _userManager.UpdateAsync(user);
        return appuser.Succeeded;
    }

    public async Task<ApplicationUserDTO> Delete(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var logins = await _userManager.GetLoginsAsync(user);
        var rolesForUser = await _userManager.GetRolesAsync(user);

        using (var transaction = _context.Database.BeginTransaction())
        {
            foreach (var login in logins.ToList())
            {
                await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
            }

            if (rolesForUser.Count() > 0)
            {
                foreach (var role in rolesForUser.ToList())
                {
                    // item should be the name of the role
                    var result = await _userManager.RemoveFromRoleAsync(user, role);
                }
            }

            await _userManager.DeleteAsync(user);
            transaction.Commit();
        }

        var mappedUser = _mapper.Map<ApplicationUserDTO>(user);

        return mappedUser;
    }
}