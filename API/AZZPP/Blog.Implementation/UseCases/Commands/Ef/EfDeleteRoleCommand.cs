using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfDeleteRoleCommand : EfUseCase, IDeleteRoleUsingIntCommand
    {
        public EfDeleteRoleCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 17;

        public string Name => "Delete role";

        public string Description => "EF delete role by ID";

        public void Execute(int request)
        {
            var r = Context.Roles.Include(x => x.Users).FirstOrDefault(x => x.Id == request);

            if (r == null)
            {
                throw new EntityNotFoundException(nameof(Role), request);
            }

            if (r.Users.Any())
            {
                throw new UseCaseConflictException("Can't delete role because of it's link to users: "
                                                  + string.Join(", ", r.Users.Select(x => x.Username)));
            }

            Context.Roles.Remove(r);
            Context.SaveChanges();
        }
    }
}
