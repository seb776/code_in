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
    /// NodePresenter class
    /// Used to link the visual nodes with the NRefactory AST nodes
    /// Needed:
    ///  Generation of AstNode if null
    /// Done:
    /// SetName
    /// Move (kinda)
    /// </summary>
    public class NodePresenter
    {
        public NodePresenter(INodeElem view, INodalPresenter nodalPres, AstNode model = null){
            _view = view;
            _nodalPresenter = nodalPres;
            _model = model;
        }

     //   Point _coords;
        String _name;
        AstNode _model;
        INodeElem _view;
        INodalPresenter _nodalPresenter;

        public bool setName(String name){
            _name = name;
            //_view.SetName(_name);
            if (_model == null)
                return false;
            #region Classes (interface, class, enum
            if (_model.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)){
                ((ICSharpCode.NRefactory.CSharp.TypeDeclaration)_model).Name = name;
            }
            #endregion
            #region Namespace
            if (_model.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)){ 
                ((ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)_model).Name = name; }
            #endregion
            #region Methods
            if (_model.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration)){
                ((ICSharpCode.NRefactory.CSharp.MethodDeclaration)_model).Name = _name;
            }
            #endregion
            return true;
        }

        public void setModel(AstNode model)
        { _model = model; }

       /* public void Move(Point point){
            _coords = point;}*/
    }
}
