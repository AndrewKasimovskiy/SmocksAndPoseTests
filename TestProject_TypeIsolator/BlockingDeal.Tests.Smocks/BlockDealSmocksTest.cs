using FakeItEasy;
using NUnit.Framework;
using Smocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockingDeal.Tests.Smocks
{
    [TestFixture]
    public class BlockDealSmocksTest
    {
        [Test]
        public void BlockDeal_ChangeDateTimeNow()
        {
            var actual = Smock.Run<DateTime>((context) =>
            {
                context.Setup(() => DateTime.Now).Returns(new DateTime(2004, 4, 4));

                var ef = A.Fake<IEfContext>();
                var deal = new TableDeals() { Id = 1, Name = "Name", OtherBlocks = "Some values", DateTimeNow = new DateTime(2020, 8, 4) };
                const int id = 1;

                A.CallTo(() => ef.GetDeal(id)).Returns(deal);
                var block = new BlockDeal(ef);
                //DateTime expected = new DateTime(2004, 4, 4);

                var actl = block.BlockingOneDeal(id);

                return actl;
            });
            DateTime expected = new DateTime(2004, 4, 4);
            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
