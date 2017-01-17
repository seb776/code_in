using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ICSharpCode.NRefactory.CSharp;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView;
using System.Windows;

namespace code_in.Presenters.Nodal
{
    /// <summary>
    /// INodalPresenter is used to abstract the real NodalPresenter behavior from the view.
    /// It will proabably be used later for collaborative development by implementing a ServerNodalPresenter and a ClientNodalPresenter.
    /// </summary>
    public interface INodalPresenter : IContextMenu 
    {
        INodalView View
        {
            get;
            set;
        }
        String DocumentName
        {
            get;
        }
        void Save();
        bool IsSaved
        {
            get;
        }

        void User_AddNode_Callback(Type nodeTypeToAdd, Point mousePos);
    }


}
