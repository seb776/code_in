using code_in.Presenters.Nodal.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles
{
    public interface ITile : ICodeInVisual
    {
        void SetParentView(IVisualNodeContainerDragNDrop vc);
       // void SetRootView(IRootDragNDrop dnd);
        void SetPresenter(INodePresenter nodePresenter);
        // From Seb: This function will be used to update the content of the visual node
        // using infos obtained by the presenter
        void UpdateDisplayedInfosFromPresenter();
    }
}
