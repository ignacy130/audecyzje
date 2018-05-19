using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        static string DirectUrlsAfter2016FileName = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\DirectUrls.txt";
        static string DirectUrlsBefore2016FileName = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\DirectUrls_before2016.txt";
        static string DirectUrlsBefore2016ComparedFileName = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\DirectUrls_compared_before2016.txt";
        static string DirectUrlsAfter2016ComparedFileName = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\DirectUrls_compared.txt";
        static string LogFile = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\LogFile.txt";

        static string ExisitingFilesHomeDir = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\txtmjnall";
        static string PDFFilesHomeDir = @"E:\Repra\test";

        static void Main(string[] args)
        {
            if (CheckAccesToFolders())
            {

                //DownloadDirectUrlsBefor2016();
                //DownloadDirectUrlsAfter2016();
                //CompareExistingFilesWithNewUrls();
                //var files = File.ReadAllLines(DirectUrlsAfter2016FileName);
                //var files2 = File.ReadAllLines(DirectUrlsAfter2016ComparedFileName);
                //var files3 = File.ReadAllLines(DirectUrlsBefore2016FileName);
                //var files4 = File.ReadAllLines(DirectUrlsBefore2016ComparedFileName);
                //DownloadFiles();
                //For downloading files use JDownloader http://jdownloader.org/download/index
                //// AfterDownload-->
                //ConvertCharsPLtoEN();
                //PutFilesInSubfolders();
                ////----OCR -> powershellscript
                //GetTextToHomeFolder();
                DeconvertCharsENtoPL();
            }
            else
            {
                Console.WriteLine("Bad file paths");
            }
            Console.WriteLine("Program finished its work. Press any key to finish");
            Console.ReadKey();
        }

        private static void ConvertCharsPLtoEN()
        {
            var filepaths = Directory.GetFiles(PDFFilesHomeDir);
            foreach (var filepath in filepaths)
            {
                var filename = Path.GetFileName(filepath);
                filename = filename.Replace("ą", "aX").Replace("Ą", "AX")
                                   .Replace("ć", "cX").Replace("Ć", "CX")
                                   .Replace("ę", "eX").Replace("Ę", "EX")
                                   .Replace("ł", "lX").Replace("Ł", "LX")
                                   .Replace("ń", "nX").Replace("Ń", "NX")
                                   .Replace("ó", "oX").Replace("Ó", "OX")
                                   .Replace("ż", "zZ").Replace("Ż", "ZZ")
                                   .Replace("ź", "zX").Replace("Ź", "ZX");
                File.Move(filepath, Path.Combine(PDFFilesHomeDir, filename));
            }
        }

        private static void DeconvertCharsENtoPL()
        {
            var filepaths = Directory.GetFiles(PDFFilesHomeDir);
            foreach (var filepath in filepaths)
            {
                var filename = Path.GetFileName(filepath);
                filename = filename.Replace("aX", "ą").Replace("AX", "Ą")
                                   .Replace("cX", "ć").Replace("CX", "Ć")
                                   .Replace("eX", "ę").Replace("EX", "Ę")
                                   .Replace("lX", "ł").Replace("LX", "Ł")
                                   .Replace("nX", "ń").Replace("NX", "Ń")
                                   .Replace("oX", "ó").Replace("OX", "Ó")
                                   .Replace("zZ", "ż").Replace("ZZ", "Ż")
                                   .Replace("zX", "ź").Replace("ZX", "Ź");
                File.Move(filepath, Path.Combine(PDFFilesHomeDir, filename));
            }
        }

        private static void PutFilesInSubfolders()
        {
            var filepaths = Directory.GetFiles(PDFFilesHomeDir).Where(x => x.Contains(".pdf"));
            foreach (var filepath in filepaths)
            {
                var filename = Path.GetFileNameWithoutExtension(filepath);
                Directory.CreateDirectory(Path.Combine(PDFFilesHomeDir, filename));
                File.Move(filepath, Path.Combine(PDFFilesHomeDir, filename, filename + ".pdf"));
            }
        }
        private static void GetTextToHomeFolder()
        {
            var directory = Directory.GetDirectories(PDFFilesHomeDir);
            foreach (var dir in directory)
            {
                var files = Directory.GetFiles(dir).Where(x => x.Contains(".txt.txt"));
                StringBuilder sb = new StringBuilder();
                foreach (var file in files)
                {
                    sb.Append(File.ReadAllText(file));
                }
                File.WriteAllText(Path.Combine(PDFFilesHomeDir, dir.Split('/')[dir.Split('/').Length - 1] + ".txt"), sb.ToString());
            }
        }

        private static void CompareExistingFilesWithNewUrls()
        {
            using (StreamWriter sw = new StreamWriter(File.Open(DirectUrlsAfter2016ComparedFileName, FileMode.Create)))
            {
                var newUrls = File.ReadAllLines(DirectUrlsAfter2016FileName);
                var oldFiles = Directory.GetFiles(ExisitingFilesHomeDir + "\\mjn2016txtonly").Select(x => Path.GetFileNameWithoutExtension(x));

                foreach (var url in newUrls)
                {
                    if (!oldFiles.Contains(Path.GetFileNameWithoutExtension(url)))
                    {
                        sw.WriteLine(url);
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(File.Open(DirectUrlsBefore2016ComparedFileName, FileMode.Create)))
            {
                var newUrls = File.ReadAllLines(DirectUrlsBefore2016FileName);
                var oldFiles = Directory.GetFiles(ExisitingFilesHomeDir + "\\mjntxtonly").Select(x => Path.GetFileNameWithoutExtension(x));
                foreach (var url in newUrls)
                {
                    if (!oldFiles.Contains(Path.GetFileNameWithoutExtension(url)))
                    {
                        sw.WriteLine(url);
                    }
                }
            }
        }

        private static void AddLog(string message)
        {
            using (StreamWriter sw = File.AppendText(LogFile))
            {
                sw.WriteLine(message);
            }
        }

        private static void DownloadDirectUrlsAfter2016()
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

            using (StreamWriter sw = new StreamWriter(File.Open(DirectUrlsAfter2016FileName, FileMode.Create), Encoding.Unicode))
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
        private static void DownloadDirectUrlsBefor2016()
        {
            List<string> linksToPages = new List<string>();
            HtmlDocument page = new HtmlDocument();
            WebClient client = new WebClient();
            for (int i = 1; i < 210; i++)
            {
                var pageString = client.DownloadString(UrlDecisonsBefore2016 + "?page=" + i);
                page.LoadHtml(pageString);
                var nodes = page.DocumentNode.SelectNodes("//a[@href]");
                foreach (var node in nodes)
                {
                    var nodevalue = node.Attributes["href"].Value;
                    if (nodevalue.Contains("SD/decyzje_1998_2016/20") && !nodevalue.Contains("default.htm"))
                    {
                        linksToPages.Add(nodevalue);
                    }
                }
            }
            var domain = "https://bip.warszawa.pl";

            using (StreamWriter sw = new StreamWriter(File.Open(DirectUrlsBefore2016FileName, FileMode.Create), Encoding.Unicode))
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
                if (!File.Exists(DirectUrlsAfter2016FileName))
                {
                    File.Create(DirectUrlsAfter2016FileName);
                }
                else
                {
                    File.OpenRead(DirectUrlsAfter2016FileName).Close();
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
