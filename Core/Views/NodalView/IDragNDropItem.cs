using code_in.Views.NodalView.NodesElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView
{
    public interface IDragNDropItem : ICodeInVisual
    {
        void SelectHighLight(bool highlighetd);
        void MustBeRemovedFromContext(); // To say the RootDragNDropManager to remove nodes from their context before starting to drag them.
        void RemoveFromContext();
        void SetParentView(IContainerDragNDrop vc);
        IContainerDragNDrop GetParentView();
    }
}
