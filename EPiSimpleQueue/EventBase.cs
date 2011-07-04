using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPiSimpleQueue
{

    [CLSCompliant(true)]
    public abstract class MessageBase
    {
        public virtual MessageBase ShallowCopy()
        {
            var copiedMessage = (MessageBase)base.MemberwiseClone();
            return copiedMessage;
        }
    }
}
