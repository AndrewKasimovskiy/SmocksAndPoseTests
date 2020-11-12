using FakeItEasy;
using NUnit.Framework;
using Pose;
using System;
using System.Globalization;
using System.Threading;

namespace BlockingDeal.Tests.Poses
{
    [TestFixture]
    public class BlockDealPoseTest
    {
        [Test]
        public void Block_DateChange()
        {
            var ef = A.Fake<IEfContext>();
            var deal = new TableDeals() { Id = 1, Name = "Name", OtherBlocks = "Some values", DateTimeNow = new DateTime(2020, 8, 4) };
            const int id = 1;

            A.CallTo(() => ef.GetDeal(id)).Returns(deal);
            var block = new BlockDeal(ef);
            bool time_getter = false;
            DateTime expected = new DateTime(2004, 4, 4);
            Shim getterDateShim = Shim.Replace(() => DateTime.Now).With(() =>
                {
                    return new DateTime(2004, 4, 4);
                });


            PoseContext.Isolate(() =>
            {
                var actual = DateTime.Now;
                if (actual.Equals(expected)) time_getter = true;
            }, getterDateShim);

            Assert.IsTrue(time_getter);
        }

        [Test]
        public void TestPose()
        {
            var getterExecuted = false;
            var getterShim = Shim.Replace(() => Pose.Is.A<Thread>().CurrentCulture).With(
                (Thread t) =>
                {
                    getterExecuted = true;
                    return t.CurrentCulture;
                });

            var setterExecuted = false;
            var setterShim = Shim.Replace(() => Pose.Is.A<Thread>().CurrentCulture, true).With(
                (Thread t, CultureInfo value) =>
                {
                    setterExecuted = true;
                    t.CurrentCulture = value;
                });

            var currentCultureProperty = typeof(Thread).GetProperty(nameof(Thread.CurrentCulture), typeof(CultureInfo));

            PoseContext.Isolate(() =>
            {
                var oldCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = oldCulture;
            }, getterShim, setterShim);

            Assert.IsTrue(getterExecuted, "Getter not executed");
            Assert.IsTrue(setterExecuted, "Setter not executed");
        }
    }
}
