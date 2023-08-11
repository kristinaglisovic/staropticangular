using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases
{
    public interface ICommand<TRequest> :IUseCase
    {
        void Execute(TRequest request);
    }
}
