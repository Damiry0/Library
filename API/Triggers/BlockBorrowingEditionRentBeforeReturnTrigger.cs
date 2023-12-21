using API.Models;
using BooksAPI.Exceptions;
using EntityFrameworkCore.Triggered;

namespace BooksAPI.Triggers
{
    public class BlockBorrowingEditionRentBeforeReturnTrigger : IBeforeSaveTrigger<Borrowing>
    {
        public Task BeforeSave(ITriggerContext<Borrowing> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                if (context.Entity.Edition.Status == Status.Borrowed)
                {
                    throw new TriggerException("Cannot borrow borrowed edition");
                }
            }

            return Task.CompletedTask;
        }
    }
}