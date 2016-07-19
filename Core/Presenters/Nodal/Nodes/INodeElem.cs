using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace code_in.Presenters.Nodal
{
    public interface INodeElem
    {
        // Actions to change/get visualAST displayed state
        void SetName(String name);
        String GetName();
        void AddGeneric(string name, EGenericVariance variance);
        // END
        void SetParentView(IVisualNodeContainerDragNDrop vc); // Each node elem is inside a masterView
        IVisualNodeContainerDragNDrop GetParentView();
        void SetRootView(IRootDragNDrop dnd);
        IRootDragNDrop GetRootView();
        void SetNodePresenter(INodePresenter nodePresenter); // Each visual node has a nodePresenter (TODO unit tests)
        void ShowEditMenu();
        void SetPosition(int posX, int posY);
        void GetSize(out int x, out int y);
        void SetSelected(bool isSelected); // Highlight the nodeElem if isSelected = true
    }
    // enum globale
    public enum EAccessModifier
    {
        PUBLIC = 0,
        PRIVATE = 1,
        PROTECTED = 2,
        INTERNAL = 3,
        PROTECTED_INTERNAL = 4
    }
}
