using Blog.Application.UseCases.DTO.Roles;
using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetRolesQuery : EfUseCase, IGetRolesQuery
    {
        public EfGetRolesQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id =>13;

        public string Name => "Get all roles";

        public string Description => "Ef Get all roles - active and inacive";

        public IEnumerable<RoleDTO> Execute(BaseSearch search)
        {
            var query = Context.Roles.Include(x => x.Users).AsQueryable();
            var kw = search.Keyword;


            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.Name.Contains(kw) || x.Users.Count.ToString().Contains(kw));
            }

            return query.Select(x => new RoleDTO
            {
                Id = x.Id,
                Name = x.Name,
                UserRoleCount = x.Users.Count,
                CreatedAt = x.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                UpdatedAt = x.UpdatedAt.HasValue ? x.UpdatedAt.Value.ToString("yyyy-MM-dd hh:mm:ss") : "",
                IsActive = x.IsActive
            }).ToList();
        }
    }
}
