using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases
{
    public interface IUseCaseLogger
    {
        void Log(UseCaseLog log);
        IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch search);
    }

    

    public class UseCaseLogSearch
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string UseCaseName { get; set; }
        public string Username { get; set; }
        public int? UserId { get; set; }
    }

    public class UseCaseLog
    {
        public string UseCaseName { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public DateTime ExecutionDateTime { get; set; }
        public string Data { get; set; }
        public bool IsAuthorized { get; set; }
    }


}
