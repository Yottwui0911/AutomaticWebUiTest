using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
  /// <summary>
  /// Selenium WebDriver を使った画面テストに共通する機能を提供します。
  /// </summary>
  [TestClass]
  public class SeleniumTest : IisProcess
  {
    /// <summary>
    /// WEB ブラウザのドライバーを取得します。
    /// </summary>
    private ChromeDriver _driver;

    /// <summary>
    /// ChromeのDriverの場所
    /// </summary>
    private string driverPath = "chromedriver_win32";

    [TestInitialize]
    public virtual void TestInitialize()
    {
      // ChromeOptionsオブジェクトを生成します。
      // var options = new ChromeOptions();
      // options.AddArgument("--headless");

      // this._driver = new ChromeDriver(driverPath, options);

      this._driver = new ChromeDriver(driverPath);
    }

    [TestCleanup]
    public virtual void TestCleanup()
    {
      this._driver.Quit();
    }

    [TestMethod]
    public void RegisterMaleTest()
    {
      this._driver.Navigate().GoToUrl(BaseAddress + "/Register");

      this._driver.FindElementById("name").SendKeys("Yottwui");

      Thread.Sleep(1000);

      this._driver.FindElementById("male").SendKeys(Keys.Space);

      Thread.Sleep(1000);

      var reason = this._driver.FindElementById("reason");
      var selectElement = new SelectElement(reason);
      selectElement.SelectByValue("テレビで見た");

      Thread.Sleep(1000);

      this._driver.FindElementById("registerButton").Click();

      var result = this._driver.FindElementById("result");
      Assert.AreEqual(result.Text.Replace("\r\n", ""), $"登録者名： Yottwui\n性別： 男性\n登録する経緯： テレビで見た".Replace("\n", ""));

      Thread.Sleep(1000);

    }

    [TestMethod]
    public void RegisterFemaleTest()
    {
      this._driver.Navigate().GoToUrl(BaseAddress + "/Register");

      this._driver.FindElementById("name").SendKeys("Peach");

      Thread.Sleep(1000);

      this._driver.FindElementById("female").SendKeys(Keys.Space);

      Thread.Sleep(1000);

      var reason = this._driver.FindElementById("reason");
      var selectElement = new SelectElement(reason);
      selectElement.SelectByValue("SNSで見た");

      Thread.Sleep(1000);

      this._driver.FindElementById("registerButton").Click();

      var result = this._driver.FindElementById("result");
      Assert.AreEqual(result.Text.Replace("\r\n", ""), $"登録者名： Peach\n性別： 女性\n登録する経緯： SNSで見た".Replace("\n", ""));

      Thread.Sleep(1000);

    }
  }
}
