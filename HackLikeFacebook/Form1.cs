using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace HackLikeFacebook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //!Make sure to add the path to where you extracting the chromedriver.exe:
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-popup-blocking");
            IWebDriver driver = new ChromeDriver(@"C:\Users\Administrator\Downloads\chromedriver_win32",options); //<-Add your path
          
driver.Navigate().GoToUrl("https://www.tempmailaddress.com/");
var email=driver.FindElement(By.Id("email")).Text;
driver.Navigate().GoToUrl("https://www.facebook.com/");
var lastname = "dau";
var firstname = "nach";
var password = "950460";
driver.FindElement(By.Name("lastname")).SendKeys(lastname);
driver.FindElement(By.Name("firstname")).SendKeys(firstname);
driver.FindElement(By.Name("reg_email__")).SendKeys(email);
driver.FindElement(By.Name("reg_passwd__")).SendKeys(password);
driver.FindElement(By.Name("reg_email_confirmation__")).SendKeys(email);
IWebElement body = driver.FindElement(By.TagName("body"));
body.SendKeys(OpenQA.Selenium.Keys.Tab);
body.SendKeys(OpenQA.Selenium.Keys.Tab);
body.SendKeys(OpenQA.Selenium.Keys.Tab);
driver.FindElement(By.Name("sex")).Click();
driver.FindElement(By.Name("websubmit")).Click();
System.Threading.Thread.Sleep(5000);
driver.Navigate().GoToUrl("https://www.tempmailaddress.com/");
System.Threading.Thread.Sleep(5000);
var verificationCode = driver.FindElement(By.Id("schranka")).Text.Substring(12,5);
driver.Navigate().Back();
driver.FindElement(By.Name("code")).SendKeys(verificationCode);
driver.FindElement(By.Name("code")).Submit();
System.Threading.Thread.Sleep(5000);  body = driver.FindElement(By.TagName("body"));
body.SendKeys(OpenQA.Selenium.Keys.Enter);

driver.Navigate().GoToUrl("https://www.facebook.com/thanh.tam.969");
body = driver.FindElement(By.TagName("body"));
for (int i = 0; i < 10; i++)
{
    body.SendKeys(OpenQA.Selenium.Keys.End);
    System.Threading.Thread.Sleep(2000); 
}
var buttonLikeList=driver.FindElements(By.CssSelector(".UFILikeLink._4x9-._4x9_._48-k"));
foreach (var buttonLike in buttonLikeList)
{
    try
    {
        if (buttonLike.GetAttribute("aria-pressed")=="false")
        buttonLike.Click();
        System.Threading.Thread.Sleep(2000); 
    }
    catch (Exception ex)
    {
        Console.WriteLine("{0} Second exception caught.", ex);
    }
}
Console.WriteLine("{0} Second exception caught.", e);

        }
    }
}
