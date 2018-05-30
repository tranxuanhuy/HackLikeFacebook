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
using System.IO;
using System.Threading;

namespace HackLikeFacebook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private static void CreateThread()
        {
            MyThread[] thr = new MyThread[100];
            Thread[] tid = new Thread[100];
            int numberInstantWeb = int.Parse(ReadFileAtLine(1, "config").Split(':')[1]);
            for (int i = 0; i < numberInstantWeb; i++)
            {





                string proxy = ReadProxyAtLine(i + 1, "proxy.txt");
                thr[i] = new MyThread();
                tid[i] = new Thread(new ThreadStart(thr[i].Thread1));
                tid[i].Name = proxy;
                tid[i].Start();




            }

            for (int i = 0; i < numberInstantWeb; i++)
            {
                tid[i].Join();
            }

            IncrementProxyOrderAstrill();
        }

        private static string ReadFileAtLine(int p, string file)
        {
            return File.ReadLines(file).Skip(p - 1).First();

        }

        private static void IncrementProxyOrderAstrill()
        {
            string SttAstrillProxy = ReadProxyAtLine(1, "toAutoitData.txt");
            if (int.Parse(SttAstrillProxy )== 128)
            {
                SttAstrillProxy = "1"; 
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("toAutoitData.txt"))
            {
                file.Write(int.Parse(SttAstrillProxy) + 1);
            }
            System.Diagnostics.Process cmd;
            cmd = System.Diagnostics.Process.Start("changeSTTAstrillProxy.exe");
            cmd.WaitForExit();
        }

        public static string ReadProxyAtLine(int p, string file)
        {
            string proxy = File.ReadLines(file).Skip(p - 1).First();
            //string[] aproxy = proxy.Split('\t');
            return proxy;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                CreateThread(); 
            }
        }

      
    }
}
