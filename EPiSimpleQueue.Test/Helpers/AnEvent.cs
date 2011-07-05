using System;

namespace EPiSimpleQueue.Test.Helpers
{
    public class AnEvent : IMessage
    {
    }

    public class AnEventThatWillThrow : IMessage
    {
        public Exception Exception { get; set; }
    }
}