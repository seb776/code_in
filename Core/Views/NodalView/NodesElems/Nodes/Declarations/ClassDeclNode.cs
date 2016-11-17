using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    /// <summary>
    /// This defines the visual nodes for "classes", class here as a large meaning, it stand for enum, struct, class, interface
    /// </summary>
    public class ClassDeclNode : AOrderedContentNode, IContainingAccessModifiers, IContainingModifiers, IContainingInheritance, IContainingGenerics, IContainingAttribute, IContainingConstraints
    {
        public override void InstantiateASTNode()
        {
            var typeDecl = new ICSharpCode.NRefactory.CSharp.TypeDeclaration();
            typeDecl.ClassType = ClassType.Class;
            this.GetNodePresenter().SetASTNode(typeDecl);
        }
        public enum EType
        {
            STRUCT = 0,
            CLASS = 1,
            INTERFACE = 2,
            ENUM = 3
        }
        public Assets.ClassNodeModifiers Modifiers = null;
        public Assets.GenericItem Generics = null;
        public Assets.GenericConstraints Constraints = null;
        public Assets.AttributesAsset Attributes = null;
        private EType _type;
        public ClassDeclNode(System.Windows.ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetType("class");
            this.SetName("TMP.Class");
            //this.SetThemeResources("ClassDeclNode");
            Generics = new GenericItem(themeResDict);
            Modifiers = new ClassNodeModifiers(themeResDict);
            Constraints = new GenericConstraints(themeResDict);
            Modifiers.SetValue(Grid.ColumnProperty, 0);
            Attributes = new AttributesAsset(themeResDict);
            this.ModifiersLayout.Children.Add(Modifiers);
            this.GenericsField.Children.Add(Generics);
            this.ConstraintLayout.Children.Add(Constraints);
            this._orderedLayout.Margin = new System.Windows.Thickness(0, 0, 0, 10);
            this.AttributesLayout.Children.Add(Attributes);
        }
        public ClassDeclNode() :
            this(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }
        #region This
        public void SetClassType(EType type)
        {
            string[] typeString = {
                                      "struct",
                                      "class",
                                      "interface",
                                      "enum"
                                  };
            this.SetType(typeString[(int)type]);
        }

        public void AddInheritance(string type)
        {
            Label lbl = new Label();
            lbl.Content = type;
            // lbl.SetResourceReference(...) // TODO
            this.InheritanceLayout.Children.Add(lbl);
        }

        public void RemoveInheritance(int idx)
        {
            this.InheritanceLayout.Children.RemoveAt(idx);
        }
        #endregion This

        #region IContainingAttribute
        public void addAttribute(string type, string arg)
        {
            Attributes.addAttribute(type, arg);
            AttributesLayout.Children.Clear();
            AttributesLayout.Children.Add(Attributes);
        }
        public void delAttribute(int index)
        {
            Attributes.deleteAttribute(index);
            AttributesLayout.Children.Clear();
            AttributesLayout.Children.Add(Attributes);
        }

        public void setExistingAttributes(List<KeyValuePair<string, string>> list)
        {
            if (list.Count > 0 && list != null)
            {
                foreach (KeyValuePair<string, string> pair in list)
                    addAttribute(pair.Key, pair.Value);
            }
        }
        #endregion IContainingAttribute

        #region IContainingAccessModifiers
        public void setAccessModifiers(Modifiers modifiers)
        {
            if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Public) == ICSharpCode.NRefactory.CSharp.Modifiers.Public)
                Modifiers.SetAccessModifiers(EAccessModifier.PUBLIC);
            else if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Private) != 0)
                Modifiers.SetAccessModifiers(EAccessModifier.PRIVATE);
            else if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Protected) != 0)
                Modifiers.SetAccessModifiers(EAccessModifier.PROTECTED);
            else if ((modifiers & ICSharpCode.NRefactory.CSharp.Modifiers.Internal) != 0)
                Modifiers.SetAccessModifiers(EAccessModifier.INTERNAL);
        }
        #endregion

        #region IContainingModifiers
        public void setModifiersList(Modifiers modifiers)
        {
            List<string> ModifiersList = new List<string>();
            ModifiersList.Clear();
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
            ModifiersList.Distinct();
            Modifiers.SetModifiers(ModifiersList.ToArray());
        }
        #endregion

        #region IContainingInheritance
        public void ManageInheritance(List<string> InheritanceList)
        {
            InheritanceLayout.Children.Clear();
            foreach (string inherit in InheritanceList)
                AddInheritance(inherit);
        }
        #endregion

        #region IContainingGenerics
        public void setGenerics(List<Tuple<string, EGenericVariance>> tmp)
        {
            Generics.SetGenerics(tmp);
        }
        #endregion

        public void setConstraint(string type, AstNodeCollection<AstType> types)
        {
            Constraints.setConstraint(type, types);
        }
    }
}
