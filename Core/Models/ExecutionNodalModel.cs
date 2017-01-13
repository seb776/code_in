using code_in.Models.NodalModel;
using code_in.Presenters.Nodal;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Models
{
    public class ExecutionNodalModel : INodalModel
    {
        public ExecutionNodalPresenterLocal Presenter;
        public DeclarationsNodalModel AssociatedFile
        {
            get;
            private set;
        }
        public AstNode Root
        {
            get;
            private set;
        }

        public bool IsSaved
        {
            get
            {
                return AssociatedFile.IsSaved;
            }
            set
            {
                AssociatedFile.IsSaved = value;
            }
        }

        public ExecutionNodalModel(DeclarationsNodalModel assocFile, AstNode root)
        {
            Debug.Assert(assocFile != null);
            Debug.Assert(root != null);
            AssociatedFile = assocFile;
            Root = root;
        }


        public void Save()
        {
            try
            {
                this.AssociatedFile.Save();
                this.Presenter.View.EnvironmentWindowWrapper.UpdateTitleState();
            }
            catch (Exception e) { }
        }
    }
}
