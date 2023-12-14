using API.Context;
using API.Models;
using EntityFrameworkCore.Triggered;

namespace BooksAPI.Triggers
{
    public class BlockChangeUserNameTrigger : IBeforeSaveTrigger<User>
    {
        public Task BeforeSave(ITriggerContext<User> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Modified)
            {
                if (context.UnmodifiedEntity?.FirstName != context.Entity.FirstName)
                {
                    throw new Exception("kupsko dupa");
                }
            }
            return Task.CompletedTask;
        }
    }
}