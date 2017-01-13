using code_in.Models;
using code_in.Models.NodalModel;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElems.Items;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Presenters.Nodal
{
    public class ExecutionNodalPresenterLocal : ANodalPresenterLocal
    {
        private ExecutionNodalModel _model;
        public ExecutionNodalView ExecNodalView
        {
            get
            {
                return View as ExecutionNodalView;
            }
        }
        public ExecutionNodalPresenterLocal(DeclarationsNodalPresenterLocal assocFile, INodePresenter nodePresenter) :
            base()
        {
            _model = new ExecutionNodalModel(assocFile._model, nodePresenter.GetASTNode());
        }
        public override String DocumentName
        {
            get
            {
                return "EXEC()";
            }
        }

        public void EditFunction(FuncDeclItem node)
        {
            this._generateVisualASTFunctionBody(node.MethodNode);
        }
        public void EditAccessor(Accessor node)
        {
            _generateVisualASTPropertyBody(node);
        }
        public void EditConstructor(ConstructorItem node)
        {
            this._generateVisualASTConstructorBody(node.ConstructorNode);
        }
        public void EditDestructor(DestructorItem node)
        {
            this._generateVisualASTDestructorBody(node.DestructorNode);
        }

        protected void _generateVisualASTFunctionBody(ICSharpCode.NRefactory.CSharp.MethodDeclaration method)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            this._generateVisualASTStatements(this.ExecNodalView.RootTileContainer, method.Body);
        }

        protected void _generateVisualASTConstructorBody(ICSharpCode.NRefactory.CSharp.ConstructorDeclaration constructor)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this.ExecNodalView.RootTileContainer, constructor.Body);
        }
        private void _generateVisualASTDestructorBody(ICSharpCode.NRefactory.CSharp.DestructorDeclaration destructor)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this.ExecNodalView.RootTileContainer, destructor.Body);
        }
        protected void _generateVisualASTPropertyBody(ICSharpCode.NRefactory.CSharp.Accessor access)
        {
            (this.ExecNodalView.RootTileContainer as System.Windows.Controls.UserControl).Margin = new System.Windows.Thickness(100, 100, 0, 0);
            _generateVisualASTStatements(this.ExecNodalView.RootTileContainer, access.Body);

        }

        public override bool IsSaved
        {
            get { return _model.IsSaved; }
        }
    }
}
