﻿using Domain.common;
using Microsoft.AspNetCore.Identity;
namespace Domain.Identity;
public class User:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
}