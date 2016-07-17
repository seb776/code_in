using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ICSharpCode.NRefactory.CSharp;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;

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
        private List<Tuple<string, EGenericVariance>> GenericList = null;
        private List<string> InheritanceList = null;

        public NodePresenter(INodalPresenter nodalPres, AstNode model) {
            System.Diagnostics.Debug.Assert(nodalPres != null);
            System.Diagnostics.Debug.Assert(model != null);
            _nodalPresenter = nodalPres;
            _model = model;
            _virtualType = EVirtualNodeType.AST_NODE;
            GenericList = new List<Tuple<string, EGenericVariance>>();
            GetExistingGenericsFromNode();
            InheritanceList = new List<string>();
            GetExistingInheritanceFromNode();
        }

        private void GetExistingGenericsFromNode()
        {
            if (_model.GetType() == typeof(TypeDeclaration))
            {
                Tuple<string, EGenericVariance> ExistingGeneric;
                foreach (var tmp in (_model as TypeDeclaration).TypeParameters)
                {
                    if (tmp.Variance == ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Contravariant)
                        ExistingGeneric = new Tuple<string, EGenericVariance>(tmp.Name.ToString(), EGenericVariance.IN);
                    else if (tmp.Variance == ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Covariant)
                        ExistingGeneric = new Tuple<string, EGenericVariance>(tmp.Name.ToString(), EGenericVariance.OUT);
                    else
                        ExistingGeneric = new Tuple<string, EGenericVariance>(tmp.Name.ToString(), EGenericVariance.NOTHING);
                    GenericList.Add(ExistingGeneric);
                }
            }
        }

        private void GetExistingInheritanceFromNode()
        {
            if (_model.GetType() == typeof(TypeDeclaration) && (_model as TypeDeclaration).BaseTypes != null)
            {
                foreach (var inherit in (_model as TypeDeclaration).BaseTypes)
                    InheritanceList.Add(inherit.ToString());
            }
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

        // Return actions according to types
        public ENodeActions GetActions()
        {
            if (_model != null)
            {
                if (_model.GetType() == typeof(TypeDeclaration))
                    return (ENodeActions.NAME | ENodeActions.ACCESS_MODIFIERS | ENodeActions.INHERITANCE | ENodeActions.MODIFIERS | ENodeActions.ATTRIBUTE | ENodeActions.COMMENT | ENodeActions.GENERICS);
                if (_model.GetType() == typeof(NamespaceDeclaration))
                    return (ENodeActions.NAME | ENodeActions.COMMENT);
                if (_model.GetType() == typeof(MethodDeclaration))
                    return (ENodeActions.ATTRIBUTE | ENodeActions.COMMENT | ENodeActions.ACCESS_MODIFIERS | ENodeActions.MODIFIERS | ENodeActions.NAME | ENodeActions.GENERICS);
                if (_model.GetType() == typeof(FieldDeclaration))
                    return (ENodeActions.NAME | ENodeActions.ACCESS_MODIFIERS | ENodeActions.MODIFIERS | ENodeActions.COMMENT | ENodeActions.ATTRIBUTE);
                if (_model.GetType() == typeof(PropertyDeclaration))
                    return (ENodeActions.ACCESS_MODIFIERS | ENodeActions.COMMENT);
                if (_model.GetType() == typeof(UsingDeclaration))
                    return (ENodeActions.COMMENT);
                if (_model.GetType() == typeof(ObjectCreateExpression))
                    return (ENodeActions.COMMENT | ENodeActions.EXEC_TYPE);
                if (_model.GetType() == typeof(IdentifierExpression))
                    return (ENodeActions.TEXT | ENodeActions.COMMENT);
                if (_model.GetType() == typeof(MemberReferenceExpression))
                    return (ENodeActions.TEXT | ENodeActions.COMMENT);
                if (_model.GetType() == typeof(InvocationExpression))
                    return (ENodeActions.EXEC_PARAMETERS | ENodeActions.EXEC_GENERICS | ENodeActions.COMMENT);
                if (_model.GetType() == typeof(PrimitiveExpression))
                    return (ENodeActions.TEXT | ENodeActions.COMMENT);
            }
            return (0);
        }

        // methods for the Nodes modification

        // this method changes the name into ast and update the view
        public void SetName(String name)
        {
            Dictionary<Type, bool> setNameRoutines = new Dictionary<Type, bool>();
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)] = true;
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)] = true;
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration)] = true;
            setNameRoutines[typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration)] = true;

            var routine = setNameRoutines[_model.GetType()];
            if ((routine != null) && routine)
                (_model as dynamic).Name = name;
            else
                throw new InvalidOperationException("NodePresenter: Trying to set the name of a \"" + _model.GetType() + "\" node");
            _view.SetName(name);
        }

        // this method add a generic into the genericList and update the view -> add the new generic to view

        public void AddGeneric(string name, Views.NodalView.NodesElems.Nodes.Assets.EGenericVariance variance)
        {
            if (_model.GetType() == typeof(TypeDeclaration))
            {
                TypeParameterDeclaration NewGeneric = new TypeParameterDeclaration();
                Tuple<string, EGenericVariance> NewGenericInList = null;
                var typeDecl = _model as TypeDeclaration;
                NewGeneric.Name = name;
                if (variance == Views.NodalView.NodesElems.Nodes.Assets.EGenericVariance.IN)
                {
                    NewGeneric.Variance = ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Contravariant;
                    NewGenericInList = new Tuple<string, EGenericVariance>(name, EGenericVariance.IN);
                }
                if (variance == Views.NodalView.NodesElems.Nodes.Assets.EGenericVariance.OUT)
                {
                    NewGeneric.Variance = ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Covariant;
                    NewGenericInList = new Tuple<string, EGenericVariance>(name, EGenericVariance.OUT);
                }
                if (variance == Views.NodalView.NodesElems.Nodes.Assets.EGenericVariance.NOTHING)
                {
                    NewGeneric.Variance = ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Invariant;
                    NewGenericInList = new Tuple<string, EGenericVariance>(name, EGenericVariance.NOTHING);
                }
                typeDecl.TypeParameters.Add(NewGeneric);
                GenericList.Add(NewGenericInList);
            }
            (_view as IContainingGenerics).setGenerics(GenericList);
            UpdateGenericsInAst();
        }

        // this method allow the get of the GenericList

        public List<Tuple<string, EGenericVariance>> getGenericList()
        {
            if (GenericList != null)
                return (GenericList);
            else
                return (new List<Tuple<string,EGenericVariance>>());
        }

        // this method is an event called when the Generic name is modified

        public void ModifGenericName(string name, int index)
        {
            if (GenericList.Count > index)
            {
                EGenericVariance CopyVariance;
                CopyVariance = GenericList[index].Item2;
                GenericList.RemoveAt(index);
                Tuple<string, EGenericVariance> ModifTuple = new Tuple<string, EGenericVariance>(name, CopyVariance);
                GenericList.Insert(index, ModifTuple);
                (_view as IContainingGenerics).setGenerics(GenericList);
            }
            UpdateGenericsInAst();
        }

        // this method allow the modification of an existing Generic variance's
        public void ModifGenericVariance(int index, EGenericVariance variance, string name)
        {
            if (GenericList.Count > index)
            {
                GenericList.RemoveAt(index);
                Tuple<string, EGenericVariance> ModifTuple = new Tuple<string, EGenericVariance>(name, variance);
                GenericList.Insert(index, ModifTuple);
                (_view as IContainingGenerics).setGenerics(GenericList);
            }
            UpdateGenericsInAst();
        }

        // return true if the genericName in parameters match in List

        public bool ifGenericExist(string name)
        {
            foreach (var tmp in GenericList)
            {
                if (tmp.Item1.Equals(name))
                    return true;
            }
            return false;
        }

        // this method allow the remove of and existing generic

        public void RemoveGeneric(int index)
        {
            if (GenericList.Count > index)
            {
                GenericList.RemoveAt(index);
                (_view as IContainingGenerics).setGenerics(GenericList);
            }
            UpdateGenericsInAst();
        }

        public void UpdateGenericsInAst()
        {
            if (_model.GetType() == typeof(TypeDeclaration))
            {
                var TypeDecl = (_model as TypeDeclaration);

                TypeDecl.TypeParameters.Clear();
                TypeParameterDeclaration updatedGeneric;
                foreach(Tuple<string, EGenericVariance> generic in GenericList)
                {
                    if (generic.Item2 == EGenericVariance.IN)
                    {
                        updatedGeneric = new TypeParameterDeclaration(generic.Item1);
                        updatedGeneric.Variance = ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Contravariant;
                    }
                    else if (generic.Item2 == EGenericVariance.OUT)
                    {
                        updatedGeneric = new TypeParameterDeclaration(generic.Item1);
                        updatedGeneric.Variance = ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Covariant;
                    }
                    else
                    {
                        updatedGeneric = new TypeParameterDeclaration(generic.Item1);
                        updatedGeneric.Variance = ICSharpCode.NRefactory.TypeSystem.VarianceModifier.Invariant;
                    }
                    TypeDecl.TypeParameters.Add(updatedGeneric);
                }
            }
        }

        public void AddInheritance(string name)
        {
            InheritanceList.Add(name);
            SimpleType NewInheriance = new SimpleType();
            NewInheriance.Identifier = name;
            (_model as TypeDeclaration).BaseTypes.Add(NewInheriance);
            (_view as IContainingInheritance).ManageInheritance(InheritanceList); // here Nodalview update
        }
        public void RemoveInheritance(int index)
        {
            if (InheritanceList.Count > index)
            {
                InheritanceList.RemoveAt(index);
                (_model as TypeDeclaration).BaseTypes.Remove((_model as TypeDeclaration).BaseTypes.ElementAt(index));
                (_view as IContainingInheritance).ManageInheritance(InheritanceList); // here Nodalview update
            }
        }

        public void ChangeInheritanceName(int index, string name)
        {
            SimpleType newNamedInheritance = new SimpleType();
            newNamedInheritance.Identifier = name;
            if (InheritanceList.Count > index)
            {
                InheritanceList.RemoveAt(index);
                InheritanceList.Insert(index, name);
                (_model as TypeDeclaration).BaseTypes.InsertAfter((_model as TypeDeclaration).BaseTypes.ElementAt(index), newNamedInheritance);
                (_model as TypeDeclaration).BaseTypes.Remove((_model as TypeDeclaration).BaseTypes.ElementAt(index));
                (_view as IContainingInheritance).ManageInheritance(InheritanceList); // here Nodalview update
            }

        }

        public List<string> getInheritanceList()
        {
            return (InheritanceList);
        }

        public void SetAccesModifier(string AccessModifier)
        {
            if (_model.GetType() == typeof(TypeDeclaration))
            {
                var typeDecl = (_model as TypeDeclaration);
                Modifiers tmp = typeDecl.Modifiers;
                typeDecl.Modifiers = typeDecl.Modifiers & ~Modifiers.VisibilityMask;
                if (AccessModifier == "public")
                    typeDecl.Modifiers |= Modifiers.Public;
                if (AccessModifier == "private")
                    typeDecl.Modifiers |= Modifiers.Private;
                if (AccessModifier == "protected")
                    typeDecl.Modifiers |= Modifiers.Protected;
                if (AccessModifier == "internal")
                    typeDecl.Modifiers |= Modifiers.Internal;
                (_view as IContainingAccessModifiers).setAccessModifiers(typeDecl.Modifiers);
            }
        }

        public void SetOtherModifiers(string OtherModifiers, bool AddOrRemove) // if true -> Add, else -> Remove
        {
            if (AddOrRemove == true)
            {
                if (_model.GetType() == typeof(TypeDeclaration))
                {
                    var typeDecl = (_model as TypeDeclaration);
                    if (OtherModifiers == "virtual")
                        typeDecl.Modifiers |= Modifiers.Virtual;
                    if (OtherModifiers == "abstract")
                        typeDecl.Modifiers |= Modifiers.Abstract;
                    if (OtherModifiers == "override")
                        typeDecl.Modifiers |= Modifiers.Override;
                    (_view as IContainingModifiers).setModifiersList(typeDecl.Modifiers); // update more than a set-> semantic detail
                }
            }
            else if (AddOrRemove == false)
            {
                if (_model.GetType() == typeof(TypeDeclaration))
                {
                    var typeDecl = (_model as TypeDeclaration);
                    if (OtherModifiers == "virtual")
                        typeDecl.Modifiers &= ~Modifiers.Virtual;
                    if (OtherModifiers == "abstract")
                        typeDecl.Modifiers &= ~Modifiers.Abstract;
                    if (OtherModifiers == "override")
                        typeDecl.Modifiers &= ~Modifiers.Override;
                    (_view as IContainingModifiers).setModifiersList(typeDecl.Modifiers);
                }
            }
        }

        public void SetExecParams(int paramsNumber)
        {
/*            while (paramsNumber >= 0)
            {
//                _view.
            }*/
        }

        public void AddExecGeneric()
        {

        }

        // methods for the contextMenu
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
            NodePresenter nodePresenter = objects[0] as NodePresenter;

            nodePresenter._view.ShowEditMenu();

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

        //This part bind methods to the options of the contextMenu
        public Tuple<EContextMenuOptions, Action<object[]>>[] GetMenuOptions()
        {
            List<Tuple<EContextMenuOptions, Action<object[]>>> optionsList = new List<Tuple<EContextMenuOptions,Action<object[]>>>();
            if (_model == null)
                return optionsList.ToArray();
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
