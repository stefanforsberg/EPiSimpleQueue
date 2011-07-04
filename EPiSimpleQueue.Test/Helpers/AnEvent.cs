using System;

namespace EPiSimpleQueue.Test.Helpers
{
    public class AnEvent : MessageBase
    {
    }

    public class AnEventThatWillThrow : MessageBase
    {
        public Exception Exception { get; set; }
    }
}