﻿using code_in.Presenters.Nodal;
using code_in.Views.NodalView;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Models.NodalModel
{
    public class DeclarationsNodalModel : INodalModel
    {
        public List<ExecutionNodalModel> AssociatedChildren = new List<ExecutionNodalModel>();
        public void CloseChildrenViews()
        {
            foreach (var v in AssociatedChildren)
                v.Presenter.View.EnvironmentWindowWrapper.CloseCode_inWindow();
            AssociatedChildren.Clear();
        }
        private bool _isSaved;

        #region INodalModel
        public bool IsSaved
        {
            get
            {
                return _isSaved;
            }
            set
            {
                _isSaved = value;
                if (this.Presenter != null && this.Presenter.View != null && this.Presenter.View.EnvironmentWindowWrapper != null)
                    this.Presenter.View.EnvironmentWindowWrapper.UpdateTitleState(); // TODO beuark
            }
        }
        public void Save()
        {
            (this.Presenter.View as DeclarationsNodalView).IsSaving = true;
            this.Save(this.FilePath);
            (this.Presenter.View as DeclarationsNodalView).IsSaving = false;
        }

        public void Save(string filePath)
        {
            String finalFilePath = filePath;// Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(filePath) + ".cdn.cs"; // Temporary to avoid bugs while developing
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(finalFilePath))
            {
                sw.AutoFlush = true;
                sw.Write(AST.ToString());
                this.IsSaved = true;
                sw.Close();
            }
        }
        #endregion INodalModel

        #region This
        public DeclarationsNodalPresenterLocal Presenter;
        public SyntaxTree AST
        {
            get;
            private set;
        }
        public string FilePath
        {
            get;
            private set;
        }
        public string FileName
        {
            get
            {
                return Path.GetFileName(FilePath);
            }
        }

        public DeclarationsNodalModel(string filePath)
        {
            IsSaved = true;
            FilePath = filePath;
            try
            {
                AST = _parseFile(FilePath);
            }
            catch (Exception e)
            {
                // TODO
                //Presenter.Close();
                Presenter.View.EnvironmentWindowWrapper.CloseCode_inWindow();
                MessageBox.Show(e.ToString());
            }
        }

        private SyntaxTree _parseFile(string filePath)
        {
            var parser = new ICSharpCode.NRefactory.CSharp.CSharpParser();
            StreamReader fileStream = new StreamReader(filePath);
            SyntaxTree ast = parser.Parse(fileStream);
            fileStream.Close();
            return ast;
        }
        #endregion This
    }
}
