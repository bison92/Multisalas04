using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
//using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace SeleniumTests
{
    [TestClass]
    public class TestCrearVenta
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        private Process _iisProcess;
        private string CurrentPath;
        private string AppLocation = @"\WebCine\obj\Debug\Package\PackageTmp";
        private int Port = 10400;
        
        [TestInitialize]
        public void SetupTest()
        {

            var thread = new Thread(StartIisExpress) { IsBackground = true };

            thread.Start();

            Thread.Sleep(2000);

            driver = new FirefoxDriver();
            baseURL = "http://localhost:10400/";
            verificationErrors = new StringBuilder();
            Thread.Sleep(2000);
        }
        
        [TestCleanup]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            _iisProcess.Kill();
            Assert.AreEqual("", verificationErrors.ToString());
        }
        [Ignore]
        [TestMethod]
        public void TheCrearVentaTest()
        {

            driver.Navigate().GoToUrl(baseURL + "/Content/index.html");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("btn-create-venta")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("btn-calcular-precio")).Click();
            Thread.Sleep(20000);
            driver.FindElement(By.Id("btn-crear-venta")).Click();
            Thread.Sleep(16000);
            //Assert.AreEqual("7 Venta Realizada", driver.FindElement(By.Id("precioCalculado")).Text);
            //driver.FindElement(By.CssSelector("button.btn.btn-danger")).Click();
            

        }

        private void StartIisExpress()
        {
            CurrentPath = Directory.GetCurrentDirectory();
            CurrentPath = CurrentPath.Substring(0, CurrentPath.LastIndexOf("\\"));
            CurrentPath = CurrentPath.Substring(0, CurrentPath.LastIndexOf("\\"));
            CurrentPath = CurrentPath.Substring(0, CurrentPath.LastIndexOf("\\"));
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardInput = true,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", CurrentPath + AppLocation, Port)
            };

            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
                                ? startInfo.EnvironmentVariables["programfiles(x86)"]
                                : startInfo.EnvironmentVariables["programfiles"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";

            try
            {
                _iisProcess = new Process { StartInfo = startInfo };
                _iisProcess.Start();

                _iisProcess.WaitForExit();
                return;
            }
            catch (Exception e)
            {
                //_iisProcess.CloseMainWindow();
                //_iisProcess.Dispose();
            }
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
