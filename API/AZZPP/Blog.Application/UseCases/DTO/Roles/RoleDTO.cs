using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.Roles
{
    public class RoleDTO : BaseDTO
    {
        public string Name { get; set; }

        public int UserRoleCount { get; set; }
        public bool IsActive { get; set; }

        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
