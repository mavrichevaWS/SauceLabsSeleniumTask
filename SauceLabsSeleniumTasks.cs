using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

namespace WebDriverTasks
{
    [TestFixture]
    [Category("SauceLabs Tests"), Category("NUnit")]
    public class WebDriverTasks
    {
        private IWebDriver driver;

        // Initialize a driver
        public void TestInitialize()
        {
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);
        }

        // Login test
        [Test]
        public void NewEmailTest(string login, string pw)
        {
            string isItAuthorized = "Selenium Test";

            driver = new RemoteWebDriver(new Uri("https://lenamavricheva:35f90510-0c41-419a-925a-811973864966@ondemand.eu-central-1.saucelabs.com:443/wd/hub"),
                 BrowserSettings.webDriverSetting(TestContext.CurrentContext.Test.Name), TimeSpan.FromSeconds(600));

            driver.Navigate().GoToUrl(UserConstantData.URL);

            // Implicit wait
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            // Thread sleep
            Thread.Sleep(5000); // This is Implicit wait because the code will be fulfilled only once when DOM is loading.

            // Make the screenshot
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(System.IO.Path.Combine("C:\\Users\\user\\source\\repos\\Selenium WebDriver Tasks\\Selenium WebDriver Tasks\\assets", "tut_by.png"), ScreenshotImageFormat.Png);

            IWebElement searchEnterButton = driver.FindElement(By.CssSelector("a.enter"));
            searchEnterButton.Click();

            IWebElement searchLoginField = driver.FindElement(By.CssSelector("input[type='text']"));
            searchLoginField.SendKeys(login);

            IWebElement searchPwField = driver.FindElement(By.CssSelector("input[type='password']"));
            searchPwField.SendKeys(pw);

            IWebElement searchApplyButton = driver.FindElement(By.CssSelector("input.button.m-green.auth__enter"));
            searchApplyButton.Click();

            // Explicit wait
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            IWebElement findProfileName = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("span.uname")));
            string profileName = findProfileName.Text;
            Assert.IsTrue(profileName == isItAuthorized);

            driver.Quit();
        }

        // Options select test
        [Test]
        public void MultiSelectTest()
        {
            driver = new RemoteWebDriver(new Uri("https://lenamavricheva:35f90510-0c41-419a-925a-811973864966@ondemand.eu-central-1.saucelabs.com:443/wd/hub"),
                 BrowserSettings.webDriverSetting(TestContext.CurrentContext.Test.Name), TimeSpan.FromSeconds(600));

            driver.Navigate().GoToUrl(UserConstantData.URL2);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            string state1 = "California";
            string state2 = "New York";
            string state3 = "Pennsylvania";

            IList<IWebElement> elements = driver.FindElements(By.CssSelector("select[name='States'] > option:nth-child(3n+1)"));
            foreach (IWebElement e in elements)
            {
                e.Click();
                if (e.Selected)
                {
                    Assert.IsTrue(e.Text == state1 || e.Text == state2 || e.Text == state3);
                    Console.WriteLine("Value of the option item is selected: " + e.Selected + " " + e.Text);
                }
            }

            driver.Quit();
        }

        // Confirm box test
        [Test]
        public void ConfirmBoxTest()
        {
            driver = new RemoteWebDriver(new Uri("https://lenamavricheva:35f90510-0c41-419a-925a-811973864966@ondemand.eu-central-1.saucelabs.com:443/wd/hub"),
                 BrowserSettings.webDriverSetting(TestContext.CurrentContext.Test.Name), TimeSpan.FromSeconds(600));

            driver.Navigate().GoToUrl(UserConstantData.URL3);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchConfirmButton = driver.FindElement(By.CssSelector("button[onclick='myConfirmFunction()']"));
            searchConfirmButton.Click();

            var confirm = driver.SwitchTo().Alert();
            confirm.Accept();

            IWebElement clickResult = driver.FindElement(By.Id("confirm-demo"));
            Console.WriteLine(clickResult.Text);
            if (clickResult.Text == "You pressed OK!") Console.WriteLine("Confirm test successful");

            driver.Quit();
        }

        // Prompt box test
        [Test]
        public void PromptBoxTest()
        {
            driver = new RemoteWebDriver(new Uri("https://lenamavricheva:35f90510-0c41-419a-925a-811973864966@ondemand.eu-central-1.saucelabs.com:443/wd/hub"),
                 BrowserSettings.webDriverSetting(TestContext.CurrentContext.Test.Name), TimeSpan.FromSeconds(600));

            driver.Navigate().GoToUrl(UserConstantData.URL3);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchPromptButton = driver.FindElement(By.CssSelector("button[onclick='myPromptFunction()']"));
            searchPromptButton.Click();

            var prompt = driver.SwitchTo().Alert();
            prompt.SendKeys("This is a test prompt message");
            prompt.Accept();

            IWebElement clickResult = driver.FindElement(By.CssSelector("#prompt-demo"));
            Console.WriteLine(clickResult.Text);

            if (clickResult.Text == "You have entered 'This is a test prompt message' !") Console.WriteLine("Prompt test successful");

            driver.Quit();
        }

        // Alert box test
        [Test]
        public void AlertBoxTest()
        {
            driver = new RemoteWebDriver(new Uri("https://lenamavricheva:35f90510-0c41-419a-925a-811973864966@ondemand.eu-central-1.saucelabs.com:443/wd/hub"),
                 BrowserSettings.webDriverSetting(TestContext.CurrentContext.Test.Name), TimeSpan.FromSeconds(600));

            driver.Navigate().GoToUrl(UserConstantData.URL3);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchAlertButton = driver.FindElement(By.CssSelector("button[onclick='myAlertFunction()']"));
            searchAlertButton.Click();

            var expectedAlertText = "I am an alert box!";
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual(expectedAlertText, alert.Text);
            if (alert.Text == expectedAlertText) Console.WriteLine("Alert test successful");
            alert.Accept();

            driver.Quit();
        }


        // User wait test
        [Test]
        public void UserWaitTest()
        {
            driver = new RemoteWebDriver(new Uri("https://lenamavricheva:35f90510-0c41-419a-925a-811973864966@ondemand.eu-central-1.saucelabs.com:443/wd/hub"),
                 BrowserSettings.webDriverSetting(TestContext.CurrentContext.Test.Name), TimeSpan.FromSeconds(600));

            driver.Navigate().GoToUrl(UserConstantData.URL4);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchNewUserButton = driver.FindElement(By.CssSelector("button#save"));
            searchNewUserButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            IWebElement foto = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("img[src*='randomuser']")));
            bool image = foto.Displayed && foto.Enabled;
            if (image) Console.WriteLine("Test successful");

            driver.Quit();
        }

        // Waiting 50% loading test
        [Test]
        public void WaitLoadingTest()
        {
            driver = new RemoteWebDriver(new Uri("https://lenamavricheva:35f90510-0c41-419a-925a-811973864966@ondemand.eu-central-1.saucelabs.com:443/wd/hub"),
                 BrowserSettings.webDriverSetting(TestContext.CurrentContext.Test.Name), TimeSpan.FromSeconds(600));

            driver.Navigate().GoToUrl(UserConstantData.URL5);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchDownloadButton = driver.FindElement(By.CssSelector("button#cricle-btn"));
            searchDownloadButton.Click();

            Thread.Sleep(10300);
            IWebElement percent = driver.FindElement(By.CssSelector("div.percenttext"));
            string percentage = percent.Text;
            string value = percentage.Remove(percentage.Length - 1, 1);

            if (Int32.Parse(value) >= 50)
            {
                driver.Navigate().Refresh();
                Console.WriteLine(Int32.Parse(value));
                Console.WriteLine("Test successful");
            }

            driver.Quit();
        }

        // Select table data test
        [Test]
        public void SelectTableDataTest()
        {
            driver = new RemoteWebDriver(new Uri("https://lenamavricheva:35f90510-0c41-419a-925a-811973864966@ondemand.eu-central-1.saucelabs.com:443/wd/hub"),
                 BrowserSettings.webDriverSetting(TestContext.CurrentContext.Test.Name), TimeSpan.FromSeconds(600));

            driver.Navigate().GoToUrl(UserConstantData.URL6);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IList<IWebElement> elements = driver.FindElements(By.CssSelector("span > a.paginate_button"));
            int value = elements.Count;

            for (int i = 1; i <= value; i++)
            {
                if (driver.FindElement(By.CssSelector("a#example_next")).Enabled == true)
                {
                    IWebElement table = driver.FindElement(By.Id("example"));

                    var columns = table.FindElements(By.TagName("th"));
                    var rows = table.FindElements(By.TagName("tr"));

                    int rowIndex = 1;
                    List<object> _tableDataColections2 = new List<object>();

                    foreach (var row in rows)
                    {
                        int colIndex = 0;
                        var colDatas = row.FindElements(By.TagName("td"));
                        List<TableDataColection> _tableDataColections = new List<TableDataColection>();

                        foreach (var colValue in colDatas)
                        {
                            _tableDataColections.Add(new TableDataColection
                            {
                                ColumnName = columns[colIndex].Text,
                                ColumnValue = colValue.Text
                            });
                            colIndex++;
                        }

                        if (rows.IndexOf(row) == rows.Count - 1)
                        {
                            IWebElement searchPaginateButtonElem = driver.FindElement(By.CssSelector("a#example_next"));
                            searchPaginateButtonElem.Click();
                        }

                        _tableDataColections2.Add(_tableDataColections);

                        rowIndex++;
                    }

                    var data = from rowData in _tableDataColections2 select rowData;

                    foreach (var tableData in data)
                    {
                        /* if (tableData.ColumnName == "Age") { Console.WriteLine(tableData.ColumnName + " " + tableData.Age); }
                        else if (tableData.ColumnName == "Salary") { Console.WriteLine(tableData.ColumnName + " " + tableData.Salary); }
                        else { Console.WriteLine(tableData.ColumnName + " " + tableData.ColumnValue); } */
                    }
                }
            }

            driver.Quit();
        }
    }
}
