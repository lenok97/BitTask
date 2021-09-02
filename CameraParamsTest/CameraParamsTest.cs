using BitTask;
using NUnit.Framework;
using System;

namespace BitTaskTest
{
    [TestFixture]
    public class CameraParamsTest
    {
        [TestCase(700, 150, 10, 0.82)]
        [TestCase(700, 350, 190, 15.19)]
        public void GetParams(float distance, float height, float expectedB, double expectedAngle)
        {
            var actual = new CameraParams(distance, height);

            Assert.AreEqual(expectedB, Math.Round(actual.B, 2));
            Assert.AreEqual(expectedAngle, Math.Round(actual.Angle, 2));
        }

        [TestCase(0, 400)]
        [TestCase(600, 0)]
        [TestCase(-600, 400)]
        [TestCase(600, -400)]
        public void GetParams_Fails_WhenDistanceIsWrong(float distance, float height)
        {
            Assert.Throws(typeof(ArgumentException), () =>
            { var actual = new CameraParams(distance, height); });
        }
    }
}