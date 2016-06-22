using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace test
{
    public class SomethingShould
    {
        [Test]
        public void DoSomething()
        {
            Assert.IsFalse(false);
            //Assert.That(true, Is.False);
        }
    }
}
