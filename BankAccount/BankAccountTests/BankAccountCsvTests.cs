using System.Globalization;
using BankAccountApp;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankAccountTests
{
    public class BankTestData
    {
        public string CustomerName { get; set; } = "";
        public decimal InitialBalance { get; set; }
        public decimal DebitAmount { get; set; }
        public string ExpectedBalance { get; set; } = "";
    }

    [TestClass]
    public class BankAccountCsvTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "BankAccountTestData.csv");
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            foreach (var r in csv.GetRecords<BankTestData>().ToList())
                yield return new object[] { r.CustomerName, r.InitialBalance, r.DebitAmount, r.ExpectedBalance };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        public void Debit_Csv(string name, decimal init, decimal debit, string expected)
        {
            var acc = new BankAccount(name, init);
            if (expected == "Insufficient funds")
            {
                Assert.ThrowsException<InvalidOperationException>(() => acc.Debit(debit));
            }
            else
            {
                acc.Debit(debit);
                Assert.AreEqual(decimal.Parse(expected, CultureInfo.InvariantCulture), acc.Balance);
            }
        }
    }
}
