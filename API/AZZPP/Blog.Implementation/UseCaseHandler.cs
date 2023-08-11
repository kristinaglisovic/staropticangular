using Blog.Application.Exeptions;
using Blog.Application.Logging;
using Blog.Application.UseCases;
using Blog.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation
{
    public class UseCaseHandler
    {
        private IExceptionLogger _logger;
        private IApplicationUser _user;
        private IUseCaseLogger _useCaseLogger;

        public UseCaseHandler(IExceptionLogger logger, IApplicationUser user, IUseCaseLogger useCaseLogger)
        {
            _useCaseLogger = useCaseLogger; 
            _logger = logger;
            _user = user;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            try
            {
                HandleLoggingAndAuthorization(command, data);


                var stopwatch = new Stopwatch();
                stopwatch.Start();
                command.Execute(data);
                stopwatch.Stop();

                Console.WriteLine($"Name: {command.Name} - Duration: {stopwatch.ElapsedMilliseconds} ms.");
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                
                throw;
            }

        }
        public TResponse HandleQuery<TRequest,TResponse>(IQuery<TRequest,TResponse> query, TRequest data)
        {
            try
            {
                HandleLoggingAndAuthorization(query, data);
                var stopwatch = new Stopwatch();

                stopwatch.Start();

                var response = query.Execute(data);

                stopwatch.Stop();

                Console.WriteLine($"Name: {query.Name} - Duration: {stopwatch.ElapsedMilliseconds} ms.");

                return response;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        private void HandleLoggingAndAuthorization<TRequest>(IUseCase useCase, TRequest data)
        {
            var isAuthorized = _user.UseCaseIds.Contains(useCase.Id);
            var log = new UseCaseLog
            {
                User = _user.Identity,
                UserId = _user.Id,
                ExecutionDateTime = DateTime.UtcNow,
                UseCaseName = useCase.Name,
                Data = JsonConvert.SerializeObject(data),
                IsAuthorized = isAuthorized
            };

            _useCaseLogger.Log(log);

            if (!isAuthorized)
            {

                throw new ForbiddenUseCaseExecutionException(useCase.Name, _user.Identity);
            }
        }
    }
}
