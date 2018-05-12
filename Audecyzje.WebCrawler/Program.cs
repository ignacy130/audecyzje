using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Audecyzje.WebCrawler
{
    class Program
    {
        static string UrlDecisonsAfter2016 = @"https://bip.warszawa.pl/Menu_podmiotowe/biura_urzedu/SD/decyzje/default.htm";
        static string UrlDecisonsBefore2016 = @"https://bip.warszawa.pl/Menu_podmiotowe/biura_urzedu/SD/decyzje_1998_2016/default.htm";

        static string DirectUrlsFileName = @"C:\Users\janbu\Desktop\Repos\AuDecyzje\Audecyzje.WebCrawler\Resources\DirectUrls.txt";
        static string FolderForFiles = @"C:\Users\janbu\Desktop\Repos\AuDecyzje\Audecyzje.WebCrawler\Resources\Downloaded";

        static void Main(string[] args)
        {
            if (CheckAccesToFolders())
            {

                DownloadDirectUrls();


            }
            else
            {
                Console.WriteLine("Bad file paths");
            }
            Console.WriteLine("Program finished its work. Press any key to finish");
            Console.ReadKey();
        }

        private static void DownloadDirectUrls()
        {
            List<string> linksToPages = new List<string>();
            HtmlDocument page = new HtmlDocument();
            WebClient client = new WebClient();
            for (int i = 1; i < 2; i++)
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
            //StringBuilder sb = new StringBuilder();
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
                            //nodevalue = WebUtility.UrlDecode(nodevalue);
                            //sb.AppendLine(domain + nodevalue);
                            sw.WriteLine(domain + DecodeString(nodevalue));
                        }
                    }
                }
            }
            //File.WriteAllText(DirectUrlsFileName, sb.ToString());
        }
        static string DecodeString(string unicodeString)
        {
            
            Encoding newCoding = Encoding.Unicode;
            Encoding oldCoding = Encoding.UTF32;

            // Convert the string into a byte array.
            byte[] unicodeBytes = oldCoding.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(oldCoding, newCoding, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            char[] asciiChars = new char[newCoding.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            newCoding.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string newString = new string(asciiChars);

            return newString;
        }

        static bool CheckAccesToFolders()
        {
            try
            {
                Directory.CreateDirectory(FolderForFiles);
                var testfile = Path.Combine(FolderForFiles, "testfile");
                if (!File.Exists(DirectUrlsFileName))
                {
                    File.Create(DirectUrlsFileName);
                }
                else
                {
                    File.OpenRead(DirectUrlsFileName).Close();
                }
                File.Create(testfile).Close();
                File.Delete(testfile);
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
