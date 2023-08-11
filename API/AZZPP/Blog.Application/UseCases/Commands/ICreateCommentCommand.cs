using Blog.Application.UseCases.DTO.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Commands
{
    public interface ICreateCommentCommand:ICommand<CreateCommentDTO>
    {
    }
}
