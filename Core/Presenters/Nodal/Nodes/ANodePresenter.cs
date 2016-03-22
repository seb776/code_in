using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ICSharpCode.NRefactory.CSharp;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems;

namespace code_in.Presenters.Nodal.Nodes
{
    /// <summary>
    /// Abstract for NodePresenters with common part for all 
    /// Needed:
    ///  Generation of AstNode if null
    /// Done:
    /// SetName
    /// Move
    /// </summary>
    public class ANodePresenter
    {
        ANodePresenter(INodeElem view, INodalPresenter nodalPres, AstNode model = null){
            _view = view;
            _nodalPresenter = nodalPres;
            _model = model;
        }

        Point _coords;
        String _name;
        AstNode _model;
        INodeElem _view;
        INodalPresenter _nodalPresenter;

        public void setName(String name)
        {_name = name;}

        public void Move(Point point)
        {_coords = point;}
    }
}
