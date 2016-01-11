using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes
{
    public class ClassDeclNode : BaseNode
    {
        public enum EType
        {
            STRUCT = 0,
            CLASS = 1
        }
        private EType _type;

        public ClassDeclNode() :
            base()
        {
            this.DisableFeatures(EFeatures.ISFLOWNODE, EFeatures.EXPENDABLES);
            this.SetColorResource("ClassDeclColor");
            this.SetNodeType("ClassDecl");
            this.SetNodeName("Class1");
            _type = EType.CLASS;
        }

        public ClassDeclNode(EType type) :
            this()
        {
            _type = type;
        }
    }
}
