﻿using Microsoft.AspNetCore.Identity;   



namespace ConfInfrastructure.Models
{
    public class User : IdentityUser
    {

        public int Year { get; set; }
    }
}
