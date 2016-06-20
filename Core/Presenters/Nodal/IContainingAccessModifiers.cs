using code_in.Views.NodalView.NodesElems.Items.Assets;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Presenters.Nodal
{
    interface IContainingAccessModifiers
    {
        void setAccessModifiers(Modifiers tmp);
    }
}
