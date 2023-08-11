using Blog.Application.UseCases;
using Blog.Application.UseCases.Queries;
using Blog.Implementation.Validators;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Implementation.UseCases.Queries.SP
{
    public class GetUseCaseLogsQuery : IGetUseCaseLogsQuery
    {

        private IUseCaseLogger _logger;
        private SearchUseCaseLogsValidator _validator;

        public GetUseCaseLogsQuery(IUseCaseLogger logger, SearchUseCaseLogsValidator validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Search use case logs";

        public string Description => "SP";

 

        public IEnumerable<UseCaseLog> Execute(UseCaseLogSearch search)
        {
            _validator.ValidateAndThrow(search);
            return _logger.GetLogs(search).OrderByDescending(x=>x.ExecutionDateTime);
        }
    }
}
