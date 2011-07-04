using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Configuration.DSL;


namespace EPiSimpleQueue.Infrastructure.Ioc
    {
    public class SimpleQueueRegistry : Registry
    {
        public SimpleQueueRegistry()
        {
            Scan(a =>
            {
                a.AssembliesFromApplicationBaseDirectory();
                a.AssemblyContainingType(typeof(IHandleMessage<>));
                a.ConnectImplementationsToTypesClosing(typeof(IHandleMessage<>));
            });
        }
    }
}
