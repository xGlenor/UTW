using BaseLibrary.DTOs;
using BaseLibrary.Models;

namespace BaseLibrary.Contracts;

public interface IAccountCodeRepository
{
    Task<AccountCode>? CreateCode(AccountCode accountCode);

    Task<AccountCode?> VerifyCode(VerifyCodeDTO verifyCodeDto);
}