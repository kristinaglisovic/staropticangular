using Blog.Application.UseCases.DTO.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public interface IGetRoleQuery:IQuery<int,RoleDTO>
    {
    }
}
