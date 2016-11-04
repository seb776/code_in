﻿using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Presenters.Nodal.Nodes
{
    interface IContainingConstraints
    {
        void setConstraint(String type, AstNodeCollection<AstType> types);
    }
}
