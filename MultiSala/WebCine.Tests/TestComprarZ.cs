using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium.Chrome;
using Cine.Model;
using System.Linq;
using Cine;
namespace SeleniumTests
{
    [TestClass]
    public class TestComprarZ
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;


        private Process _iisProcess;
        private string CurrentPath;
        private string AppLocation = @"\WebCine\obj\Debug\Package\PackageTmp";
        private int Port = 10600;
    /*
        [TestInitialize]
        public static void ClassInit(TestContext context)
        {
            
            using (var ctx = new DatosDB())
            {
                ctx.Sesiones.Find(2);
            }
        }
     */
    [TestInitialize]
        public void SetupTest()
        {

            using (var ctx = new DatosDB())
            {
                Sesion s = ctx.Sesiones.Find(1);
                s.Abierto = true;
                ctx.SaveChanges();

                Venta V =  new Venta();
                ctx.Ventas.Add(V);

            }

            var thread = new Thread(StartIisExpress) { IsBackground = true };

            thread.Start();
            Thread.Sleep(2000);
            driver = new ChromeDriver();
            baseURL = "http://localhost:" + Port + "/";
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
       // [Ignore]
        [TestMethod]
        public void TheComprarTest()
        {
            driver.Navigate().GoToUrl(baseURL + "INDEX.html");
            driver.FindElement(By.LinkText("Entradas")).Click();
          
            driver.FindElement(By.LinkText("Comprar Entradas")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("sesionid")).Clear();
            driver.FindElement(By.Id("sesionid")).SendKeys("1");
            driver.FindElement(By.Id("nentradas")).Clear();
            driver.FindElement(By.Id("nentradas")).SendKeys("5");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button.btn.btn-success")).Click();

            Thread.Sleep(10000);
            Thread.Sleep(15000);
            Thread.Sleep(10000);

            Assert.AreEqual("Realizado", driver.FindElement(By.CssSelector("h1.site-title")).Text);
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
