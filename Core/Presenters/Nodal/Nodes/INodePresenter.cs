using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// z0rg: This file is a work in progress
// Just exposing few ideas to see how to use them

namespace code_in.Presenters.Nodal.Nodes
{
    public enum ENodeActions
    {
        MODIFIERS = 1,
        NAME = 2,
        GENERICS = 4,
        ATTRIBUTE = 8,
        INHERITANCE = 16,
        CONSTRAINT = 32 // where T : ...
    }
    public interface INodePresenter
    {
        ENodeActions GetActions(); // Gets the possible actions for this node
        void AddGeneric(String name);
        void RemoveGeneric(int index);
        void AddInheritance(String name);
        //bool AddConstraint() // TODO @z0rg
        void SetName(String name);
    }
}
