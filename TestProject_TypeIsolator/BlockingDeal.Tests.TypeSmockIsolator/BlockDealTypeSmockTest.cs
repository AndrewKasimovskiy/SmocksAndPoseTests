using NUnit.Framework;
using TypeMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeMock.ArrangeActAssert;

namespace BlockingDeal.Tests.TypeSmockIsolator
{
    [TestFixture]
    public class BlockDealTypeSmockTest
    {
        [Test]
        void test()
        {
            Isolate.Fake.StaticMethods<>()
        }
    }
}
