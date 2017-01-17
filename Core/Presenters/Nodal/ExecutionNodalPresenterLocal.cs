using code_in.Models;
using code_in.Models.NodalModel;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Tiles;
using code_in.Views.NodalView.NodesElems.Tiles.Statements;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace code_in.Presenters.Nodal
{
    public class ExecutionNodalPresenterLocal : ANodalPresenterLocal
    {
        public ExecutionNodalModel ExecModel
        {
            get
            {
                return Model as ExecutionNodalModel;
            }
        }

        public ExecutionNodalView ExecNodalView
        {
            get
            {
                return View as ExecutionNodalView;
            }
        }

        #region this
        public ExecutionNodalPresenterLocal(DeclarationsNodalPresenterLocal assocFile, INodePresenter nodePresenter) :
            base()
        {
            Model = new ExecutionNodalModel(assocFile.DeclModel, nodePresenter.GetASTNode());
            ExecModel.Presenter = this;
        }

        public void EditFunction(FuncDeclItem node)
        {
            this._generateVisualASTFunctionBody(node.MethodNode);
        }
        public void EditAccessor(Accessor node)
        {
            _generateVisualASTPropertyBody(node);
        }
        public void EditConstructor(ConstructorItem node)
        {
            this._generateVisualASTConstructorBody(node.ConstructorNode);
        }
        public void EditDestructor(DestructorItem node)
        {
            this._generateVisualASTDestructorBody(node.DestructorNode);
        }

        protected void _generateVisualASTFunctionBody(ICSharpCode.NRefactory.CSharp.MethodDeclaration method)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            this._generateVisualASTStatements(this.ExecNodalView.RootTileContainer, method.Body);
        }

        protected void _generateVisualASTConstructorBody(ICSharpCode.NRefactory.CSharp.ConstructorDeclaration constructor)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this.ExecNodalView.RootTileContainer, constructor.Body);
        }
        private void _generateVisualASTDestructorBody(ICSharpCode.NRefactory.CSharp.DestructorDeclaration destructor)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this.ExecNodalView.RootTileContainer, destructor.Body);
        }
        protected void _generateVisualASTPropertyBody(ICSharpCode.NRefactory.CSharp.Accessor access)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this.ExecNodalView.RootTileContainer, access.Body);

        }
        #endregion this
        #region IContextMenu
        public override List<Type> GetAvailableNodes()
        {
            List<Type> availableNodes = new List<Type>();

            availableNodes = Tools.Tools.GetAllSubclassesOf(typeof(BaseTile));
            return availableNodes;
        }
        #endregion IContextMenu
        #region INodalPresenter
        public override void User_AddNode_Callback(Type nodeTypeToAdd, Point mousePos)
        {
            if (nodeTypeToAdd != null)
            {
                Dictionary<Type, code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode> types = new Dictionary<Type, code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode>();
                MethodInfo mi = _viewStatic.GetType().GetMethod("CreateAndAddTile");
                MethodInfo gmi = mi.MakeGenericMethod(nodeTypeToAdd);

                types.Add(typeof(BreakStmtTile), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.BREAK_STMT); // TODO not sure

                var astNode = NodePresenter.InstantiateASTNode(types[nodeTypeToAdd]);
                var nodePresenter = new NodePresenter(_viewStatic.Presenter, astNode);
                var array = new object[1];
                array[0] = nodePresenter;
                BaseTile node = gmi.Invoke(_viewStatic, array) as BaseTile;
                node.SetPosition((int)mousePos.X, (int)mousePos.Y);

                //if (_viewStatic.IsDeclarative) // TODO @Seb 05/01/2017
                //{
                //    if (astNode != null)
                //    {
                //        //var thisAst = (_viewStatic._nodalPresenter as ANodalPresenterLocal)._model; // TODO uncomment this
                //        //if (thisAst != null)
                //        //    thisAst.AST.Members.Add(astNode);
                //    }
                //}
            }
            //_viewStatic = null;
        }
        public override bool IsSaved
        {
            get { return ExecModel.IsSaved; }
        }
        public override void Save()
        {
            this.ExecModel.Save();
        }
        public override String DocumentName
        {
            get
            {
                return ""; // TODO not sure if style useful
            }
        }
        #endregion INodalPresenter
    }
}
