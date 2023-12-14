using API.Context;
using API.Models;
using EntityFrameworkCore.Triggered;

namespace BooksAPI.Triggers
{
    public class RestrictUserDailyBorrowingCountTrigger : IBeforeSaveTrigger<Borrowing>
    {
        private readonly LibraryMySQLDbContext _applicationContext;

        public RestrictUserDailyBorrowingCountTrigger(LibraryMySQLDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public Task BeforeSave(ITriggerContext<Borrowing> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);

                var dailyBorrowedCount = _applicationContext.Borrowings.Count(r =>
                    r.User.Id == context.Entity.User.Id && r.BorrowDate >= today && r.BorrowDate < tomorrow &&
                    r.IsReturned == false);

                if (dailyBorrowedCount >= 3)
                {
                    throw new Exception("Cannot rent more than 3 books in one day");
                }
            }

            return Task.CompletedTask;
        }
    }
}