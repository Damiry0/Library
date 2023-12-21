using API.Models;
using BooksAPI.Exceptions;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Triggers
{
    public class BlockMultipleChangeStatusTrigger : IBeforeSaveTrigger<Borrowing>
    {
        private readonly DbContext _applicationContext;

        public BlockMultipleChangeStatusTrigger(DbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Task BeforeSave(ITriggerContext<Borrowing> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Modified)
            {
                if (_applicationContext.ChangeTracker.Entries<Borrowing>().Count(e => e.State == EntityState.Modified) >
                    1)
                {
                    throw new TriggerException("Cannot modify more than one record");
                }
            }

            return Task.CompletedTask;
        }
    }
}