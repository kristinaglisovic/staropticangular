using Blog.Application.Exeptions;
using Blog.Application.UseCases.DTO.Roles;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetRoleQuery : EfUseCase, IGetRoleQuery
    {
        public EfGetRoleQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 14;

        public string Name => "Get one role";

        public string Description => "EF Get one role by id";

        public RoleDTO Execute(int id)
        {
            var role = Context.Roles.Include(x => x.Users).FirstOrDefault(x=>x.Id==id);

            if (role == null)
            {
                throw new EntityNotFoundException(nameof(Role), id);
            }


            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                UserRoleCount = role.Users.Count,
                CreatedAt = role.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                UpdatedAt = role.UpdatedAt.HasValue ? role.UpdatedAt.Value.ToString("yyyy-MM-dd hh:mm:ss") : "",
                IsActive = role.IsActive
            };
        }
    }
}
