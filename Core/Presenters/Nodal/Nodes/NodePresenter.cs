﻿using System;
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
        private AstNode _model;
        private INodeElem _view;
        private INodalPresenter _nodalPresenter;

        public NodePresenter(INodeElem view, INodalPresenter nodalPres, AstNode model) {
            System.Diagnostics.Debug.Assert(view != null);
            System.Diagnostics.Debug.Assert(nodalPres != null);
            System.Diagnostics.Debug.Assert(model != null);
            _view = view;
            _nodalPresenter = nodalPres;
            _model = model;
        }

        public NodePresenter(INodeElem view, INodalPresenter nodalPres, Type modelType)
        {

        }



        public bool SetName(bool updateView, String name)
        {
            Dictionary<Type, bool> setNameRoutines = new Dictionary<Type,bool>(); 
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)] = true;
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)] = true;
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration)] = true;

            var routine = setNameRoutines[_model.GetType()];
            if (routine != null && routine)
                (_model as dynamic).Name = name;
            else
                throw new InvalidOperationException("NodePresenter: Trying to set the name of a \"" + _model.GetType() + "\" node");
            if (updateView)
                _view.SetName(name);
            return true;
        }

        public ENodeActions GetActions()
        {
            throw new NotImplementedException();
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

        public Tuple<EContextMenuOptions, Action<object[]>>[] GetMenuOptions()
        {
            throw new NotImplementedException();
        }


        public string[] GetAvailableModifiers()
        {
            throw new NotImplementedException();
        }

        public string[] GetAvailableAccessModifiers()
        {
            throw new NotImplementedException();
        }
    }
}
