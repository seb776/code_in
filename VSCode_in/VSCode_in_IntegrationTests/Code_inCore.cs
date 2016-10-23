using code_in;
using Code_in.VSCode_in;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSCode_in_IntegrationTests
{
    [TestClass()]
    public class Code_inCore
    {
        [TestMethod()]
        public void VSCode_inIsIEnvironmentWrapper()
        {
            VSCode_inPackage package = new VSCode_inPackage();
            Assert.IsNotNull(package as IEnvironmentWrapper, "The object does not implement IVsPackage");
        }
    }
}
