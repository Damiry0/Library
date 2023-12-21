using API.Context;
using API.Models;
using BooksAPI.Exceptions;
using EntityFrameworkCore.Triggered;

namespace BooksAPI.Triggers
{
    public class RestrictUserTotalBorrowingCountTrigger : IBeforeSaveTrigger<Borrowing>
    {
        private readonly LibraryMsSQLDbContext _applicationContext;

        public RestrictUserTotalBorrowingCountTrigger(LibraryMsSQLDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Task BeforeSave(ITriggerContext<Borrowing> context, CancellationToken cancellationToken)
        {
            var userBorrowedCount = _applicationContext.Borrowings.Count(r =>
                r.User.Id == context.Entity.User.Id && r.IsReturned == false);

            if (context.ChangeType == ChangeType.Added)
            {
                if (userBorrowedCount >= 5)
                {
                    throw new TriggerException("Cannot have more than 5 borrows per user");
                }
            }

            return Task.CompletedTask;
        }
    }
}