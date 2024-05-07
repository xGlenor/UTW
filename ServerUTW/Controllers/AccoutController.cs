using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ServerUTW.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccoutController(IAccountRepository accountRepository) : ControllerBase
{
    [HttpPost("registerUser")]
    public async Task<IActionResult> Register(UserDTO userDto)
    {
        var res = await accountRepository.CreateAccount(userDto);

        return Ok(res);
    }

    [HttpPost("loginUser")]
    public async Task<IActionResult> Login(LoginDTO loginDto)
    {
        var res = await accountRepository.LoginAccount(loginDto);

        return Ok(res);
    }
}