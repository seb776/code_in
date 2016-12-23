﻿using code_in.Presenters.Nodal;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Models.NodalModel
{
    public class DeclarationsNodalModel
    {
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
            FilePath = filePath;
            try
            {
                AST = _parseFile(FilePath);
            }
            catch (Exception e)
            {
                // TODO
                Presenter.Close();
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
    }
}
