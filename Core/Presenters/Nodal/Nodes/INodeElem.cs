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
        void SetName(String name);
        String GetName();
        void AddGeneric(string name, EGenericVariance variance);
        void SetParentView(IVisualNodeContainer vc); // Each node elem is inside a masterView
        IVisualNodeContainer GetParentView();
        void SetRootView(IVisualNodeContainerDragNDrop dnd);
        IVisualNodeContainerDragNDrop GetRootView();
        void SetNodePresenter(INodePresenter nodePresenter);
        void ShowEditMenu();
        void SetPosition(int posX, int posY);
        void GetSize(out int x, out int y);
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
