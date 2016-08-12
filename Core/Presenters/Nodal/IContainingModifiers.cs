using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Presenters.Nodal
{
    interface IContainingModifiers
    {
        void setModifiersList(Modifiers modifiers); // TODO @Mo the view should not be aware of NRefactory types
    }
}
