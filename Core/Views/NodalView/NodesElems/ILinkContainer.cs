using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems
{
    public interface ILinkContainer
    {
        bool DraggingLink
        {
            get;
            set;
        }
        void DragLink(AIOAnchor from, bool isGenerated);
        void DropLink(AIOAnchor to, bool isGenerated);
        void UpdateLinkDraw(Point mousePosRelToParentLinkContainer); // This function is here to update the view when mouse is moving (update view and links)
    }
}
