using code_in.Models.NodalModel;
using code_in.Views.NodalView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace code_in.Presenters.Nodal
{
    public class DeclarationsNodalPresenterLocal : ANodalPresenterLocal
    {
        public DeclarationsNodalModel _model = null;
        public override String DocumentName
        {
            get
            {
                return _model.FileName;
            }
        }
        public override void Save()
        {
            this._model.Save();
        }
        public void Save(string filePath)
        {
            this._model.Save(filePath);
        }
        public DeclarationsNodalPresenterLocal() :
            base()
        {

        }
        public bool OpenFile(String path)
        {
            try
            {
                _model = new DeclarationsNodalModel(path);
                _model.Presenter = this;
                this._generateVisualASTDeclaration(_model.AST, this.View);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        public override bool IsSaved
        {
            get { return _model.IsSaved; }
        }
    }
}
