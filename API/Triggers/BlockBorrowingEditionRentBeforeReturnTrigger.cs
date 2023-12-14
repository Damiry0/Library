using API.Models;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Triggers
{
    public class BlockBorrowingEditionRentBeforeReturnTrigger : IBeforeSaveTrigger<Borrowing>
    {
        private readonly DbContext _applicationContext;

        public BlockBorrowingEditionRentBeforeReturnTrigger(DbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public Task BeforeSave(ITriggerContext<Borrowing> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                if (context.Entity.Edition.Status == Status.Borrowed)
                {
                    throw new Exception("Cannot borrow borrowed edition");
                }
                
            }
            return Task.CompletedTask;
        }
    }
}