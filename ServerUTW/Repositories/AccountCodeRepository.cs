using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.Models;
using Microsoft.EntityFrameworkCore;
using ServerUTW.Data;

namespace ServerUTW.Repositories;

public class AccountCodeRepository: IAccountCodeRepository
{
    private readonly AppDbContext _dbContext;

    public AccountCodeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<AccountCode>? CreateCode(AccountCode accountCode)
    {
        var entity = _dbContext.AccountCodes.Add(accountCode);
        await _dbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public Task<AccountCode?> VerifyCode(VerifyCodeDTO verifyCodeDto)
    {
       
        return _dbContext
            .AccountCodes
            .FirstOrDefaultAsync(ac => ac.Email.Equals(verifyCodeDto.Email) && ac.Code.Equals(verifyCodeDto.Code));;
    }
}