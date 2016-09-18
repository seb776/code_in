using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles
{
    public interface ITile
    {
        void SetParentView(IVisualNodeContainerDragNDrop vc);
       // void SetRootView(IRootDragNDrop dnd);
        //void SetNodePresenter(INodePresenter nodePresenter);
    }
}
