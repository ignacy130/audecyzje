using Audecyzje.Core.Domain;
using Audecyzje.Infrastructure;
using Audecyzje.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure
{
	public class DbInitializer : IDisposable
	{
		private WarsawContext context;

		public DbInitializer(WarsawContext dbContext)
		{
			context = dbContext;
		}

		public void Initialize()
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

			if (!context.Decisions.Any())
			{
				LoadFilesToDb();
			}
		}

		void LoadFilesToDb()
		{
			var projectPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.FullName;
			var contentdirectory = Path.Combine(projectPath, "Resources");
			contentdirectory = @"D:\D\Projekty\KdP\audecyzje\audecyzje\Audecyzje.WebQuickDemo\Resources";
			if (Directory.Exists(contentdirectory))
			{
				LoadBefore2016(Path.Combine(contentdirectory, "txtmjnall_25052018"), true);
				//LoadAfter2016approach1(Path.Combine(contentdirectory, "txtmjnall_25052018"));

				//po pobraniu kolejnej paczki okazało się że nazwy trochę się pozmieniały -_-
				//LoadAfter2016approach2(contentdirectory);
			}
		}

		private void LoadBefore2016(string mjnallFolder, bool loadSmallSet = false)
		{
			var sourceLinks = File.ReadAllLines(Path.Combine(mjnallFolder, "DirectUrls_before2016.txt"));

			CultureInfo provider = CultureInfo.InvariantCulture;
			var filePaths = Directory.GetFiles(Path.Combine(mjnallFolder, "mjntxtonly"));

			if (loadSmallSet)
			{
				filePaths = filePaths.Take(500).ToArray();
			}

			DateTime uploadedDate = DateTime.Now;
			foreach (var filePath in filePaths)
			{
				var filename = Path.GetFileName(filePath);
				var sourceLink = sourceLinks.FirstOrDefault(x => x.Contains(Path.GetFileNameWithoutExtension(filePath)));
				var GKorDW = filename.Contains("GK") || filename.Contains("DW");
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

				if (GKorDW)
				{
					var submissionDate = new DateTime();
					var datestring = string.Empty;
					var streetFull = "";

					try
					{
						//Parsowanie numeru dla GK lub GKDW lub GNDW
						decisionNumber = ExtractGKorGKDWorGNDWDecisionNumber(filename);

						//Parsowanie ulicy z nazwy
						int streetStartIndex = 0;
						streetFull = ExtractStreetFull(filename, CRWIP, CRWIPbis, randomEnding, out streetStartIndex);

						datestring = filename.Substring(streetStartIndex, 8); //niespodzianka po pominieciu 8 znaków znajdujemy pierwsza litere ulicy a -8 daje index startu daty z filename
						submissionDate = DateTime.ParseExact(datestring, "ddMMyyyy", provider);
					}
					catch (FormatException fex)
					{
						try
						{
							//znowu jakiś debil nie wstawił zera
							datestring = "0" + datestring.Substring(1, 7);
							submissionDate = DateTime.ParseExact(datestring, "ddMMyyyy", provider);
						}
						catch (Exception ex)
						{
							//fuck it
						}
					}
					catch (Exception ex)
					{

					}
					finally
					{
						var loc = new Localization()
						{
							Street = streetFull
						};
						var dec = new Decision()
						{
							Localizations = new List<Localization>() { loc },
							SubmissionDate = submissionDate,
							DecisionNumber = decisionNumber,
							Content = fileContent,
							UploadedTime = uploadedDate,
							SourceLink = sourceLink,
							Address = streetFull
						};
						context.Add(dec);
						context.SaveChanges();
					}
				}
				//Pozostałe decyzje
				else
				{
					var submissionDate = new DateTime();
					var datestring = string.Empty;
					var streetFull = "";

					try
					{
						int streetStartIndex = 0;
						streetFull = ExtractOthersStreetFull(filename, CRWIP, CRWIPbis, randomEnding, out streetStartIndex);

						datestring = filename.Substring(streetStartIndex - 8, 8);
						submissionDate = DateTime.ParseExact(datestring, "ddMMyyyy", provider);
					}
					catch (FormatException fex)
					{
						try
						{
							//znowu jakiś debil nie wstawił zera
							datestring = "0" + datestring.Substring(1, 7);
							submissionDate = DateTime.ParseExact(datestring, "ddMMyyyy", provider);
						}
						catch (Exception ex)
						{
							//fuck it
						}
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
			}

			var streets = context.Decisions.Select(x => x.Address).ToList();
		}

		private static string ExtractOthersStreetFull(string filename, bool CRWIP, bool CRWIPbis, bool randomEnding, out int streetStartIndex)
		{
			string streetFull = "Failed to parse street";
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

			streetStartIndex = filename.IndexOfFirstLetter();
			streetFull = filename.Substring(streetStartIndex, streetEndIndex - streetStartIndex);
			streetFull = TransformToStreetFormat(streetFull);

			return streetFull;
		}

		private static string TransformToStreetFormat(string streetFull)
		{
			if (streetFull.StartsWith("ul"))
			{
				streetFull = $"ul. {streetFull.Substring("ul".Length)}";
			}

			if (streetFull.StartsWith("al"))
			{
				streetFull = $"al. {streetFull.Substring("al".Length)}";
			}

			if (streetFull.StartsWith("Al"))
			{
				streetFull = $"Al. {streetFull.Substring("Al".Length)}";
			}

			streetFull = streetFull.Replace("dawnaul", " dawna ul. ")
								   .Replace("iul", " i ul. ")
								   .Replace("rógul", " róg ul. ")
								   .Replace("dawniej", " dawniej ");

			var i = 0;
			var indexInDigitSeries = false;
			while (i < streetFull.Length)
			{
				if (Char.IsDigit(streetFull[i]) && !indexInDigitSeries)
				{
					indexInDigitSeries = true;
					streetFull = streetFull.Insert(i, " ");
				}
				else if(Char.IsLetter(streetFull[i]))
				{
					indexInDigitSeries = false;
				}
				i++;
			}

			streetFull = streetFull.Insert(streetFull.IndexOfFirstDigit(), " ")
								   .Trim()
								   .Replace("  ", " ");
			return streetFull;
		}

		private static string ExtractStreetFull(string filename, bool CRWIP, bool CRWIPbis, bool randomEnding, out int streetStartIndex)
		{
			var streetFull = "Failed to parse street";
			streetFull = filename.Substring(8, filename.Length - 8);//spokojnie 8 można pominąć
			streetStartIndex = streetFull.IndexOfFirstLetter();
			var streetEndIndex = streetFull.IndexOf('.'); // jeśli nic sie nie dzieje to do kropki
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

			streetFull = streetFull.Substring(streetStartIndex, streetEndIndex - streetStartIndex);

			streetFull = TransformToStreetFormat(streetFull);

			return streetFull;
		}

		private static string ExtractGKorGKDWorGNDWDecisionNumber(string filename)
		{
			var indexGK = filename.IndexOf("G");
			string decisionNumber;
			var decisonNumberStartIndex = 0;
			var decisonNumberEndIndex = indexGK + 2 + 4;
			if (filename.Contains("DW"))
			{
				decisonNumberEndIndex += 2;
			}
			decisionNumber = filename.Substring(decisonNumberStartIndex, decisonNumberEndIndex - decisonNumberStartIndex);
			return decisionNumber;
		}

		private void LoadAfter2016approach1(string mjnallFolder)
		{
			var sourceLinks = File.ReadAllLines(Path.Combine(mjnallFolder, "DirectUrls.txt"));

			DateTime uploadedDate = DateTime.Now;
			CultureInfo provider = CultureInfo.InvariantCulture;
			var filePaths2016 = Directory.GetFiles(Path.Combine(mjnallFolder, "mjn2016txtonly"));
			foreach (var filePath in filePaths2016)
			{
				var filename = Path.GetFileName(filePath);
				var fileContent = File.ReadAllText(filePath);
				var sourceLink = sourceLinks.Where(x => x.Contains(Path.GetFileNameWithoutExtension(filePath))).FirstOrDefault();

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
						DecisionNumber = decisionNumber,
						UploadedTime = uploadedDate,
						SourceLink = sourceLink
					};
					context.Add(dec);
					context.SaveChanges();
				}
			}
		}

		private void LoadAfter2016approach2(string mjnallFolder)
		{
			var sourceLinks = File.ReadAllLines(Path.Combine(mjnallFolder, "DirectUrls.txt"));
			DateTime uploadedDate = DateTime.Now;
			CultureInfo provider = CultureInfo.InvariantCulture;
			var filePaths2016 = Directory.GetFiles(Path.Combine(mjnallFolder, "mjn2016txtonly"));
			foreach (var filePath in filePaths2016)
			{
				var filename = Path.GetFileName(filePath);
				var fileContent = File.ReadAllText(filePath);
				var sourceLink = sourceLinks.Where(x => x.Contains(Path.GetFileNameWithoutExtension(filePath))).FirstOrDefault();

				var submissionDate = new DateTime();
				var datestring = string.Empty;
				var streetFull = "No street in file name";
				var decisionNumber = "decison name not provided";

				var DW = filename.Contains("DW");
				var SD = filename.Contains("SD");
				try
				{
					if (SD)
					{
						datestring = filename.Substring(filename.IndexOf("SD") + 6, 8);
						decisionNumber = filename.Substring(0, filename.IndexOf("SD") + 1 + 5);
					}
					else if (DW)
					{
						datestring = filename.Substring(filename.IndexOf("DW") + 6, 8);
						decisionNumber = filename.Substring(0, filename.IndexOf("DW") + 1 + 5);
					}
					else
					{
						datestring = filename.Substring(0, 8);
					}
					if (Char.IsLetter(datestring[7]))
					{
						//bo ktoś kurwa wpadł na pomysł żeby zapisać dzień bez 0 z przodu parę razy -_- kyrie eleison
						datestring = "0" + datestring.Substring(0, 7);
					}
					streetFull = filename.Substring(10, filename.Length - 11);//pierwsze 10 możemy spokojnie pominąc żeby nie przeszkadzały
					var streetStartIndex = streetFull.IndexOfFirstLetter();
					var streetEndIndex = streetFull.IndexOf('.');
					streetFull = streetFull.Substring(streetStartIndex, streetEndIndex - streetStartIndex - 1);
					submissionDate = DateTime.ParseExact(datestring, "ddMMyyyy", provider);
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
						DecisionNumber = decisionNumber,
						UploadedTime = uploadedDate,
						SourceLink = sourceLink
					};
					context.Add(dec);
					context.SaveChanges();
				}
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (context != null)
				{
					context.Dispose();
				}
			}
		}
	}

}

