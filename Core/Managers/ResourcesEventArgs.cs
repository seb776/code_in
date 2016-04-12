using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Managers
{
    public class ResourcesEventArgs : EventArgs
    {
        public object OldValue { get; private set; }
        public object NewValue { get; private set; }

        public ResourcesEventArgs(object oldValue, object newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
