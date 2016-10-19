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
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Items;
using System.Windows.Controls;
using System.Reflection;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System.Windows.Input;
using code_in.Views.NodalView.NodesElems.Anchors;

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
        private List<string> ModifiersList = null;
        private List<string> AttributesList = null;
        private string _type = null;
        private int ExecParamsNb;
        public AstNode GetASTNode()
        {
            return _model;
        }
        public NodePresenter(INodalPresenter nodalPres, AstNode model)
        {
            System.Diagnostics.Debug.Assert(nodalPres != null);
            //System.Diagnostics.Debug.Assert(model != null);
            _nodalPresenter = nodalPres;
            _model = model;
            _virtualType = EVirtualNodeType.AST_NODE;
            GenericList = new List<Tuple<string, EGenericVariance>>();
            GetExistingGenericsFromNode();
            InheritanceList = new List<string>();
            GetExistingInheritanceFromNode();
            ModifiersList = new List<string>();
            GetExistingModifiersFromNode();
            AttributesList = new List<string>();
            GetTypeFromNode();
            LoadExecParamsCount();
            //GetExistingAttributesFromNode();
            //TODO getExistingmodifiers then 
        }

        private void GetTypeFromNode()
        {
            if (_model.GetType() == typeof(FieldDeclaration))
            {
                var ast = _model as FieldDeclaration;
                _type = ast.ReturnType.ToString();
            }
            if (_model.GetType() == typeof(MethodDeclaration))
            {
                var ast = _model as MethodDeclaration;
                _type = ast.ReturnType.ToString();
            }
        }

        public string getType()
        {
            return (_type);
        }

        private void GetExistingAttributesFromNode()
        {
            throw new NotImplementedException();
            //TODO recup infos attributes dans l'ast et stocke dans la liste
        }

        public List<string> getModifiersList()
        {
            return (ModifiersList);
        }
        public void InsertStatementAfter(Statement stmt)
        {
            if (_model == null)
                return;
            if (this._model is Statement)
            {
                Statement curStmt = this._model as Statement;
                if (curStmt.Parent is BlockStatement)
                {
                    var parentBlock = curStmt.Parent as BlockStatement;
                    //parentBlock.Statements.Ins
                }
                // listStmts
            }
        }
        private void GetExistingModifiersFromNode()
        {
            if (_model == null)
                return;
            if (_model.GetType() == typeof(TypeDeclaration))
            {
                Modifiers modifiers = (_model as TypeDeclaration).Modifiers;
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.New) == ICSharpCode.NRefactory.CSharp.Modifiers.New)
                    ModifiersList.Add("new");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Partial) == ICSharpCode.NRefactory.CSharp.Modifiers.Partial)
                    ModifiersList.Add("partial");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Static) == ICSharpCode.NRefactory.CSharp.Modifiers.Static)
                    ModifiersList.Add("static");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Abstract) == ICSharpCode.NRefactory.CSharp.Modifiers.Abstract)
                    ModifiersList.Add("abstract");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Const) == ICSharpCode.NRefactory.CSharp.Modifiers.Const)
                    ModifiersList.Add("Const");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Async) == ICSharpCode.NRefactory.CSharp.Modifiers.Async)
                    ModifiersList.Add("async");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Override) == ICSharpCode.NRefactory.CSharp.Modifiers.Override)
                    ModifiersList.Add("override");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Virtual) == ICSharpCode.NRefactory.CSharp.Modifiers.Virtual)
                    ModifiersList.Add("virtual");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Extern) == ICSharpCode.NRefactory.CSharp.Modifiers.Extern)
                    ModifiersList.Add("extern");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Readonly) == ICSharpCode.NRefactory.CSharp.Modifiers.Readonly)
                    ModifiersList.Add("readonly");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Sealed) == ICSharpCode.NRefactory.CSharp.Modifiers.Sealed)
                    ModifiersList.Add("sealed");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Unsafe) == ICSharpCode.NRefactory.CSharp.Modifiers.Unsafe)
                    ModifiersList.Add("unsafe");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Volatile) == ICSharpCode.NRefactory.CSharp.Modifiers.Volatile)
                    ModifiersList.Add("volatile");
            }
            if (_model.GetType() == typeof(MethodDeclaration))
            {
                Modifiers modifiers = (_model as MethodDeclaration).Modifiers;
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.New) == ICSharpCode.NRefactory.CSharp.Modifiers.New)
                    ModifiersList.Add("new");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Partial) == ICSharpCode.NRefactory.CSharp.Modifiers.Partial)
                    ModifiersList.Add("partial");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Static) == ICSharpCode.NRefactory.CSharp.Modifiers.Static)
                    ModifiersList.Add("static");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Abstract) == ICSharpCode.NRefactory.CSharp.Modifiers.Abstract)
                    ModifiersList.Add("abstract");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Const) == ICSharpCode.NRefactory.CSharp.Modifiers.Const)
                    ModifiersList.Add("Const");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Async) == ICSharpCode.NRefactory.CSharp.Modifiers.Async)
                    ModifiersList.Add("async");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Override) == ICSharpCode.NRefactory.CSharp.Modifiers.Override)
                    ModifiersList.Add("override");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Virtual) == ICSharpCode.NRefactory.CSharp.Modifiers.Virtual)
                    ModifiersList.Add("virtual");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Extern) == ICSharpCode.NRefactory.CSharp.Modifiers.Extern)
                    ModifiersList.Add("extern");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Readonly) == ICSharpCode.NRefactory.CSharp.Modifiers.Readonly)
                    ModifiersList.Add("readonly");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Sealed) == ICSharpCode.NRefactory.CSharp.Modifiers.Sealed)
                    ModifiersList.Add("sealed");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Unsafe) == ICSharpCode.NRefactory.CSharp.Modifiers.Unsafe)
                    ModifiersList.Add("unsafe");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Volatile) == ICSharpCode.NRefactory.CSharp.Modifiers.Volatile)
                    ModifiersList.Add("volatile");
            }
            if (_model.GetType() == typeof(FieldDeclaration))
            {
                Modifiers modifiers = (_model as FieldDeclaration).Modifiers;
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.New) == ICSharpCode.NRefactory.CSharp.Modifiers.New)
                    ModifiersList.Add("new");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Partial) == ICSharpCode.NRefactory.CSharp.Modifiers.Partial)
                    ModifiersList.Add("partial");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Static) == ICSharpCode.NRefactory.CSharp.Modifiers.Static)
                    ModifiersList.Add("static");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Abstract) == ICSharpCode.NRefactory.CSharp.Modifiers.Abstract)
                    ModifiersList.Add("abstract");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Const) == ICSharpCode.NRefactory.CSharp.Modifiers.Const)
                    ModifiersList.Add("Const");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Async) == ICSharpCode.NRefactory.CSharp.Modifiers.Async)
                    ModifiersList.Add("async");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Override) == ICSharpCode.NRefactory.CSharp.Modifiers.Override)
                    ModifiersList.Add("override");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Virtual) == ICSharpCode.NRefactory.CSharp.Modifiers.Virtual)
                    ModifiersList.Add("virtual");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Extern) == ICSharpCode.NRefactory.CSharp.Modifiers.Extern)
                    ModifiersList.Add("extern");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Readonly) == ICSharpCode.NRefactory.CSharp.Modifiers.Readonly)
                    ModifiersList.Add("readonly");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Sealed) == ICSharpCode.NRefactory.CSharp.Modifiers.Sealed)
                    ModifiersList.Add("sealed");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Unsafe) == ICSharpCode.NRefactory.CSharp.Modifiers.Unsafe)
                    ModifiersList.Add("unsafe");
                if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Volatile) == ICSharpCode.NRefactory.CSharp.Modifiers.Volatile)
                    ModifiersList.Add("volatile");
            }
            ModifiersList.Distinct();
        }

        private void GetExistingGenericsFromNode()
        {
            if (_model == null)
                return;
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
            if (_model.GetType() == typeof(MethodDeclaration))
            {
                Tuple<string, EGenericVariance> ExistingGeneric;
                foreach (var tmp in (_model as MethodDeclaration).TypeParameters)
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
            if (_model == null)
                return;
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
                else if (_model.GetType() == typeof(NamespaceDeclaration))
                    return (ENodeActions.NAME | ENodeActions.COMMENT);
                else if (_model.GetType() == typeof(MethodDeclaration))
                    return (ENodeActions.ATTRIBUTE | ENodeActions.COMMENT | ENodeActions.ACCESS_MODIFIERS | ENodeActions.MODIFIERS | ENodeActions.NAME | ENodeActions.GENERICS | ENodeActions.TYPE);
                else if (_model.GetType() == typeof(FieldDeclaration))
                    return (ENodeActions.NAME | ENodeActions.ACCESS_MODIFIERS | ENodeActions.MODIFIERS | ENodeActions.COMMENT | ENodeActions.ATTRIBUTE | ENodeActions.TYPE);
                else if (_model.GetType() == typeof(PropertyDeclaration))
                    return (ENodeActions.ACCESS_MODIFIERS | ENodeActions.COMMENT);
                else if (_model.GetType() == typeof(UsingDeclaration))
                    return (ENodeActions.COMMENT);
                else if (_model.GetType() == typeof(ObjectCreateExpression))
                    return (ENodeActions.COMMENT | ENodeActions.EXEC_TYPE);
                else if (_model.GetType() == typeof(IdentifierExpression))
                    return (ENodeActions.TEXT | ENodeActions.COMMENT);
                else if (_model.GetType() == typeof(MemberReferenceExpression))
                    return (ENodeActions.TEXT | ENodeActions.COMMENT);
                else if (_model.GetType() == typeof(InvocationExpression))
                    return (ENodeActions.EXEC_PARAMETERS | ENodeActions.EXEC_GENERICS | ENodeActions.COMMENT);
                else if (_model.GetType() == typeof(PrimitiveExpression))
                    return (ENodeActions.TEXT | ENodeActions.COMMENT);
                else
                    return (ENodeActions.TEXT); // Any not supported node allows text modification
            }

            return (0);
        }

        public void SetASTNode(AstNode node)
        {
            _model = node;
        }
        // methods for the Nodes modification

        // this method changes the name into ast and update the view
        public void SetName(String name)
        {
            if (_model == null)
                return;
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
            if (_model == null)
                return;
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
            if (_model.GetType() == typeof(MethodDeclaration))
            {
                TypeParameterDeclaration NewGeneric = new TypeParameterDeclaration();
                Tuple<string, EGenericVariance> NewGenericInList = null;
                var typeDecl = _model as MethodDeclaration;
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
                return (new List<Tuple<string, EGenericVariance>>());
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
            if (_model == null)
                return;
            if (_model.GetType() == typeof(TypeDeclaration))
            {
                var TypeDecl = (_model as TypeDeclaration);

                TypeDecl.TypeParameters.Clear();
                TypeParameterDeclaration updatedGeneric;
                foreach (Tuple<string, EGenericVariance> generic in GenericList)
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
            if (_model.GetType() == typeof(MethodDeclaration))
            {
                var TypeDecl = (_model as MethodDeclaration);

                TypeDecl.TypeParameters.Clear();
                TypeParameterDeclaration updatedGeneric;
                foreach (Tuple<string, EGenericVariance> generic in GenericList)
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
            if (_model == null)
                return;
            if (_model.GetType() == typeof(TypeDeclaration))
            {
                InheritanceList.Add(name);
                SimpleType NewInheriance = new SimpleType();
                NewInheriance.Identifier = name;
                (_model as TypeDeclaration).BaseTypes.Add(NewInheriance);
            }
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
            if (_model == null)
                return;
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
            if (_model.GetType() == typeof(MethodDeclaration))
            {
                var typeDecl = (_model as MethodDeclaration);
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
            if (_model.GetType() == typeof(FieldDeclaration))
            {
                var typeDecl = (_model as FieldDeclaration);
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
            if (_model == null)
                return;
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
                    if (OtherModifiers == "new")
                        typeDecl.Modifiers |= Modifiers.New;
                    if (OtherModifiers == "partial")
                        typeDecl.Modifiers |= Modifiers.Partial;
                    if (OtherModifiers == "static")
                        typeDecl.Modifiers |= Modifiers.Static;
                    if (OtherModifiers == "const")
                        typeDecl.Modifiers |= Modifiers.Const;
                    if (OtherModifiers == "async")
                        typeDecl.Modifiers |= Modifiers.Async;
                    if (OtherModifiers == "extern")
                        typeDecl.Modifiers |= Modifiers.Extern;
                    if (OtherModifiers == "readonly")
                        typeDecl.Modifiers |= Modifiers.Readonly;
                    if (OtherModifiers == "sealed")
                        typeDecl.Modifiers |= Modifiers.Sealed;
                    if (OtherModifiers == "unsafe")
                        typeDecl.Modifiers |= Modifiers.Unsafe;
                    if (OtherModifiers == "volatile")
                        typeDecl.Modifiers |= Modifiers.Volatile;
                    (_view as IContainingModifiers).setModifiersList(typeDecl.Modifiers); // update more than a set-> semantic detail
                }
                if (_model.GetType() == typeof(MethodDeclaration))
                {
                    var typeDecl = (_model as MethodDeclaration);
                    if (OtherModifiers == "virtual")
                        typeDecl.Modifiers |= Modifiers.Virtual;
                    if (OtherModifiers == "abstract")
                        typeDecl.Modifiers |= Modifiers.Abstract;
                    if (OtherModifiers == "override")
                        typeDecl.Modifiers |= Modifiers.Override;
                    if (OtherModifiers == "new")
                        typeDecl.Modifiers |= Modifiers.New;
                    if (OtherModifiers == "partial")
                        typeDecl.Modifiers |= Modifiers.Partial;
                    if (OtherModifiers == "static")
                        typeDecl.Modifiers |= Modifiers.Static;
                    if (OtherModifiers == "const")
                        typeDecl.Modifiers |= Modifiers.Const;
                    if (OtherModifiers == "async")
                        typeDecl.Modifiers |= Modifiers.Async;
                    if (OtherModifiers == "extern")
                        typeDecl.Modifiers |= Modifiers.Extern;
                    if (OtherModifiers == "readonly")
                        typeDecl.Modifiers |= Modifiers.Readonly;
                    if (OtherModifiers == "sealed")
                        typeDecl.Modifiers |= Modifiers.Sealed;
                    if (OtherModifiers == "unsafe")
                        typeDecl.Modifiers |= Modifiers.Unsafe;
                    if (OtherModifiers == "volatile")
                        typeDecl.Modifiers |= Modifiers.Volatile;
                    (_view as IContainingModifiers).setModifiersList(typeDecl.Modifiers); // update more than a set-> semantic detail
                }
                if (_model.GetType() == typeof(FieldDeclaration))
                {
                    var typeDecl = (_model as FieldDeclaration);
                    if (OtherModifiers == "virtual")
                        typeDecl.Modifiers |= Modifiers.Virtual;
                    if (OtherModifiers == "abstract")
                        typeDecl.Modifiers |= Modifiers.Abstract;
                    if (OtherModifiers == "override")
                        typeDecl.Modifiers |= Modifiers.Override;
                    if (OtherModifiers == "new")
                        typeDecl.Modifiers |= Modifiers.New;
                    if (OtherModifiers == "partial")
                        typeDecl.Modifiers |= Modifiers.Partial;
                    if (OtherModifiers == "static")
                        typeDecl.Modifiers |= Modifiers.Static;
                    if (OtherModifiers == "const")
                        typeDecl.Modifiers |= Modifiers.Const;
                    if (OtherModifiers == "async")
                        typeDecl.Modifiers |= Modifiers.Async;
                    if (OtherModifiers == "extern")
                        typeDecl.Modifiers |= Modifiers.Extern;
                    if (OtherModifiers == "readonly")
                        typeDecl.Modifiers |= Modifiers.Readonly;
                    if (OtherModifiers == "sealed")
                        typeDecl.Modifiers |= Modifiers.Sealed;
                    if (OtherModifiers == "unsafe")
                        typeDecl.Modifiers |= Modifiers.Unsafe;
                    if (OtherModifiers == "volatile")
                        typeDecl.Modifiers |= Modifiers.Volatile;
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
                    if (OtherModifiers == "new")
                        typeDecl.Modifiers &= ~Modifiers.New;
                    if (OtherModifiers == "partial")
                        typeDecl.Modifiers &= ~Modifiers.Partial;
                    if (OtherModifiers == "static")
                        typeDecl.Modifiers &= ~Modifiers.Static;
                    if (OtherModifiers == "const")
                        typeDecl.Modifiers &= ~Modifiers.Const;
                    if (OtherModifiers == "async")
                        typeDecl.Modifiers &= ~Modifiers.Async;
                    if (OtherModifiers == "extern")
                        typeDecl.Modifiers &= ~Modifiers.Extern;
                    if (OtherModifiers == "readonly")
                        typeDecl.Modifiers &= ~Modifiers.Readonly;
                    if (OtherModifiers == "sealed")
                        typeDecl.Modifiers &= ~Modifiers.Sealed;
                    if (OtherModifiers == "unsafe")
                        typeDecl.Modifiers &= ~Modifiers.Unsafe;
                    if (OtherModifiers == "volatile")
                        typeDecl.Modifiers &= ~Modifiers.Volatile;
                    (_view as IContainingModifiers).setModifiersList(typeDecl.Modifiers);
                }
                if (_model.GetType() == typeof(MethodDeclaration))
                {
                    var typeDecl = (_model as MethodDeclaration);
                    if (OtherModifiers == "virtual")
                        typeDecl.Modifiers &= ~Modifiers.Virtual;
                    if (OtherModifiers == "abstract")
                        typeDecl.Modifiers &= ~Modifiers.Abstract;
                    if (OtherModifiers == "override")
                        typeDecl.Modifiers &= ~Modifiers.Override;
                    if (OtherModifiers == "new")
                        typeDecl.Modifiers &= ~Modifiers.New;
                    if (OtherModifiers == "partial")
                        typeDecl.Modifiers &= ~Modifiers.Partial;
                    if (OtherModifiers == "static")
                        typeDecl.Modifiers &= ~Modifiers.Static;
                    if (OtherModifiers == "const")
                        typeDecl.Modifiers &= ~Modifiers.Const;
                    if (OtherModifiers == "async")
                        typeDecl.Modifiers &= ~Modifiers.Async;
                    if (OtherModifiers == "extern")
                        typeDecl.Modifiers &= ~Modifiers.Extern;
                    if (OtherModifiers == "readonly")
                        typeDecl.Modifiers &= ~Modifiers.Readonly;
                    if (OtherModifiers == "sealed")
                        typeDecl.Modifiers &= ~Modifiers.Sealed;
                    if (OtherModifiers == "unsafe")
                        typeDecl.Modifiers &= ~Modifiers.Unsafe;
                    if (OtherModifiers == "volatile")
                        typeDecl.Modifiers &= ~Modifiers.Volatile;
                    (_view as IContainingModifiers).setModifiersList(typeDecl.Modifiers);
                }
                if (_model.GetType() == typeof(FieldDeclaration))
                {
                    var typeDecl = (_model as FieldDeclaration);
                    if (OtherModifiers == "virtual")
                        typeDecl.Modifiers &= ~Modifiers.Virtual;
                    if (OtherModifiers == "abstract")
                        typeDecl.Modifiers &= ~Modifiers.Abstract;
                    if (OtherModifiers == "override")
                        typeDecl.Modifiers &= ~Modifiers.Override;
                    if (OtherModifiers == "new")
                        typeDecl.Modifiers &= ~Modifiers.New;
                    if (OtherModifiers == "partial")
                        typeDecl.Modifiers &= ~Modifiers.Partial;
                    if (OtherModifiers == "static")
                        typeDecl.Modifiers &= ~Modifiers.Static;
                    if (OtherModifiers == "const")
                        typeDecl.Modifiers &= ~Modifiers.Const;
                    if (OtherModifiers == "async")
                        typeDecl.Modifiers &= ~Modifiers.Async;
                    if (OtherModifiers == "extern")
                        typeDecl.Modifiers &= ~Modifiers.Extern;
                    if (OtherModifiers == "readonly")
                        typeDecl.Modifiers &= ~Modifiers.Readonly;
                    if (OtherModifiers == "sealed")
                        typeDecl.Modifiers &= ~Modifiers.Sealed;
                    if (OtherModifiers == "unsafe")
                        typeDecl.Modifiers &= ~Modifiers.Unsafe;
                    if (OtherModifiers == "volatile")
                        typeDecl.Modifiers &= ~Modifiers.Volatile;
                    (_view as IContainingModifiers).setModifiersList(typeDecl.Modifiers);
                }
            }
        }

        public void AddExecParam()
        {
            System.Diagnostics.Debug.Assert(_view is code_in.Views.NodalView.NodesElems.Nodes.Expressions.FuncCallExprNode);
            System.Diagnostics.Debug.Assert(this._model is InvocationExpression);

            var funcExprView = (_view as code_in.Views.NodalView.NodesElems.Nodes.Expressions.FuncCallExprNode);
            var invocExpr = this._model as InvocationExpression;
            var inAnchor = funcExprView.CreateAndAddInput<code_in.Views.NodalView.NodesElems.Anchors.DataFlowAnchor>();
            var idExpr = new IdentifierExpression(""); // This node is only used to fill the gap, the type of this node could be anything it does not matter
            invocExpr.Arguments.Add(idExpr); 
            inAnchor.SetASTNodeReference((e) => { idExpr.ReplaceWith(e); });
        }

        public void RemoveExecParam(int index)
        {
            System.Diagnostics.Debug.Assert(_view is code_in.Views.NodalView.NodesElems.Nodes.Expressions.FuncCallExprNode);
            System.Diagnostics.Debug.Assert(this._model is InvocationExpression);

            var invocExpr = this._model as InvocationExpression;
            var funcExprView = (_view as code_in.Views.NodalView.NodesElems.Nodes.Expressions.FuncCallExprNode);

            System.Diagnostics.Debug.Assert(index < invocExpr.Arguments.Count);
            var dataFlowAnchor = funcExprView._inputs.Children[index + 1] as DataFlowAnchor;
            invocExpr.Arguments.Remove(invocExpr.Arguments.ElementAt(index)); // TODO assert, it may crash
            _nodalPresenter.RemoveLink(dataFlowAnchor);
            funcExprView._inputs.Children.RemoveAt(index + 1);

        }

        // It keeps existing links and updates offsets
        public void SetExecParams(int paramsNumber)
        {
            var funcExprView = (_view as code_in.Views.NodalView.NodesElems.Nodes.Expressions.FuncCallExprNode);
            var invocExpr = this._model as InvocationExpression;
            int paramsToAdd = invocExpr.Arguments.Count;
            for (int i = 0; i < paramsNumber; ++i)
            {
                funcExprView.CreateAndAddInput<code_in.Views.NodalView.NodesElems.Anchors.DataFlowAnchor>();
            }
            /*            while (paramsNumber >= 0)
                        {
            //                _view.
                        }*/
        }

        public void AddExecGeneric()
        {

        }


        public void AddAttribute(string attribute)
        {
            AttributesList.Add(attribute);
            CSharpParser parser = new CSharpParser();

            ICSharpCode.NRefactory.CSharp.Expression newExpr = parser.ParseExpression(attribute);
            ICSharpCode.NRefactory.CSharp.Attribute newAttribute = new ICSharpCode.NRefactory.CSharp.Attribute();
            newAttribute.Type = new SimpleType(newExpr.Children.ElementAt(0).ToString());
            newExpr.FirstChild.Remove();
            newExpr.FirstChild.Remove();
            /*            newExpr.Children.ElementAt(0).Remove(); // TODO Try to remove brackets from expression but be carefull, i tried to remove more than one time, but didn't worked
                                    newExpr.Children.ElementAt(newExpr.Children.Count() - 1).Remove();*/
            newAttribute.Arguments.Add(newExpr);
            ICSharpCode.NRefactory.CSharp.AttributeSection newSection = new AttributeSection();
            newSection.Attributes.Add(newAttribute);

            if (_model.GetType() == typeof(TypeDeclaration))
            {
                var toto = (_model as TypeDeclaration);
                toto.Attributes.Add(newSection);
            }
            /*            foreach(var tmp in newExpr.Children)
                        {
                            newAttribute.Arguments.Add(tmp);
                        }*/
            //TODO add in ast + add in visual node
        }


        public void RemoveAttribute(int index)
        {
            if (index < AttributesList.Count)
                AttributesList.RemoveAt(index);
            //TODO remove in ast and in visual node
        }
        public List<string> getAttributeList()
        {
            return (AttributesList);
        }
        // methods for the contextMenu
        static void AddNode(object[] objects)
        {
            ContextMenu cm = new ContextMenu();
            UIElement view = (objects[0] as NodePresenter)._view as UIElement;
            cm.Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse;

            //var listOfBs = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
            //                from assemblyType in domainAssembly.GetTypes()
            //                where typeof(BaseNode).IsAssignableFrom(assemblyType)
            //                select assemblyType).ToArray();

            var listOfBs = (objects[0] as NodePresenter).GetAvailableNodes();

            foreach (var entry in listOfBs)
            {
                MenuItem mi = new MenuItem();
                mi.Header = entry.Name;
                mi.Click += mi_Click;
                mi.DataContext = entry;
                cm.Items.Add(mi);
            }
            cm.IsOpen = true;
            _viewStatic = ((objects[0] as NodePresenter)._view) as INodeElem;
            _presStatic = ((objects[0] as NodePresenter)._nodalPresenter) as INodalPresenter;
        }

        public void LoadExecParamsCount()
        {
            if (_model != null && _model.GetType() == typeof(InvocationExpression))
            {
                var tmp = (_model as InvocationExpression);
                ExecParamsNb = tmp.Arguments.Count;
            }
        }

        public int getExecParamsNb()
        {
            LoadExecParamsCount();
            return (ExecParamsNb);
        }

        static void mi_Click(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).DataContext != null)
            {
                //                (((MenuItem)sender).DataContext as NodalPresenterLocal)._view.CreateAndAddNode<_nodeCreationType>();
                MethodInfo mi = _viewStatic.GetType().GetMethod("CreateAndAddNode");
                MethodInfo gmi = mi.MakeGenericMethod(((MenuItem)sender).DataContext as Type);
                var tmp = new NodePresenter(_presStatic, null);
                //                var tmp = _presStatic;
                var toto = new object[1];
                toto[0] = tmp;
                BaseNode node = gmi.Invoke(_viewStatic, toto) as BaseNode;
                var pos = _viewStatic.GetPosition();
                node.SetPosition((int)pos.X, (int)pos.Y);
            }
            //_viewStatic = null;
        }
        static INodeElem _viewStatic = null;
        static INodalPresenter _presStatic = null;
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
            NodePresenter self = objects[0] as NodePresenter;

            self._view.Remove();
            if (self._model == null)
                return;
        }
        static void SaveNode(object[] objects)
        {
            MessageBox.Show(objects[0].GetType().ToString());
        }

        //This part bind methods to the options of the contextMenu
        public Tuple<EContextMenuOptions, Action<object[]>>[] GetMenuOptions()
        {
            List<Tuple<EContextMenuOptions, Action<object[]>>> optionsList = new List<Tuple<EContextMenuOptions, Action<object[]>>>();
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
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ADD, AddNode));
            }
            else if (_model.GetType() == typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration)) // for namespace
            {
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EDIT, EditNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.REMOVE, RemoveNode));
            }
            else if (_model.GetType() == typeof(ICSharpCode.NRefactory.CSharp.InvocationExpression)) // for namespace
            {
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EDIT, EditNode));
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.REMOVE, RemoveNode));
            }
            else
            {
                optionsList.Add(new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.EDIT, EditNode));
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


        public List<Type> GetAvailableNodes()
        {
            List<Type> tmp = new List<Type>();
            if (_model == null)
                return tmp;
            if (_model.GetType() == typeof(NamespaceDeclaration))
            {
                tmp.Add(typeof(UsingDeclNode));
                tmp.Add(typeof(ClassDeclNode));
            }
            if (_model.GetType() == typeof(TypeDeclaration))
            {
                tmp.Add(typeof(ClassDeclNode));
                tmp.Add(typeof(FuncDeclItem));
                tmp.Add(typeof(ClassItem));
            }
            return (tmp);
        }

       

        public void UpdateType(string type)
        {
            if (_model.GetType() == typeof(FieldDeclaration))
            {
                var ast = _model as FieldDeclaration;

                ast.ReturnType = new PrimitiveType(type);
                (_view as IContainingType).SetTypeFromString(type);
            }
            if (_model.GetType() == typeof(MethodDeclaration))
            {
                var ast = _model as MethodDeclaration;

                ast.ReturnType = new PrimitiveType(type);
                (_view as IContainingType).SetTypeFromString(type);
            }
        }
    }
}
