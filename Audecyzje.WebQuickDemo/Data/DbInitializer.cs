using Audecyzje.WebQuickDemo.Models;
using Microsoft.ApplicationInsights.AspNetCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Audecyzje.WebQuickDemo.Data
{
    public class DbInitializer
    {
        public static void Initialize(WarsawContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Tags.Any())
            {
                Tag tag = new Tag()
                {
                    TagName = "Czy jest kurator spadku",
                    RegExp = "kurator"
                };
                Tag tag2 = new Tag()
                {
                    TagName = "Czy jest kurator spadku",
                    RegExp = "otrzymuj"
                };
                Tag tag3 = new Tag()
                {
                    TagName = "Kolejny wniosek do sprawy",
                    RegExp = "po rozpatrzeniu wniosku z dnia"
                };
                context.Add(tag);
                context.Add(tag2);
                context.Add(tag3);
                context.SaveChanges();
            }

            //LoadFilesToDb(context);
        }
        static void LoadFilesToDb(WarsawContext context)
        {
            var projectPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.FullName;
            var resourcesFolder = Path.Combine(projectPath, "Resources");
            var mjnallFolder = @"E:\mjntestypars";// Path.Combine(resourcesFolder, "txtmjnall");
            if (Directory.Exists(mjnallFolder))
            {
                LoadBefore2016(context, mjnallFolder);

                //LoadAfter2016(context, mjnallFolder);
            }
        }
        private static void LoadBefore2016(WarsawContext context, string mjnallFolder)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var filePaths = Directory.GetFiles(Path.Combine(mjnallFolder, "mjntxtonly"));
            DateTime uploadedDate = DateTime.Now;
            foreach (var filePath in filePaths)
            {
                var filename = Path.GetFileName(filePath);
                var GK = filename.Contains("GK");
                var CRWIP = filename.Contains("CRWIP") || filename.Contains("CRWiP");
                var CRWIPbis = filename.Contains("CWRiP");// w minimum jednym pliku literówka...
                var fileContent = File.ReadAllText(filePath);

                //czasem konczy sie jakimś numeren identyfikacyjnym więc :
                var s2689 = filename.Contains("2689");
                var s2889 = filename.Contains("2889");
                var s3490 = filename.Contains("3490");
                var s2733 = filename.Contains("2733");
                var randomEnding = s2689 || s2889 || s3490 || s2733;

                string decisionNumber = "Failed to parse decision number";

                if (GK)
                {
                    var submissionDate = new DateTime();
                    var datestring = string.Empty;
                    var streetFull = "Failed to parse street";
                    var indexGK = filename.IndexOf("GK");
                    try
                    {
                        //Parsowanie numeru dla GK lub GKDW
                        var decisonNumberStartIndex = 0;
                        var decisonNumberEndIndex = indexGK + 4;
                        if (filename.Contains("GKDW"))
                        {
                            decisonNumberEndIndex += 2;
                        }
                        decisionNumber = filename.Substring(decisonNumberStartIndex, decisonNumberEndIndex - decisonNumberStartIndex);

                        //Parsowanie ulicy z nazwy
                        var streetStartIndex = indexGK + 4 + 8 + 4;//pomijam gkdw, date, i powtorzony rok 
                        var streetEndIndex = filename.IndexOf('.'); // jeśli nic sie nie dzieje to do kropki
                        if (CRWIP)
                        {
                            streetEndIndex = filename.IndexOf("CRW");
                        }
                        if (CRWIPbis)
                        {
                            streetEndIndex = filename.IndexOf("CWR");
                        }
                        if (randomEnding && !(CRWIP || CRWIPbis))
                        {
                            streetEndIndex = streetEndIndex - 4;//cofamy od kropki 4 wstecz
                        }
                        streetFull = filename.Substring(streetStartIndex, streetEndIndex - streetStartIndex);
                        datestring = filename.Substring(indexGK + 4, 8);
                        submissionDate = DateTime.ParseExact(datestring, "yyyyddMM", provider);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        Localization loc = new Localization()
                        {
                            Street = streetFull
                        };
                        Decision dec = new Decision()
                        {
                            Localizations = new List<Localization>() { loc },
                            SubmissionDate = submissionDate,
                            DecisionNumber = decisionNumber,
                            Content = fileContent,
                            UploadedTime = uploadedDate
                        };
                        context.Add(dec);
                        context.SaveChanges();
                    }
                }
                else
                {
                    //Pozostałe decyzje
                    decisionNumber = "impossible to parse dec number";
                    string streetFull = "Failed to parse street";
                    int streetStartIndex = IndexOfFirstLetter(filename);

                    var datestring = filename.Substring(streetStartIndex - 8, 8);
                    DateTime submissionDate = new DateTime();
                    try
                    {
                        var streetEndIndex = filename.IndexOf('.'); // jeśli nic sie nie dzieje to do kropki
                        if (CRWIP)
                        {
                            streetEndIndex = filename.IndexOf("CRW");
                        }
                        if (CRWIPbis)
                        {
                            streetEndIndex = filename.IndexOf("CWR");
                        }
                        if (randomEnding && !(CRWIP || CRWIPbis))
                        {
                            streetEndIndex = streetEndIndex - 4;//cofamy od kropki 4 wstecz
                        }

                        streetFull = filename.Substring(streetStartIndex, streetEndIndex - streetStartIndex);
                        submissionDate = DateTime.ParseExact(datestring, "ddMMyyyy", provider);
                    }
                    finally
                    {
                        Localization loc = new Localization()
                        {
                            Street = streetFull
                        };
                        Decision dec = new Decision()
                        {
                            Localizations = new List<Localization>() { loc },
                            SubmissionDate = submissionDate,
                            DecisionNumber = decisionNumber,
                            Content = fileContent,
                            UploadedTime = uploadedDate
                        };
                        context.Add(dec);
                        context.SaveChanges();
                    }
                }
            }
        }

        private static int IndexOfFirstLetter(string filename)
        {
            var pos = -1;
            var i = 0;
            while (pos == -1)
            {
                if (!Char.IsDigit(filename[i]))
                {
                    pos = i;
                }
                i++;
            }
            return pos;
        }

        private static void LoadAfter2016(WarsawContext context, string mjnallFolder)
        {

            CultureInfo provider = CultureInfo.InvariantCulture;
            var filePaths2016 = Directory.GetFiles(Path.Combine(mjnallFolder, "mjn2016txtonly"));
            foreach (var filePath in filePaths2016)
            {
                var filename = Path.GetFileName(filePath);
                var fileContent = File.ReadAllText(filePath);

                var submissionDate = new DateTime();
                var datestring = string.Empty;
                var streetFull = "No street in file name";
                var decisionNumber = "decison name/number";
                try
                {
                    datestring = filename.Substring(0, 8);
                    submissionDate = DateTime.ParseExact(datestring, "ddMMyyyy", provider);
                    var endindex = filename.IndexOf('.');
                    decisionNumber = filename.Substring(8, endindex - 8).Replace("janr", "ja_nr").Replace("nr", "nr_");//TODO - decide how we should keep names in db
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Localization loc = new Localization()
                    {
                        Street = streetFull
                    };
                    Decision dec = new Decision()
                    {
                        Localizations = new List<Localization>() { loc },
                        SubmissionDate = submissionDate,
                        Content = fileContent,
                        DecisionNumber = decisionNumber

                    };
                    context.Add(dec);
                    context.SaveChanges();
                }
            }
        }

    }

}

