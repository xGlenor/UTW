using BaseLibrary.DTOs;

using static BaseLibrary.DTOs.ServiceResponses;
namespace BaseLibrary.Contracts;

public interface IAccountRepository
{
    Task<GeneralResponse> CreateAccount(UserDTO? userDto);

    Task<LoginResponse> LoginAccount(LoginDTO? loginDto);

}