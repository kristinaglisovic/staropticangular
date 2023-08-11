using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.User
{
    public class UpdateUserUseCasesDTO
    {
        public int UserId { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; }
    }
}
