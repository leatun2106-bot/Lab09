using System.Globalization;
using CsvHelper;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Allure.NUnit;
using Allure.NUnit.Attributes;

namespace LoginAutomation
{
    public class LoginData
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Expected { get; set; } = "";
    }
    [AllureNUnit]
    [AllureSuite("Login Tests")]
    [AllureFeature("Login")]
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver driver = null!;

        public static IEnumerable<TestCaseData> GetTestData()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "LoginTestData.csv");
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower()
            };
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);
            foreach (var r in csv.GetRecords<LoginData>().ToList())
                yield return new TestCaseData(r.Username, r.Password, r.Expected)
                    .SetName($"Login_{r.Username}_{r.Password}");
        }

        [SetUp]
        public void SetUp() => driver = new ChromeDriver();

        [TearDown]
        public void TearDown() { driver.Quit(); driver.Dispose(); }

        [TestCaseSource(nameof(GetTestData))]
        public void Login_ShowsExpectedMessage(string username, string password, string expected)
        {
            var page = Path.Combine(AppContext.BaseDirectory, "login.html");
            driver.Navigate().GoToUrl(new Uri(page).AbsoluteUri);

            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("loginButton")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.Id("message")).Text.Length > 0);

            Assert.That(driver.FindElement(By.Id("message")).Text, Is.EqualTo(expected));
        }
    }
}