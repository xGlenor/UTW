﻿using BaseLibrary.DTOs;
using BaseLibrary.Models;
using BaseLibrary.Responses;
namespace BaseLibrary.Contracts;

public interface IAccountRepository
{
    Task<GeneralResponse> CreateAccount(UserDTO? userDto);

    Task<LoginResponse> LoginAccount(LoginDTO? loginDto);
    
}