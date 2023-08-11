using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.Tag
{
    public class UpdateTagDTO
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public string NameUs { get; set; }
        
    }
}
