using API.Models;
using BooksAPI.Exceptions;
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
                    throw new TriggerException("Cannot modify user credentials");
                }
            }

            return Task.CompletedTask;
        }
    }
}