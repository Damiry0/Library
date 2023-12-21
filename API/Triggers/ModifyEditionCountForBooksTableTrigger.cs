using API.Models;
using EntityFrameworkCore.Triggered;

namespace BooksAPI.Triggers
{
    public class ModifyEditionCountForBooksTableTrigger : IAfterSaveTrigger<Edition>
    {
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