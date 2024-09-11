using System.Diagnostics;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ServerUTW.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountCodeController(IAccountCodeRepository accountCodeRepository) : ControllerBase
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(AccountCode accountCode)
    {
        var res = await accountCodeRepository.CreateCode(accountCode)!;

        return Ok(res);
    }

    [HttpPost("VerifyCode")]
    public async Task<IActionResult> VerifyCode(VerifyCodeDTO verifyCodeDto)
    {
        var entity = await accountCodeRepository.VerifyCode(verifyCodeDto);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }
}