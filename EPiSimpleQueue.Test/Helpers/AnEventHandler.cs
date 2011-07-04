using System;

namespace EPiSimpleQueue.Test.Helpers
{
    public class AnEventHandler : IHandleMessage<AnEvent>
    {
        public void Handle(AnEvent message)
        {
            
        }
    }

    public class AnEventHandlerThatWillThrow : IHandleMessage<AnEventThatWillThrow>
    {
        public void Handle(AnEventThatWillThrow message)
        {
            throw message.Exception;
        }
    }
}