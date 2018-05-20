using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace HackLikeFacebook
{
    internal class MyThread
    {
        public void Thread1()
        {
            Thread thr = Thread.CurrentThread;
            string proxy = thr.Name;

 
            Demo w = new Demo();
            for (int i = 0; i < 1; i++)
            {

                try
                {
                    string emailCreated = CreateAccountandLike();

                    if (emailCreated!=null)
                    {
                        w.WriteToFileThreadSafe(emailCreated, "adsLog1.txt"); 
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Second exception caught.", e);
                }  
            }
            

        }

        private string CreateAccountandLike()
        {

            {
                //!Make sure to add the path to where you extracting the chromedriver.exe:
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--disable-popup-blocking");
                options.AddArguments("--disable-notifications");
                string proxyFromFile = ReadFileAtLine(1, "proxy.txt").Replace("\t", ":");
                var proxy = new Proxy();
                //Set the http proxy value, host and port.
                proxy.HttpProxy = proxyFromFile;
                proxy.SslProxy = proxyFromFile;
                //Set the proxy to the Chrome options
                //options.Proxy = proxy;
                var userAgent = ReadRandomLineOfFile("useragentswitcher.txt");
                //options.AddArgument("--user-agent="+ userAgent);
                IWebDriver driver = new ChromeDriver(@"C:\", options); //<-Add your path

                driver.Navigate().GoToUrl("https://generator.email/inbox7/");
                var email = driver.FindElement(By.Id("email_ch_text")).Text;
                driver.Navigate().GoToUrl("https://www.facebook.com/");

                int Numrd;
                Random rd = new Random();
                Numrd = rd.Next(1, 3);
                var lastname = ReadRandomLineOfFile("vnname.txt").Split(' ')[Numrd];
                System.Threading.Thread.Sleep(1000);
                Numrd = rd.Next(1, 3);
                var firstname = ReadRandomLineOfFile("vnname.txt").Split(' ')[Numrd];
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
                System.Threading.Thread.Sleep(10000);
                driver.Navigate().GoToUrl("https://generator.email/inbox7/");
                System.Threading.Thread.Sleep(5000);
                string[] separatingChars1 = { "Subject: " };
                try
                {
                    var verificationCode = driver.FindElement(By.TagName("body")).Text.Split(separatingChars1, System.StringSplitOptions.RemoveEmptyEntries)[1].Substring(0, 5);
                }
                catch (Exception)
                {
                    driver.Quit();
                    return null;
                }
                driver.Navigate().Back();
                //driver.FindElement(By.Name("code")).SendKeys(verificationCode);
                //driver.FindElement(By.Name("code")).Submit();
                System.Threading.Thread.Sleep(5000); body = driver.FindElement(By.TagName("body"));
                body.SendKeys(OpenQA.Selenium.Keys.Enter);

                //follow tam tho
                driver.Navigate().GoToUrl("https://www.facebook.com/thanh.tam.969");
                var buttonLikeList = driver.FindElements(By.CssSelector("._42ft._4jy0._63_s._4jy4._517h._51sy"));
                foreach (var buttonLike in buttonLikeList)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(2000);
                        //if (buttonLike.GetAttribute("aria-pressed") == "false")
                        buttonLike.Click();
                        System.Threading.Thread.Sleep(2000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Second exception caught.", ex);
                    }
                }

                //like trang tam tho
                driver.Navigate().GoToUrl("https://www.facebook.com/thanh.tam.969");
                body = driver.FindElement(By.TagName("body"));
                for (int i = 0; i < 6; i++)
                {
                    body.SendKeys(OpenQA.Selenium.Keys.End);
                    System.Threading.Thread.Sleep(2000);
                }
                buttonLikeList = driver.FindElements(By.CssSelector(".UFILikeLink._4x9-._4x9_._48-k"));
                foreach (var buttonLike in buttonLikeList)
                {
                    try
                    {
                        if (buttonLike.GetAttribute("aria-pressed") == "false")
                            buttonLike.Click();
                        System.Threading.Thread.Sleep(2000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Second exception caught.", ex);
                    }
                }

                //like fanpage ori japan

                //like page
                driver.Navigate().GoToUrl("https://www.facebook.com/orijapanskincarebeauty/");
                buttonLikeList = driver.FindElements(By.CssSelector(".likeButton._4jy0._4jy4._517h._51sy._42ft"));
                foreach (var buttonLike in buttonLikeList)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(2000);
                        //if (buttonLike.GetAttribute("aria-pressed") == "false")
                        buttonLike.Click();
                        System.Threading.Thread.Sleep(2000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Second exception caught.", ex);
                    }
                }

                //like post trong page
                driver.Navigate().GoToUrl("https://www.facebook.com/orijapanskincarebeauty/");
                body = driver.FindElement(By.TagName("body"));
                for (int i = 0; i < 4; i++)
                {
                    body.SendKeys(OpenQA.Selenium.Keys.End);
                    System.Threading.Thread.Sleep(2000);
                }
                buttonLikeList = driver.FindElements(By.CssSelector(".UFILikeLink._4x9-._4x9_._48-k"));
                foreach (var buttonLike in buttonLikeList)
                {
                    try
                    {
                        if (buttonLike.GetAttribute("aria-pressed") == "false")
                            buttonLike.Click();
                        System.Threading.Thread.Sleep(2000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Second exception caught.", ex);
                    }
                }
                
                driver.Quit();
                //neu ko co like button nghia la bi checkpoint thi ko log email
                if (buttonLikeList.Count!=0)
                {
                    return email;
                }
                else
                {
                    return null;
                }
            }
        }

        public static void WriteLinePostingLog(string content = "", string filewrite = "adsLog.txt")
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filewrite, true))
            {

                file.WriteLine(content);

            }
        }

        public static string ReadFileAtLine(int p, string file)
        {
            return File.ReadLines(file).Skip(p - 1).First();

        }

        public static string ReadRandomLineOfFile(string file)
        {
            string[] lines = File.ReadAllLines(file); //i hope that the file is not too big
            Random rand = new Random();
            return lines[rand.Next(lines.Length)];
        }
        public static implicit operator Thread(MyThread v)
        {
            throw new NotImplementedException();
        }
    }
}