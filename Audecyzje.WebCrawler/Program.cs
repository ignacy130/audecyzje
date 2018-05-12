using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace Audecyzje.WebCrawler
{
    class Program
    {
        static string UrlDecisonsAfter2016 = @"https://bip.warszawa.pl/Menu_podmiotowe/biura_urzedu/SD/decyzje/default.htm";
        static string UrlDecisonsBefore2016 = @"https://bip.warszawa.pl/Menu_podmiotowe/biura_urzedu/SD/decyzje_1998_2016/default.htm";

        static string DirectUrlsFileName = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\DirectUrls.txt";
        static string LogFile = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\LogFile.txt";

        static void Main(string[] args)
        {
            if (CheckAccesToFolders())
            {

                DownloadDirectUrls();
                //DownloadFiles();
                //For downloading files use JDownloader http://jdownloader.org/download/index

            }
            else
            {
                Console.WriteLine("Bad file paths");
            }
            Console.WriteLine("Program finished its work. Press any key to finish");
            Console.ReadKey();
        }

        private static void AddLog(string message)
        {
            using (StreamWriter sw = File.AppendText(LogFile))
            {
                sw.WriteLine(message);
            }
        }

        private static void DownloadDirectUrls()
        {
            List<string> linksToPages = new List<string>();
            HtmlDocument page = new HtmlDocument();
            WebClient client = new WebClient();
            for (int i = 1; i < 11; i++)
            {
                var pageString = client.DownloadString(UrlDecisonsAfter2016 + "?page=" + i);
                page.LoadHtml(pageString);
                var nodes = page.DocumentNode.SelectNodes("//a[@href]");
                foreach (var node in nodes)
                {
                    var nodevalue = node.Attributes["href"].Value;
                    if (nodevalue.Contains("SD/decyzje/20") && !nodevalue.Contains("default.htm"))
                    {
                        linksToPages.Add(nodevalue);
                    }
                }
            }
            var domain = "https://bip.warszawa.pl";

            using (StreamWriter sw = new StreamWriter(File.Open(DirectUrlsFileName, FileMode.Create), Encoding.Unicode))
            {

                foreach (var link in linksToPages)
                {
                    var pageString = client.DownloadString(domain + link);
                    page.LoadHtml(pageString);
                    var nodes = page.DocumentNode.SelectNodes("//a[@href]");
                    foreach (var node in nodes)
                    {
                        var nodevalue = node.Attributes["href"].Value;
                        if (nodevalue.Contains("NR/rdonlyres"))
                        {
                            sw.WriteLine(domain + HttpUtility.HtmlDecode(nodevalue));
                        }
                    }
                }
            }
        }

        static bool CheckAccesToFolders()
        {
            try
            {
                if (!File.Exists(DirectUrlsFileName))
                {
                    File.Create(DirectUrlsFileName);
                }
                else
                {
                    File.OpenRead(DirectUrlsFileName).Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
