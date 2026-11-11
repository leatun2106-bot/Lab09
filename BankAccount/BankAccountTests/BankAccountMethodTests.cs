using BankAccountApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankAccountTests
{
    [TestClass]
    public class BankAccountMethodTests
    {
        // ---- Debit: EP (valid) + BVA (0, == balance, balance+1) ----
        [DataTestMethod]
        [DataRow(1000, 200, 800)]   // EP valid
        [DataRow(100, 0, 100)]      // BVA lower: 0
        [DataRow(100, 100, 0)]      // BVA upper: == balance
        public void Debit_Valid_UpdatesBalance(int init, int amt, int expected)
        {
            var acc = new BankAccount("A", init);
            acc.Debit(amt);
            Assert.AreEqual((decimal)expected, acc.Balance);
        }

        [DataTestMethod]
        [DataRow(100, 101)]         // BVA: balance + 1
        [DataRow(100, 150)]         // EP invalid: too much
        public void Debit_InsufficientFunds_Throws(int init, int amt)
        {
            var acc = new BankAccount("A", init);
            Assert.ThrowsException<InvalidOperationException>(() => acc.Debit(amt));
        }

        [TestMethod]
        public void Debit_Negative_Throws()
        {
            var acc = new BankAccount("A", 100);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => acc.Debit(-1));
        }

        // ---- Credit ----
        [DataTestMethod]
        [DataRow(1000, 200, 1200)]
        [DataRow(100, 0, 100)]
        [DataRow(0, 1, 1)]
        public void Credit_Valid_UpdatesBalance(int init, int amt, int expected)
        {
            var acc = new BankAccount("A", init);
            acc.Credit(amt);
            Assert.AreEqual((decimal)expected, acc.Balance);
        }

        [TestMethod]
        public void Credit_Negative_Throws()
        {
            var acc = new BankAccount("A", 100);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => acc.Credit(-1));
        }

        // ---- Withdraw ----
        [DataTestMethod]
        [DataRow(1000, 200, 800)]
        [DataRow(100, 100, 0)]
        public void Withdraw_Valid_UpdatesBalance(int init, int amt, int expected)
        {
            var acc = new BankAccount("A", init);
            acc.Withdraw(amt);
            Assert.AreEqual((decimal)expected, acc.Balance);
        }

        [TestMethod]
        public void Withdraw_InsufficientFunds_Throws()
        {
            var acc = new BankAccount("A", 100);
            Assert.ThrowsException<InvalidOperationException>(() => acc.Withdraw(101));
        }
    }
}
