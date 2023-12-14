using API.Models;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Triggers
{
    public class RestrictUserTotalBorrowingCountTrigger : IBeforeSaveTrigger<Borrowing>
    {
        private readonly DbContext _applicationContext;

        public RestrictUserTotalBorrowingCountTrigger(DbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public Task BeforeSave(ITriggerContext<Borrowing> context, CancellationToken cancellationToken)
        {
            switch (context.ChangeType)
            {
                case ChangeType.Added:
                {
                    if (context.Entity.User.Borrowings.Count() > 5)
                    {
                        throw new Exception("Cannot have more than 5 borrows per user");
                    }
                    break;
                }
                case ChangeType.Modified:
                {
                    if (context.Entity.IsReturned)
                    {
                        context.Entity.User.Borrowings.Remove(context.Entity);
                    }
                    break;
                }
            }
            return Task.CompletedTask;
        }
    }
}