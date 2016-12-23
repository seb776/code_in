using code_in.Presenters.Nodal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView
{
    public class DeclarationsNodalView : ANodalView
    {
        public DeclarationsNodalPresenterLocal NodalPresenterDecl
        {
            get
            {
                return _nodalPresenter as DeclarationsNodalPresenterLocal;
            }
            private set;
        }
        public DeclarationsNodalView(ResourceDictionary themeResDict) :
            base(themeResDict)
        {

        }

        public void OpenFile(String path)
        {
            // TODO Show Animation loadingFile
            this.NodalPresenterDecl.OpenFile(path);
            AlignDeclarations();
        }
    }
}
