using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
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
    public class ClassDeclNode : AOrderedContentNode
    {
        public enum EType
        {
            STRUCT = 0,
            CLASS = 1,
            INTERFACE = 2
        }
        public Assets.ClassNodeModifiers Modifiers = null;
        private EType _type;
        public ClassDeclNode(System.Windows.ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("class");
            this.SetName("TMP.Class");
            //this.SetThemeResources("ClassDeclNode");
            Modifiers = new Assets.ClassNodeModifiers(themeResDict);
            Modifiers.SetValue(Grid.ColumnProperty, 0);
            this.ModifiersLayout.Children.Add(Modifiers);
            this._orderedLayout.Margin = new System.Windows.Thickness(0, 0, 0, 10);
        }
        public ClassDeclNode() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }
        #region This
        public void SetClassType(EType type)
        {
            string[] typeString = {
                                      "struct",
                                      "class",
                                      "interface"
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
    }
}
