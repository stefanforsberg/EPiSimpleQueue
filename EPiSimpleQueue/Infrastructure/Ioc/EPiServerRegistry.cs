using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Data.Dynamic;
using StructureMap.Configuration.DSL;

namespace EPiSimpleQueue.Infrastructure.Ioc
{
    public class EPiServerRegistry : Registry
    {
        public EPiServerRegistry()
        {
            For<DynamicDataStoreFactory>()
                    .Use(() => DynamicDataStoreFactory.Instance);
        }
    }
}
