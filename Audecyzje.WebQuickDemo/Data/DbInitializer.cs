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
            if (!context.Descisions.Any())
            {
                Localization loc = new Localization()
                {
                    Latitude = 20.9945904,
                    Longitude = 52.2330803,
                    Street = "Initial Warsaw"
                };
                Tag tag = new Tag()
                {
                    TagName = "Initial Tag"
                };
                Decision dec = new Decision()
                {
                    Content = "Initial decision",
                    Localizations = new List<Localization>() { loc },

                };
                DecisionTag dg = new DecisionTag()
                {
                    Tag = tag,
                    Decision = dec
                };
                context.Add(dec);
                context.Add(dg);
                context.SaveChanges();
            }

            //LoadFilesToDb(context);
        }
        static void LoadFilesToDb(WarsawContext context)
        {
            var projectPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.FullName;
            var resourcesFolder = Path.Combine(projectPath, "Resources");
            var mjnallFolder = Path.Combine(resourcesFolder, "txtmjnall");
            if (Directory.Exists(mjnallFolder))
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var filePaths = Directory.GetFiles(Path.Combine(mjnallFolder, "mjntxtonly"));
                foreach (var filePath in filePaths)
                {
                    var filename = Path.GetFileName(filePath);
                    var GKDW = filename.Contains("GKDW");
                    var CRWIP = filename.Contains("CRWIP") || filename.Contains("CRWiP");
                    var CRWIPbis = filename.Contains("CWRiP");// w minimum jednym pliku literówka...
                    var fileContent = File.ReadAllText(filePath);

                    //czasem konczy sie jakimś numeren identyfikacyjnym więc :
                    var s2689 = filename.Contains("2689");
                    var s2889 = filename.Contains("2889");
                    var s3490 = filename.Contains("3490");
                    var s2733 = filename.Contains("2733");
                    var randomEnding = s2689 || s2889 || s3490 || s2733;

                    if (GKDW)
                    {
                        var submissionDate = new DateTime();
                        var datestring = string.Empty;
                        var streetFull = "Failed to parse street";
                        try
                        {
                            var ind = filename.IndexOf("GKDW");
                            
                            var streetstartIndex = ind + 4 + 8 + 4;//pomijam gkdw, date, i powtorzony rok 
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
                            streetFull = filename.Substring(streetstartIndex, streetEndIndex - streetstartIndex);
                            datestring = filename.Substring(ind + 4, 8);
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
                                Content = fileContent
                            };
                            context.Add(dec);
                            context.SaveChanges();
                        }     

                    }
                    if (!GKDW)
                    {
                        //przypadek do zrobienia "odtylu"
                    }
                }
            }
        }
    }

}

