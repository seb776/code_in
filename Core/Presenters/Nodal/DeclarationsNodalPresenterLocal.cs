using code_in.Models.NodalModel;
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
        private DeclarationsNodalModel _model = null;
        public bool OpenFile(String path)
        {
            try
            {
                _model = this._parseFile(path);
                this._generateVisualASTDeclaration(_model.AST, this._view);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        private DeclarationsNodalModel _parseFile(String path)
        {
            var parser = new ICSharpCode.NRefactory.CSharp.CSharpParser();
            StreamReader fileStream = new StreamReader(path);
            var ast = parser.Parse(fileStream);
            var model = new DeclarationsNodalModel(ast);
            return model;
        }
    }
}
