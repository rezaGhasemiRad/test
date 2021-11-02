using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Mail.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<UserGroup> UserGroups { get; set; }
    }
}