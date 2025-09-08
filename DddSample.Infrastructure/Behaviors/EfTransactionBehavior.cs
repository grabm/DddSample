using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Behaviors
{
    internal sealed class EfTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        private readonly DbContext _dbContext;

        public EfTransactionBehavior(DbContext dbContext) => _dbContext = dbContext;


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //only for commands (create, update, delete)
            bool isCommand = typeof(TRequest).Name.EndsWith("Command", StringComparison.Ordinal);
            if (!isCommand)
            {
                return await next();
            }

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            var result = strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                var response = await next();

                await _dbContext.SaveChangesAsync(cancellationToken);
                await tx.CommitAsync(cancellationToken);

                return response;


            });

            return await result;
        }
    }
}
