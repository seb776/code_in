using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView
{
    public interface INodalView : IRootDragNDrop, ILinkContainer
    {
        void AlignNodes(double deltaTime);
        ITileContainer RootTileContainer
        {
            get;
            set;
        }
    }
}
