using System;
using EPiServer.Data.Dynamic;

namespace EPiSimpleQueue
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class ErrorQueueItem : IIdentity
    {
        public string ExceptionMessage { get; set; }
        public MessageBase Message { get; set; }
        public Guid Id { get; set; }

        public static ErrorQueueItem FromQueueItem(MessageBase message, Exception error)
        {
            
            //System.Reflection.MethodInfo inst = message.GetType().GetMethod("MemberwiseClone",
            //    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            //var clone = inst.Invoke(message, null);


            return new ErrorQueueItem
                       {
                           ExceptionMessage = error.ToString(),
                           Message = message.ShallowCopy()
                       };
        }
    }


    public class TestTest
    {
        public DateTime Timez { get; set; }
    }
}