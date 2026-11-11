using System.Globalization;
using BasicMath;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicMathTests
{
    public class TestData
    {
        public double A { get; set; }
        public double B { get; set; }
        public double Expected { get; set; }
    }

    // ---------- Exercise 2: CSV data source ----------
    [TestClass]
    public class BasicMathsCsvTests
    {
        private readonly BasicMaths bm = new BasicMaths();

        public static IEnumerable<object[]> Load(string file)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "TestData", file);
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            foreach (var r in csv.GetRecords<TestData>().ToList())
                yield return new object[] { r.A, r.B, r.Expected };
        }

        public static IEnumerable<object[]> AddData() => Load("add_testdata.csv");
        public static IEnumerable<object[]> SubtractData() => Load("subtract_testdata.csv");
        public static IEnumerable<object[]> MultiplyData() => Load("multiply_testdata.csv");
        public static IEnumerable<object[]> DivideData() => Load("divide_testdata.csv");

        [DataTestMethod, DynamicData(nameof(AddData), DynamicDataSourceType.Method)]
        public void Add_Csv(double a, double b, double e) => Assert.AreEqual(e, bm.Add(a, b));

        [DataTestMethod, DynamicData(nameof(SubtractData), DynamicDataSourceType.Method)]
        public void Subtract_Csv(double a, double b, double e) => Assert.AreEqual(e, bm.Subtract(a, b));

        [DataTestMethod, DynamicData(nameof(MultiplyData), DynamicDataSourceType.Method)]
        public void Multiply_Csv(double a, double b, double e) => Assert.AreEqual(e, bm.Multiply(a, b));

        [DataTestMethod, DynamicData(nameof(DivideData), DynamicDataSourceType.Method)]
        public void Divide_Csv(double a, double b, double e) => Assert.AreEqual(e, bm.Divide(a, b));
    }
}
