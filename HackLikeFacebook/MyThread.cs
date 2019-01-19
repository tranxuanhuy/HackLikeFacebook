using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
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
                CreateAccountandLike(proxy);
                
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

        private void CreateAccountandLike(string proxy)
        {

            {
                //!Make sure to add the path to where you extracting the chromedriver.exe:
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--disable-popup-blocking");
                options.AddArguments("--disable-notifications");
                //options.AddUserProfilePreference("profile.managed_default_content_settings.images", 2);
                //options.AddArguments("--proxy-server="+proxy);
                //options.AddArguments("--proxy-server=socks5://" + proxy);
                //string proxyFromFile = ReadFileAtLine(1, "proxy.txt").Replace("\t", ":");

                var userAgent = ReadRandomLineOfFile("useragentswitcher.txt");
                options.AddArgument("--user-agent=" + userAgent);
                IWebDriver driver = new ChromeDriver(options); //<-Add your path
                                                               //driver.Manage().Window.Position = new Point(-2000, 0);
                                                               //FirefoxProfileManager profileManager = new FirefoxProfileManager();
                                                               //FirefoxProfile profile = profileManager.GetProfile("Default User");
                                                               //profile.SetPreference("dom.webnotifications.enabled", false);
                                                               //IWebDriver driver = new FirefoxDriver(profile);



                
                try
                {
                    driver.Navigate().GoToUrl("https://www.facebook.com/");
                    driver.FindElement(By.Name("email")).SendKeys("0932415093");
                    driver.FindElement(By.Name("pass")).SendKeys("34ERdfcv#$");
                    driver.FindElement(By.Name("pass")).SendKeys(Keys.Enter);

                    //like
                    IWebElement body = driver.FindElement(By.TagName("body"));
                    for (int i = 0; i < 4; i++)
                    {
                        body.SendKeys(OpenQA.Selenium.Keys.End);
                        System.Threading.Thread.Sleep(2000);
                    }
                    var buttonLikeList = driver.FindElements(By.CssSelector("._6a-y._3l2t._18vj"));
                    foreach (var buttonLike in buttonLikeList)
                    {
                        try
                        {
                            if (buttonLike.GetAttribute("aria-pressed") == "false")
                                buttonLike.Click();
                            System.Threading.Thread.Sleep(500);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("{0} Second exception caught.", ex);
                        }
                    }

                    //accept fr
                    driver.Navigate().GoToUrl("https://www.facebook.com/find-friends/browser/");
                    body = driver.FindElement(By.TagName("body"));
                    for (int i = 0; i < 1; i++)
                    {
                        body.SendKeys(OpenQA.Selenium.Keys.End);
                        System.Threading.Thread.Sleep(2000);
                    }
                    buttonLikeList = driver.FindElements(By.CssSelector("._42ft._4jy0._4jy3._4jy1.selected._51sy"));
                    foreach (var buttonLike in buttonLikeList)
                    {
                        try
                        {
                            
                                buttonLike.Click();
                            System.Threading.Thread.Sleep(2000);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("{0} Second exception caught.", ex);
                        }
                    }

                    //add fr
                         buttonLikeList = driver.FindElements(By.CssSelector("._42ft._4jy0.FriendRequestAdd.addButton._4jy3._4jy1.selected._51sy"));
                    foreach (var buttonLike in buttonLikeList)
                    {
                        try
                        {

                            buttonLike.Click();
                            System.Threading.Thread.Sleep(2000);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("{0} Second exception caught.", ex);
                        }
                    }
                    driver.Quit();
                }
                catch (Exception)
                {

                    throw;
                }

               
              
                
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