using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems
{
    public interface IIOAnchorContainer
    {
        T CreateAndAddInput<T>() where T : AIOAnchor;
        T CreateAndAddOutput<T>() where T : AIOAnchor;
        ILinkContainer ParentLinksContainer
        {
            get;
            set;
        }
    }
}
