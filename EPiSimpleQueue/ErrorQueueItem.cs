using System;
using EPiServer.Data.Dynamic;

namespace EPiSimpleQueue
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class ErrorQueueItem : IIdentity
    {
        public string ExceptionMessage { get; set; }
        public IMessage Message { get; set; }
        public Guid Id { get; set; }

        public static ErrorQueueItem FromQueueItem(IMessage message, Exception error)
        {
            return new ErrorQueueItem
                       {
                           ExceptionMessage = error.ToString(),
                           Message = message//ShallowCopy()
                       };
        }
    }
}