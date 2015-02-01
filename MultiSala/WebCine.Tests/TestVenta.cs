using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Cine;
using System.IO;


namespace SeleniumTests
{
    [TestClass]
    public class CreateVentas
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private Process _iisProcess;
        private string CurrentPath;
        private string AppLocation = @"\WebCine\obj\Debug\Package\PackageTmp";
        private int Port = 10400;

        [ClassInitialize]
        public static void TestClass(TestContext testctx)
        {
            using (var ctx = new DatosDB())
            {
                ctx.Database.Initialize(true);
            }
        }

        [TestInitialize]
        public void SetupTest()
        {
            var thread = new Thread(StartIisExpress) { IsBackground = true };
            thread.Start();
            Thread.Sleep(2000);
            driver = new ChromeDriver("C:\\chromedriver\\bin");
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
                _iisProcess.Kill();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
           
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [TestMethod]
        public void TheCreaEditaYBorraVentasTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/Content/index.html");
            driver.FindElement(By.Id("btn-create-venta")).Click();
            Thread.Sleep(12000);
            new SelectElement(driver.FindElement(By.Id("sesionid"))).SelectByText("6 : 2015-05-12T22:00:00");
            driver.FindElement(By.CssSelector("option[value=\"6\"]")).Click();
            driver.FindElement(By.Id("nentradasjoven")).Clear();
            driver.FindElement(By.Id("nentradasjoven")).SendKeys("15");
            driver.FindElement(By.Id("nentradas")).Clear();
            
            driver.FindElement(By.Id("nentradas")).SendKeys("25");
            driver.FindElement(By.Id("btn-comprar")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btn-confirmarcompra")).Click();
            Thread.Sleep(1000);
            String idventa1 = driver.FindElement(By.Id("ventaid")).GetAttribute("value");
            driver.FindElement(By.Id("btn-volver")).Click();
            Thread.Sleep(1000);
            new SelectElement(driver.FindElement(By.Id("sesionid"))).SelectByText("6 : 2015-05-12T22:00:00");
            driver.FindElement(By.CssSelector("option[value=\"6\"]")).Click();
            driver.FindElement(By.Id("nentradasjoven")).Clear();
            driver.FindElement(By.Id("nentradasjoven")).SendKeys("14");
            driver.FindElement(By.Id("nentradas")).Clear();
            driver.FindElement(By.Id("nentradas")).SendKeys("25");
            driver.FindElement(By.Id("btn-comprar")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btn-confirmarcompra")).Click();
            Thread.Sleep(1000);
            String idventa2 = driver.FindElement(By.Id("ventaid")).GetAttribute("value");
            driver.FindElement(By.Id("btn-volver")).Click();
            Thread.Sleep(1000);
            new SelectElement(driver.FindElement(By.Id("sesionid"))).SelectByText("6 : 2015-05-12T22:00:00");
            driver.FindElement(By.CssSelector("option[value=\"6\"]")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("0", driver.FindElement(By.Id("nentradasdisponibles")).Text);
            driver.FindElement(By.Id("btn-volver")).Click();
            driver.FindElement(By.Id("btn-cambia-venta")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("ventaid")).Clear();
            driver.FindElement(By.Id("ventaid")).SendKeys(idventa1);
            driver.FindElement(By.Id("btn-cambiar")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("sesionid")).Clear();
            driver.FindElement(By.Id("sesionid")).SendKeys("1");
            driver.FindElement(By.Id("nentradasjoven")).Clear();
            driver.FindElement(By.Id("nentradasjoven")).SendKeys("20");
            driver.FindElement(By.Id("nentradas")).Clear();
            driver.FindElement(By.Id("nentradas")).SendKeys("20");
            driver.FindElement(By.Id("btn-cambiar")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btn-confirmarcambio")).Click();
            Thread.Sleep(100);
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^¿Seguro que desea confirmar los cambios de la venta #"+idventa1+"[\\s\\S]$"));
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btn-comprar")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("80", driver.FindElement(By.Id("nentradasdisponibles")).Text);
            new SelectElement(driver.FindElement(By.Id("sesionid"))).SelectByText("6 : 2015-05-12T22:00:00");
            driver.FindElement(By.CssSelector("option[value=\"6\"]")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("25", driver.FindElement(By.Id("nentradasdisponibles")).Text);
            driver.FindElement(By.Id("btn-volver")).Click();
            driver.FindElement(By.Id("btn-cambia-venta")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("ventaid")).Clear();
            driver.FindElement(By.Id("ventaid")).SendKeys(idventa1);
            driver.FindElement(By.Id("btn-devolucion-venta")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btn-confirmardevolucion")).Click();
            Thread.Sleep(100);
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^¿Seguro que desea devolver la venta #" + idventa1 + "[\\s\\S]$"));
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btn-volver")).Click();
            driver.FindElement(By.Id("btn-cambia-venta")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("ventaid")).Clear();
            driver.FindElement(By.Id("ventaid")).SendKeys(idventa2);
            driver.FindElement(By.Id("btn-devolucion-venta")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btn-confirmardevolucion")).Click();
            Thread.Sleep(100);
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^¿Seguro que desea devolver la venta #" + idventa2 + "[\\s\\S]$"));
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btn-volver")).Click();
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
    }
}
