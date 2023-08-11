using Blog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation
{
    public abstract class EfUseCase
    {

        protected BlogDbContext Context { get; }

        protected EfUseCase(BlogDbContext context)
        {
            Context = context;
        }
    }
}
