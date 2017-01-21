using code_in.Models.NodalModel;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using ICSharpCode.NRefactory.CSharp;
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

        protected void _generateVisualASTDeclaration(AstNode node, IVisualNodeContainer parentContainer)
        {
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.SyntaxTree))
            {
                var usingNodePresenter = new NodePresenter(this, null); // This node presenter does not reflect a node in the AST as our representation is different than the AST
                var usingNodeView = parentContainer.CreateAndAddNode<UsingDeclNode>(usingNodePresenter); // We have one using view for the global scope

                var syntaxTree = node as ICSharpCode.NRefactory.CSharp.SyntaxTree;
                foreach (var decl in syntaxTree.Members)
                    this._generateVisualASTDeclarationRecur(decl, parentContainer, usingNodeView);
                if (usingNodeView._orderedLayout.Children.Count == 0) // If there is no using at all we remove it
                    usingNodeView.Remove();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected void _generateVisualASTDeclarationRecur(AstNode node, IVisualNodeContainer parentContainer, UsingDeclNode parentUsingDeclNode)
        {
            INodeElem visualNode = null;
            var nodePresenter = new NodePresenter(this, node);
            if (node.Children == null)
                return;

            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
            {
                NamespaceNode namespaceNode = parentContainer.CreateAndAddNode<NamespaceNode>(nodePresenter);
                var namespaceDecl = node as ICSharpCode.NRefactory.CSharp.NamespaceDeclaration;
                var usingNodePres = new NodePresenter(this, null);
                UsingDeclNode usingDeclView = namespaceNode.CreateAndAddNode<UsingDeclNode>(usingNodePres);

                visualNode = namespaceNode;
                namespaceNode.SetName(namespaceDecl.Name);
                foreach (var member in namespaceDecl.Members)
                    this._generateVisualASTDeclarationRecur(member, namespaceNode, usingDeclView);
                if (usingDeclView._orderedLayout.Children.Count == 0) // TODO do better than this
                    usingDeclView.Remove();
            }
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UsingDeclaration))
            {
                UsingDeclItem usingItem = null;
                visualNode = usingItem = parentUsingDeclNode.CreateAndAddNode<UsingDeclItem>(nodePresenter);
                usingItem.SetName(nodePresenter.GetASTNode().ToString());
            }
            #region Classes (interface, struct, class, enum)
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)) // Handles class, struct, enum (see further)
            {
                var tmpNode = (ICSharpCode.NRefactory.CSharp.TypeDeclaration)node;
                #region Enum
                if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Enum)
                {
                    ClassDeclNode enumDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>(nodePresenter);
                    visualNode = enumDeclNode;
                    enumDeclNode.SetClassType(code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode.EType.ENUM);
                    enumDeclNode.SetName(tmpNode.Name);

                    foreach (var v in tmpNode.Members)
                    {
                        var tmp = v as EnumMemberDeclaration;
                        var item = enumDeclNode.CreateAndAddNode<ClassItem>(nodePresenter);
                        if (tmp.Initializer.IsNull == false)
                            item.SetName(v.Name + " = " + tmp.Initializer.ToString());
                        else
                            item.SetName(v.Name);
                    }
                    setOtherModifiers(enumDeclNode, tmpNode.Modifiers);
                }
                #endregion Enum
                #region Class
                else if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Class)
                {
                    ClassDeclNode classDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>(nodePresenter);
                    visualNode = classDeclNode;
                    classDeclNode.SetName(tmpNode.Name);
                    setAccessModifiers(classDeclNode, tmpNode.Modifiers);
                    setOtherModifiers(classDeclNode, tmpNode.Modifiers);

                    //inheritance
                    InitInheritance(classDeclNode, tmpNode);
                    //Generic
                    SetAllGenerics(classDeclNode, tmpNode);
                    //Attributes
                    InitAttributes(classDeclNode, tmpNode);
                    //Constraint
                    foreach (var constraint in tmpNode.Constraints)
                    {
                        classDeclNode.setConstraint(constraint.TypeParameter.ToString(), constraint.BaseTypes);
                    }
                    foreach (var member in tmpNode.Members)
                        _generateVisualASTDeclarationRecur(member, classDeclNode, null); // No using in typeDecl
                }
                #endregion Class
                #region Interface
                else if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Interface)
                {
                    ClassDeclNode interfaceDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>(nodePresenter);
                    visualNode = interfaceDeclNode;
                    interfaceDeclNode.SetClassType(code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode.EType.INTERFACE);
                    interfaceDeclNode.SetName(tmpNode.Name);
                    setAccessModifiers(interfaceDeclNode, tmpNode.Modifiers);

                    //inheritance
                    InitInheritance(interfaceDeclNode, tmpNode);
                    //Generic
                    SetAllGenerics(interfaceDeclNode, tmpNode);

                    foreach (var member in tmpNode.Members)
                        _generateVisualASTDeclarationRecur(member, interfaceDeclNode, null); // No using in interface
                }
                #endregion Interface
            }
            #endregion Classes (interface, struct, class, enum)
            #region Field
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration))
            {
                var fieldDecl = node as ICSharpCode.NRefactory.CSharp.FieldDeclaration;
                var item = parentContainer.CreateAndAddNode<ClassItem>(nodePresenter);
                visualNode = item;
                item.setTypeFromString(fieldDecl.ReturnType.ToString()); //Type setter for variable base -> TypeInfo.xaml.cs
                string varName = null;
                foreach (var variable in fieldDecl.Variables)
                {
                    if (variable != fieldDecl.Variables.Last<VariableInitializer>())
                        varName += variable.Name + ", ";
                    else
                        varName += variable.Name;
                }
                item.SetName(varName);
                setAccessModifiers(item, fieldDecl.Modifiers); // here just call setAccessModifiers from the interface
                setOtherModifiers(item, fieldDecl.Modifiers);
            }
            #endregion Field
            #region Property (get, set)
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.PropertyDeclaration))
            {
                var propertyDecl = node as ICSharpCode.NRefactory.CSharp.PropertyDeclaration;
                var item = parentContainer.CreateAndAddNode<PropertyItem>(nodePresenter);
                visualNode = item;
                item.PropertyNode = propertyDecl;
                item.SetName(propertyDecl.Name.ToString()); // TODO Complete
                item.setTypeFromString(propertyDecl.ReturnType.ToString());
                setAccessModifiers(item, propertyDecl.Modifiers);
            }
            #endregion Property (get, set)
            #region Constructor
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ConstructorDeclaration))
            {
                ConstructorItem constructorDecl = parentContainer.CreateAndAddNode<ConstructorItem>(nodePresenter);
                visualNode = constructorDecl;
                constructorDecl.ConstructorNode = node as ConstructorDeclaration;
                ConstructorDeclaration construct = node as ConstructorDeclaration;
                constructorDecl.SetName(construct.Name);
                setAccessModifiers(constructorDecl, construct.Modifiers);

                var parameters = construct.Parameters.ToList();
                for (int i = 0; i < parameters.Count; i++)
                {
                    constructorDecl.AddParam(parameters[i].Type.ToString());
                }
            }
            #endregion Constructor
            #region Destructor
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.DestructorDeclaration))
            {
                DestructorItem constructorDecl = parentContainer.CreateAndAddNode<DestructorItem>(nodePresenter);
                visualNode = constructorDecl;
                constructorDecl.DestructorNode = node as DestructorDeclaration;
                DestructorDeclaration construct = node as DestructorDeclaration;
                constructorDecl.SetName(construct.Name);
                setAccessModifiers(constructorDecl, construct.Modifiers);

            }
            #endregion Destructor
            #region Method
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration))
            {
                FuncDeclItem funcDecl = parentContainer.CreateAndAddNode<FuncDeclItem>(nodePresenter);
                visualNode = funcDecl;
                funcDecl.MethodNode = node as ICSharpCode.NRefactory.CSharp.MethodDeclaration;
                ICSharpCode.NRefactory.CSharp.MethodDeclaration method = node as ICSharpCode.NRefactory.CSharp.MethodDeclaration;
                var parameters = method.Parameters.ToList();
                for (int i = 0; i < parameters.Count; ++i)
                {
                    funcDecl.AddParam(parameters[i].Type.ToString());
                    //var item = funcDecl.CreateAndAddNode<ClassItem>(); // TODO ArgItem
                    //item.SetName(parameters[i].Name);
                    //item.SetItemType(parameters[i].Type.ToString());
                }
                funcDecl.UpdateDisplayedInfosFromPresenter();
            }
            #endregion Method
        }

        #region INodalPresenter
        public override bool IsSaved
        {
            get { return DeclModel.IsSaved; }
        }
        #endregion INodalPresenter
    }
}
