using System;
using EPiServer.Data.Dynamic;

namespace EPiSimpleQueue
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class QueueItem : IIdentity
    {
        public IMessage Message { get; set; }
        public Guid Id { get; set; }
    }
}