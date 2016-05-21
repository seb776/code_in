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
        ACCESS_MODIFIERS = 1,
        MODIFIERS = 2,
        NAME = 4,
        GENERICS = 8, // Type parameters, covariance-contravariance, constraints
        ATTRIBUTE = 16,
        INHERITANCE = 32,
        COMMENT = 64,
    }
    public interface INodePresenter : IContextMenu
    {
        ENodeActions GetActions(); // Gets the possible actions for this node
        String[] GetAvailableModifiers(); // TODO @z0rg: String ? :S
        String[] GetAvailableAccessModifiers();
        void AddGeneric(bool updateView, String name);
        void RemoveGeneric(bool updateView, int index);
        void AddInheritance(bool updateView, String name);
        //bool AddConstraint() // TODO @z0rg
        void SetName(bool updateView, String name);
    }
}
