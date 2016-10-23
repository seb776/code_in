using code_in.Views.NodalView.NodesElems.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSCode_in_UnitTests.Core.UI
{
    [TestClass()]
    public class Nodes
    {
        [TestMethod()]
        public void CreateClassDeclNode()
        {
            try
            {
                ClassDeclNode node = new ClassDeclNode();
            }
            catch (Exception e)
            {
                // All is good it should throw an exception
                return;
            }
            throw new Exception();
        }
    }
}
