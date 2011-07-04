using System;
using System.Reflection;
using StructureMap;

namespace EPiSimpleQueue
{
    public class Handler : IHandler
    {
        readonly IContainer _container;

        public Handler(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        public void Execute(object message)
        {
            var handlerType = typeof(IHandleMessage<>).MakeGenericType(message.GetType());
            var handler = _container.GetInstance(handlerType);
            handler.GetType().InvokeMember("Handle", BindingFlags.InvokeMethod, null, handler, new[] { message });
        }
    }
}