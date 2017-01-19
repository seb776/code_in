using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;

namespace code_in.Presenters.Nodal
{
    public interface IContainingAttribute
    {
        void setExistingAttributes(List<KeyValuePair<string, string>> list);
        void addAttribute(string type, string arg);
        void delAttribute(int index);
    }
}
