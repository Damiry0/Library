using API.Models;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Triggers
{
    public class ModifyEditionStatusOnBorrowTrigger : IAfterSaveTrigger<Borrowing>
    {
        private readonly DbContext _applicationContext;

        public ModifyEditionStatusOnBorrowTrigger(DbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public Task AfterSave(ITriggerContext<Borrowing> context, CancellationToken cancellationToken)
        {
            switch (context.ChangeType)
            {
                case ChangeType.Added:
                {
                    context.Entity.Edition.ChangeBookStatus(Status.Borrowed);
                    break;
                }
                case ChangeType.Modified:
                {
                    if (context.Entity.IsReturned)
                    {
                        context.Entity.Edition.ChangeBookStatus(Status.Available);
                    }
                    break;
                }
            }
            return Task.CompletedTask;
        }
    }
}