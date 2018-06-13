using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
                string emailPassIdFB = CreateAccountandLike(proxy);
                try
                {
                    
                    
                    if (emailPassIdFB != null)
                    {
                        string country = getCountryofProxy(proxy);
                        w.WriteToFileThreadSafe(emailPassIdFB+"\t\t"+ country, "adsLog1.txt");
                    }
                }
                catch (Exception e)
                {
                    w.WriteToFileThreadSafe(emailPassIdFB + "\t\t" + "country error", "adsLog1.txt");
                    Console.WriteLine("{0} Second exception caught.", e);
                }  
            }
            

        }

        private static string getCountryofProxy(string proxy)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create("http://ip-score.com");
            //request.Proxy = new WebProxy(proxy.Split(':')[0], int.Parse(proxy.Split(':')[1]));
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            string filename = proxy.Replace("\t", " ");
            filename = filename + ".txt";
            File.WriteAllText(filename, responseFromServer);
            string info = File.ReadLines(filename).Skip(145).First() + File.ReadLines(filename).Skip(146).First() + File.ReadLines(filename).Skip(147).First();
            info = info.Remove(0, info.IndexOf("png\">") + 6);
            info = info.Replace("</p>							<p><em>State:</em> ", "\t");
            info = info.Replace("</p>							<p><em>City:</em> ", "\t");
            info = info.Replace("</p>", "");
            return info;
        }

        private string CreateAccountandLike(string proxy)
        {

            {
                //!Make sure to add the path to where you extracting the chromedriver.exe:
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--disable-popup-blocking");
                options.AddArguments("--disable-notifications");
                //options.AddUserProfilePreference("profile.managed_default_content_settings.images", 2);
                //options.AddArguments("--proxy-server="+proxy);
                //options.AddArguments("--proxy-server=socks5://" + proxy);
                string proxyFromFile = ReadFileAtLine(1, "proxy.txt").Replace("\t", ":");

                //var userAgent = ReadRandomLineOfFile("useragentswitcher.txt");
                //options.AddArgument("--user-agent="+ userAgent);
                IWebDriver driver = new ChromeDriver(options); //<-Add your path
                driver.Manage().Window.Position = new Point(-2000, 0);
                //FirefoxProfileManager profileManager = new FirefoxProfileManager();
                //FirefoxProfile profile = profileManager.GetProfile("default");
                //profile.SetPreference("dom.webnotifications.enabled", false);
                //IWebDriver driver = new FirefoxDriver(profile);
                //IWebDriver driver = new FirefoxDriver();

                driver.Navigate().GoToUrl("https://generator.email/inbox7/");
                var email="";
                try
                {
                    email = driver.FindElement(By.Id("email_ch_text")).Text;
                }
                catch (Exception)
                {
                    driver.Quit();
                    return null;

                }
                driver.Navigate().GoToUrl("https://www.facebook.com/");

                int Numrd;
                Random rd = new Random();
                Numrd = rd.Next(1, 3);
                var firstname = ReadRandomLineOfFile("vnname.txt").Split(' ')[Numrd];
                System.Threading.Thread.Sleep(1000);
                Numrd = rd.Next(1, 3);
                firstname += " " + ReadRandomLineOfFile("vnname.txt").Split(' ')[Numrd];
                System.Threading.Thread.Sleep(1000);
                Numrd = rd.Next(1, 3);
                var lastname = ReadRandomLineOfFile("vnname.txt").Split(' ')[Numrd];
                System.Threading.Thread.Sleep(1000);
                Numrd = rd.Next(1, 3);
                lastname += " "+ReadRandomLineOfFile("vnname.txt").Split(' ')[Numrd];
                var password = GeneratePassword(10, 3);

                driver.FindElement(By.Name("lastname")).SendKeys(lastname);
                driver.FindElement(By.Name("firstname")).SendKeys(firstname);
                driver.FindElement(By.Name("reg_email__")).SendKeys(email);
                driver.FindElement(By.Name("reg_passwd__")).SendKeys(password);
                driver.FindElement(By.Name("reg_email_confirmation__")).SendKeys(email);
                
                
                Numrd = rd.Next(1, 28);
                driver.FindElement(By.Id("day")).SendKeys(Numrd.ToString());
                Numrd = rd.Next(1962, 2000);
                driver.FindElement(By.Id("year")).SendKeys(Numrd.ToString());
                driver.FindElement(By.Id("month")).SendKeys(Keys.Down+ Keys.Down + Keys.Down + Keys.Down);
                IWebElement body = driver.FindElement(By.TagName("body"));
                body.SendKeys(OpenQA.Selenium.Keys.Tab);
                body.SendKeys(OpenQA.Selenium.Keys.Tab);
                body.SendKeys(OpenQA.Selenium.Keys.Tab);
                driver.FindElement(By.Name("sex")).Click();
                driver.FindElement(By.Name("websubmit")).Click();
                System.Threading.Thread.Sleep(10000);
                body = driver.FindElement(By.TagName("body"));
                if (body.Text.Contains("again")|| body.Text.Contains("Only you will see your number"))
                {
                    driver.Quit();
                    return null; 
                }
                driver.Navigate().GoToUrl("https://generator.email/inbox7/");
                System.Threading.Thread.Sleep(5000);
                string[] separatingChars1 = { "Subject: " };
                //try
                //{
                //    var verificationCode = driver.FindElement(By.TagName("body")).Text.Split(separatingChars1, System.StringSplitOptions.RemoveEmptyEntries)[1].Substring(0, 5);
                //}
                //catch (Exception)
                //{
                //    driver.Quit();
                //    return null;
                //}
                driver.Navigate().Back();
                //driver.FindElement(By.Name("code")).SendKeys(verificationCode);
                //driver.FindElement(By.Name("code")).Submit();
                System.Threading.Thread.Sleep(5000); body = driver.FindElement(By.TagName("body"));
                body.SendKeys(OpenQA.Selenium.Keys.Enter);
                driver.Navigate().Back();
                driver.FindElement(By.CssSelector("._1vp5")).Click();
                System.Threading.Thread.Sleep(5000);

                string idFB = driver.Url;

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
                for (int i = 0; i < 5; i++)
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
                        System.Threading.Thread.Sleep(200);
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
                for (int i = 0; i < 3; i++)
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
                        System.Threading.Thread.Sleep(200);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Second exception caught.", ex);
                    }
                }
                //neu ko co ori nghia la bi checkpoint thi ko log email
                if (!driver.Url.Contains("ori")) email = null;
                driver.Quit();
                return email+"\t"+password+"\t" + idFB;
                
            }
        }

        public static void WriteLineEmptyFile(string content = "", string filewrite = "pvas.txt")
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filewrite, false))
            {

                file.WriteLine(content.ToLower());

            }
        }

        public static string GeneratePassword(int Length, int NonAlphaNumericChars)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            string allowedNonAlphaNum = "!@#$%^&*()_-+=[{]};:<>|./?";
            Random rd = new Random();

            if (NonAlphaNumericChars > Length || Length <= 0 || NonAlphaNumericChars < 0)
                throw new ArgumentOutOfRangeException();

            char[] pass = new char[Length];
            int[] pos = new int[Length];
            int i = 0, j = 0, temp = 0;
            bool flag = false;

            //Random the position values of the pos array for the string Pass
            while (i < Length - 1)
            {
                j = 0;
                flag = false;
                temp = rd.Next(0, Length);
                for (j = 0; j < Length; j++)
                    if (temp == pos[j])
                    {
                        flag = true;
                        j = Length;
                    }

                if (!flag)
                {
                    pos[i] = temp;
                    i++;
                }
            }

            //Random the AlphaNumericChars
            for (i = 0; i < Length - NonAlphaNumericChars; i++)
                pass[i] = allowedChars[rd.Next(0, allowedChars.Length)];

            //Random the NonAlphaNumericChars
            for (i = Length - NonAlphaNumericChars; i < Length; i++)
                pass[i] = allowedNonAlphaNum[rd.Next(0, allowedNonAlphaNum.Length)];

            //Set the sorted array values by the pos array for the rigth posistion
            char[] sorted = new char[Length];
            for (i = 0; i < Length; i++)
                sorted[i] = pass[pos[i]];

            string Pass = new String(sorted);

            return Pass;
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