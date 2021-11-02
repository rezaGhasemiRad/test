using System;
using System.Collections.Generic;

namespace Mail.Models.Entities
{
    public class UserGroup
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}