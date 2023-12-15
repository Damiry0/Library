using API.Models;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Triggers
{
    public class ModifyEditionCountForBooksTableTrigger : IAfterSaveTrigger<Edition>
    {
        private readonly DbContext _applicationContext;

        public ModifyEditionCountForBooksTableTrigger(DbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public Task AfterSave(ITriggerContext<Edition> context, CancellationToken cancellationToken)
        {
            switch (context.ChangeType)
            {
                case ChangeType.Added:
                {
                    context.Entity.Book.AddAmount();
                    break;
                }
                case ChangeType.Deleted:
                {
                    context.Entity.Book.ReduceAmount();
                    break;
                }
            }
            return Task.CompletedTask;
        }
    }
}