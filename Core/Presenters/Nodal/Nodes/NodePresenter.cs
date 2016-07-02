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
    public class NodePresenter : INodePresenter // TODO @z0rg NodePresenter private and INodePresenter public ?
    {
        private AstNode _model = null;
        private INodeElem _view = null;
        private INodalPresenter _nodalPresenter = null;
        private EVirtualNodeType _virtualType;

        public NodePresenter(INodalPresenter nodalPres, AstNode model) {
            System.Diagnostics.Debug.Assert(nodalPres != null);
            System.Diagnostics.Debug.Assert(model != null);
            _nodalPresenter = nodalPres;
            _model = model;
            _virtualType = EVirtualNodeType.AST_NODE;
        }
        /// <summary>
        /// This describes the type of node when it's a node that does not exist in the AST
        /// </summary>
        public enum EVirtualNodeType
        {
            AST_NODE,
            FUNC_ENTRY
        }
        public NodePresenter(INodalPresenter nodalPres, EVirtualNodeType nodeType)
        {
            System.Diagnostics.Debug.Assert(nodalPres != null);
            _nodalPresenter = nodalPres;
            _model = null;
            _virtualType = nodeType;
        }

        public bool SetName(bool updateView, String name)
        {
            Dictionary<Type, bool> setNameRoutines = new Dictionary<Type,bool>(); 
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)] = true;
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)] = true;
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration)] = true;

            var routine = setNameRoutines[_model.GetType()];
            if ((routine != null) && routine)
                (_model as dynamic).Name = name;
            else
                throw new InvalidOperationException("NodePresenter: Trying to set the name of a \"" + _model.GetType() + "\" node");
            if (updateView)
                _view.SetName(name);
            return true;
        }

        public ENodeActions GetActions()
        {
            if (_model != null)
            {
                if (_model.GetType() == typeof(TypeDeclaration))
                {
                    return (ENodeActions.GENERICS & ENodeActions.NAME & ENodeActions.ACCESS_MODIFIERS & ENodeActions.INHERITANCE & ENodeActions.MODIFIERS & ENodeActions.ATTRIBUTE);
                }

            }
            return (ENodeActions.GENERICS & ENodeActions.NAME & ENodeActions.ACCESS_MODIFIERS & ENodeActions.INHERITANCE & ENodeActions.MODIFIERS & ENodeActions.ATTRIBUTE);
        }

        public void AddGeneric(bool updateView, string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveGeneric(bool updateView, int index)
        {
            throw new NotImplementedException();
        }

        public void AddInheritance(bool updateView, string name)
        {
            throw new NotImplementedException();
        }

        void INodePresenter.SetName(bool updateView, string name)
        {
            throw new NotImplementedException();
        }

        static void AddNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void AlignNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void CloseNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void CollapseNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void CollapseAllNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void DuplicateNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void EditNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void ExpandNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void ExpandAllNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void GoIntoNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void HelpNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void RemoveNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }
        static void SaveNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }

        public Tuple<EContextMenuOptions, Action<object[]>>[] GetMenuOptions()
        {
            List<Tuple<EContextMenuOptions, Action<object[]>>> optionsList = new List<Tuple<EContextMenuOptions,Action<object[]>>>();
            if (_model.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)) // for classes, enums, interfaces
            {
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ADD, AddNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.REMOVE, RemoveNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EDIT, EditNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.COLLAPSE, CollapseNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EXPAND, ExpandNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.DUPLICATE, DuplicateNode));
            }
            else if (_model.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration)) // for FuncDeclItem and other items?
            {
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.DUPLICATE, DuplicateNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.REMOVE, RemoveNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EDIT, EditNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.GOINTO, GoIntoNode));
            }
            else if (_model.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)) // for namespace
            {
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.COLLAPSE, CollapseNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EXPAND, ExpandNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.DUPLICATE, DuplicateNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EDIT, EditNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.REMOVE, RemoveNode));
            }
/*            else // basic behaviour to avoid crashes
            {
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ADD, AddNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.REMOVE, RemoveNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EDIT, EditNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.COLLAPSE, CollapseNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EXPAND, ExpandNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.DUPLICATE, DuplicateNode));
            } */
            return optionsList.ToArray();
        }


        public string[] GetAvailableModifiers()
        {
            throw new NotImplementedException();
        }

        public string[] GetAvailableAccessModifiers()
        {
            throw new NotImplementedException();
        }


        public void SetView(INodeElem visualNode)
        {
            System.Diagnostics.Debug.Assert(visualNode != null);
            this._view = visualNode;
        }
    }
}
