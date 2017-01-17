using code_in.Models.NodalModel;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace code_in.Presenters.Nodal
{
    public class DeclarationsNodalPresenterLocal : ANodalPresenterLocal
    {
        public override void User_AddNode_Callback(Type nodeTypeToAdd, Point mousePos)
        {
            if (nodeTypeToAdd != null)
            {
                Dictionary<Type, code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode> types = new Dictionary<Type, code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode>();
                MethodInfo mi = _viewStatic.GetType().GetMethod("CreateAndAddNode");
                MethodInfo gmi = mi.MakeGenericMethod(nodeTypeToAdd);

                types.Add(typeof(UsingDeclNode), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.USING_DECL); // TODO not sure
                types.Add(typeof(NamespaceNode), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.NAMESPACE_DECL);
                types.Add(typeof(ClassDeclNode), code_in.Presenters.Nodal.Nodes.NodePresenter.ECSharpNode.TYPE_DECL);

                var astNode = NodePresenter.InstantiateASTNode(types[nodeTypeToAdd]);
                var nodePresenter = new NodePresenter(_viewStatic.Presenter, astNode);
                var array = new object[1];
                array[0] = nodePresenter;
                BaseNode node = gmi.Invoke(_viewStatic, array) as BaseNode;
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
        public DeclarationsNodalModel DeclModel
        {
            get
            {
                return Model as DeclarationsNodalModel;
            }
        }
        public override String DocumentName
        {
            get
            {
                return DeclModel.FileName;
            }
        }
        public override void Save()
        {
            this.DeclModel.Save();
        }
        public void Save(string filePath)
        {
            this.DeclModel.Save(filePath);
        }
        public DeclarationsNodalPresenterLocal() :
            base()
        {

        }
        public override List<Type> GetAvailableNodes()
        {
            List<Type> tmp = new List<Type>();

            tmp.Add(typeof(ClassDeclNode));
            tmp.Add(typeof(NamespaceNode));
            tmp.Add(typeof(UsingDeclNode));
            if (false)
            {
                //TODO zorg
            }
            return (tmp);
        }
        public bool OpenFile(String path)
        {
            try
            {
                Model = new DeclarationsNodalModel(path);
                DeclModel.Presenter = this;
                this._generateVisualASTDeclaration(DeclModel.AST, this.View);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        public override bool IsSaved
        {
            get { return DeclModel.IsSaved; }
        }
    }
}
