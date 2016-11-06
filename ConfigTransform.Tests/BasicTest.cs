using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConfigTransform.Tests
{
    public class BasicTest
    {
        [Fact]
        public void Test()
        {
            Program.Main("-src=../../App.config", "-trans=../../App.Stage.config", "-dest=App.config");
            Assert.True(true);
        }
    }
}
