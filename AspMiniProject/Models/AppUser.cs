﻿using Microsoft.AspNetCore.Identity;

namespace AspMiniProject.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
