using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class ClassDeclNode : Base.OrderedContentNode
    {
        public enum EType
        {
            STRUCT = 0,
            CLASS = 1
        }
        private EType _type;

        public ClassDeclNode(System.Windows.ResourceDictionary resDict) : base(resDict)
        {
            this.SetColorResource("ClassDeclColor");
            this.SetNodeType("ClassDecl");
            this.SetName("Class1");
            _type = EType.CLASS;
        }
        public ClassDeclNode() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        public ClassDeclNode(EType type) :
            this()
        {
            _type = type;
        }
    }
}
