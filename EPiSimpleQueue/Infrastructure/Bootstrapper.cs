using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using StructureMap.Graph;

namespace EPiSimpleQueue.Infrastructure
{
    public class Bootstrapper
    {
        static IContainer _container;

        public static IContainer Container
        {
            get
            {
                if(_container == null)
                {
                    throw new InvalidOperationException("Container must be initilaized before accessing it. Have you called CreateContainer?");
                }

                return _container;
            }
        }

        public static IContainer CreateContainer()
        {
            return _container = new Container(c => c.Scan(Scan));
        }

        private static void Scan(IAssemblyScanner s)
        {
            s.AssembliesFromApplicationBaseDirectory();
            s.LookForRegistries();
            s.WithDefaultConventions();
        }
    }
}
