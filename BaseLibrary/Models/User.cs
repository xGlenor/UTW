﻿using BaseLibrary.enums;
using Microsoft.AspNetCore.Identity;

namespace BaseLibrary.Models;

public class User : IdentityUser
{
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Address { get; set; }
    
    public DateOnly Birthdate { get; set; }
    
    public string? Skills { get; set; }
    
    public UserRole UserRole { get; set; }
}