using BasicMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicMathTests
{
    [TestClass]
    public class BasicMathsTests
    {
        private readonly BasicMaths bm = new BasicMaths();

        // ---------- Exercise 1: inline data (EP + BVA) ----------
        [DataTestMethod]
        [DataRow(1.0, 1.0, 2.0)]                                              // EP: positive
        [DataRow(-1.0, -1.0, -2.0)]                                           // EP: negative
        [DataRow(0.0, 0.0, 0.0)]                                              // EP: zero
        [DataRow((double)int.MaxValue, 1.0, (double)int.MaxValue + 1)]        // BVA: upper
        [DataRow((double)int.MinValue, -1.0, (double)int.MinValue - 1)]       // BVA: lower
        public void Add_Test(double a, double b, double expected) =>
            Assert.AreEqual(expected, bm.Add(a, b));

        [DataTestMethod]
        [DataRow(5.0, 3.0, 2.0)]
        [DataRow(-5.0, -3.0, -2.0)]
        [DataRow(0.0, 0.0, 0.0)]
        [DataRow((double)int.MinValue, 1.0, (double)int.MinValue - 1)]
        public void Subtract_Test(double a, double b, double expected) =>
            Assert.AreEqual(expected, bm.Subtract(a, b));

        [DataTestMethod]
        [DataRow(2.0, 3.0, 6.0)]
        [DataRow(-2.0, -3.0, 6.0)]
        [DataRow(0.0, 5.0, 0.0)]
        [DataRow((double)int.MaxValue, 1.0, (double)int.MaxValue)]
        public void Multiply_Test(double a, double b, double expected) =>
            Assert.AreEqual(expected, bm.Multiply(a, b));

        [DataTestMethod]
        [DataRow(6.0, 3.0, 2.0)]
        [DataRow(-6.0, -3.0, 2.0)]
        [DataRow(0.0, 1.0, 0.0)]
        [DataRow((double)int.MaxValue, 1.0, (double)int.MaxValue)]
        public void Divide_Test(double a, double b, double expected) =>
            Assert.AreEqual(expected, bm.Divide(a, b));

        [TestMethod]
        public void Divide_ByZero_ThrowsException() =>
            Assert.ThrowsException<DivideByZeroException>(() => bm.Divide(1, 0));
    }
}
