using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.DataAccess.Extensions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfSoftDeleteRoleCommand : EfUseCase, ISoftDeleteRoleUsingIntCommand
    {
        public EfSoftDeleteRoleCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => "Soft Delete Role";

        public string Description => "Ef Soft Delete Role using deactivation";

        public void Execute(int request)
        {
            var r = Context.Roles.Where(x => x.IsActive)
                                      .Include(x => x.Users).FirstOrDefault(x => x.Id == request);

            if (r == null)
            {
                throw new EntityNotFoundException(nameof(Role), request);
            }

            if (r.Users.Any())
            {
                throw new UseCaseConflictException("Can't deactivate role because of it's link to users: "
                                                  + string.Join(", ", r.Users.Select(x => x.Username)));
            }

            Context.Deactivate<Role>(r.Id);
            Context.SaveChanges();
        }
    }
}
