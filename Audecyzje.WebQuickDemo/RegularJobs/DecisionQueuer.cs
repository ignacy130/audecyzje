using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using HtmlAgilityPack;

using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Audecyzje.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace Audecyzje.WebQuickDemo.RegularJobs
{
    public class DecisionQueuer
    {
        IDecisionsService _decisionsService;
        public DecisionQueuer(IDecisionsService documentService)
        {
            _decisionsService = documentService;
        }

        static string UrlDecisonsAfter2016 = @"https://bip.warszawa.pl/Menu_podmiotowe/biura_urzedu/SD/decyzje/default.htm";
        static string UrlDecisonsBefore2016 = @"https://bip.warszawa.pl/Menu_podmiotowe/biura_urzedu/SD/decyzje_1998_2016/default.htm";

        static string DirectUrlsAfter2016FileName = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\DirectUrls.txt";
        static string DirectUrlsBefore2016FileName = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\DirectUrls_before2016.txt";
        static string DirectUrlsBefore2016ComparedFileName = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\DirectUrls_compared_before2016.txt";
        static string DirectUrlsAfter2016ComparedFileName = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\DirectUrls_compared.txt";
        static string LogFile = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\LogFile.txt";

        static string ExisitingFilesHomeDir = @"C:\Users\ya\Documents\SourceCode\audecyzje\Audecyzje.WebCrawler\Resources\txtmjnall";
        static string PDFFilesHomeDir = @"E:\Repra";

        /// <summary>
        /// <para>This method should start every night to compare differences between current db content and available decisons. </para>
        /// <para>It won't start if there are still queued items. </para>
        /// </summary>

        public async void CheckForNewDecisions()
        {
            //explanation: we assume that when you load decision it should be queued to parse. 
            //             if parsing hasn't finished we wait until queuing a new one. 
            //IMPORTANT: on error while parsing decision we should put in content some data informing that there was an error and we need to repeat the process
            //           (repeat will be done automatically if we simply remove the whole row and let the worker try to parse again

            var decisions = await _decisionsService.GetAll();
            bool stillWorking = decisions.Any(x => string.IsNullOrEmpty(x.Content));
            if (!stillWorking)
            {
                List<string> urlsAfter2016 = DownloadDirectUrlsAfter2016();
                List<string> urlsBefore2016 = DownloadDirectUrlsBefor2016();

                var newUrls = urlsAfter2016.Concat(urlsBefore2016);
                var existingUrls = decisions.Select(x => x.SourceLink);
                var urlsToDownload = newUrls.Except(existingUrls);

                foreach (var url in urlsToDownload)
                {
                    _decisionsService.AddNewDecision(new Infrastructure.Dtos.DecisionDto()
                    {
                        SourceLink = url
                    });
                }
            }
        }

        #region CopiedFromCrawler-NeedsWork
        private static void TranslateAllNestedDirsAndFileENtoPL(string workingDirectory)
        {
            foreach (var dir in Directory.GetDirectories(workingDirectory))
            {
                var dirname = Path.GetFileNameWithoutExtension(dir);
                var newName = GetStringENtoPL(dirname);
                if (newName != dirname)
                {
                    Directory.Move(dir, Path.Combine(Path.GetDirectoryName(dir), newName));
                }
            }

            foreach (var dir in Directory.GetDirectories(workingDirectory))
            {
                foreach (var filepath in Directory.GetFiles(dir))
                {
                    var filename = Path.GetFileName(filepath);
                    var newName = GetStringENtoPL(filename);
                    if (newName != filename)
                    {
                        File.Move(filepath, Path.Combine(dir, newName));
                    }
                }
            }
        }

        private static string GetStringPLtoEN(string toReplace)
        {
            return toReplace.Replace("ą", "aX").Replace("Ą", "AX")
                             .Replace("ć", "cX").Replace("Ć", "CX")
                             .Replace("ę", "eX").Replace("Ę", "EX")
                             .Replace("ł", "lX").Replace("Ł", "LX")
                             .Replace("ń", "nX").Replace("Ń", "NX")
                             .Replace("ó", "oX").Replace("Ó", "OX")
                             .Replace("ś", "sX").Replace("Ś", "SX")
                             .Replace("ż", "zZ").Replace("Ż", "ZZ")
                             .Replace("ź", "zX").Replace("Ź", "ZX");
        }
        private static string GetStringENtoPL(string toReplace)
        {
            return toReplace.Replace("aX", "ą").Replace("AX", "Ą")
                            .Replace("cX", "ć").Replace("CX", "Ć")
                            .Replace("eX", "ę").Replace("EX", "Ę")
                            .Replace("lX", "ł").Replace("LX", "Ł")
                            .Replace("nX", "ń").Replace("NX", "Ń")
                            .Replace("oX", "ó").Replace("OX", "Ó")
                            .Replace("sX", "ś").Replace("SX", "Ś")
                            .Replace("zZ", "ż").Replace("ZZ", "Ż")
                            .Replace("zX", "ź").Replace("ZX", "Ź");
        }

        private static void TranslateFilepathsPLtoEN()
        {
            var filepaths = Directory.GetFiles(PDFFilesHomeDir);
            foreach (var filepath in filepaths)
            {
                var filename = Path.GetFileName(filepath);
                filename = GetStringPLtoEN(filename);
                File.Move(filepath, Path.Combine(PDFFilesHomeDir, filename));
            }
        }

        private static void TranslateFilepathsENtoPL()
        {
            var filepaths = Directory.GetFiles(PDFFilesHomeDir);
            foreach (var filepath in filepaths)
            {
                var filename = Path.GetFileName(filepath);
                filename = GetStringENtoPL(filename);
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

        private static List<string> DownloadDirectUrlsAfter2016()
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

            //TODO remove streamwriter when tested link to pages return CAREFUL - strings might need to be decoded with httputility since they are stored xml nodes
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
            return linksToPages;
        }
        private static List<string> DownloadDirectUrlsBefor2016()
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

            //TODO remove streamwriter when tested link to pages return CAREFUL - strings might need to be decoded with httputility since they are stored xml nodes
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
            return linksToPages;
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
        #endregion
    }
}
