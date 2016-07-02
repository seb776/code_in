using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// z0rg: This file is a work in progress
// Just exposing few ideas to see how to use them

namespace code_in.Presenters.Nodal.Nodes
{
    /// <summary>
    /// DECL_* for Declaration side nodes modifications
    /// EXEC_* for Execution side nodes modifications (like nodes in the method view)
    /// </summary>
    public enum ENodeActions
    {
        ACCESS_MODIFIERS = 1,
        MODIFIERS = 2,
        NAME = 4,
        GENERICS = 8, // Type parameters, covariance-contravariance, constraints
        ATTRIBUTE = 16,
        INHERITANCE = 32,
        COMMENT = 64,
        EXEC_GENERICS = 128,
        EXEC_TYPE = 256,
        TYPE = 512,
        TEXT = 1024,
        EXEC_PARAMETERS = 2048
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

        /// <summary>
        /// This function is used to associate the visual node to the node presenter.
        /// </summary>
        /// <param name="visualNode"></param>
        void SetView(INodeElem visualNode);
    }
}
