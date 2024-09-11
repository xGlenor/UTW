using AutoMapper;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerUTW.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository _repository)
    {
        this._repository = _repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
    {
        return Ok(await _repository.GetAllUsers());
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<ApplicationUser>> GetUser(string userId)
    {
        var user = await _repository.GetById(userId);

        if (user == null)
            return NotFound(new { message = userId });

        return Ok(user);
    }

    [HttpGet("TeacherFull/{userId}")]
    public async Task<ActionResult<ApplicationUser>> GetUserById(string userId)
    {
        var user = await _repository.GetUserById(userId);

        if (user == null)
            return NotFound(new { message = userId });

        return Ok(user);
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult<ApplicationUserDTO>> Delete(string userId)
    {
        var user = await _repository.Delete(userId);

        if (user != null)
            return Ok(user);

        return NotFound(user);
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<ApplicationUser>> Update(ApplicationUserDTO user)
    {
        var isUpdated = await _repository.Update(user);
        if (isUpdated)
            return Ok(new GeneralResponse(true, "User has been updated"));

        return NotFound(new GeneralResponse(false, "There was a problem, try again"));
    }
}