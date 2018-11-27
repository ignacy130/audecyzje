using System;
using System.Collections.Generic;
using Audecyzje.Core.Domain;

namespace Audecyzje.Infrastructure.DatabaseContext
{
    public static class AppDbContextInMemory
    {
        public static async void Seed(WarsawContext context)
        {
            var documentOne = new Decision()
            {
                Localization = "Nowogrodzka 22",
                Localizations = new List<Localization>(),
                Content = "tresc dokumentu",
                Date = DateTime.Today,
                LegalBasis = "Podstawy prawne",
                DecisionNumber = "13/GK/DW/1007",
                SubmissionDate = DateTime.Today,

            };
            context.Decisions.Add(documentOne);
            var localizationOne = new Localization()
            {
                Id = 1,
                City = "Warszawa",
                Number = "5",
                PostalCode = "01-476",
                Street = "Nowogrodzka 22, Warszawa"
            };
            context.Localizations.Add(localizationOne);
            documentOne.Localizations.Add(localizationOne);
            localizationOne = new Localization()
            {
                Id = 2,
                City = "Warszawa",
                Number = "6",
                PostalCode = "01-476",
                Street = "Nowogrodzka 44"
            };
            context.Localizations.Add(localizationOne);
            documentOne.Localizations.Add(localizationOne);

            localizationOne = new Localization()
            {
                Id = 3,
                City = "Warszawa",
                Number = "6",
                PostalCode = "01-476",
                Street = "Niepodleglości 12"
            };
            context.Localizations.Add(localizationOne);
            documentOne.Localizations.Add(localizationOne);
            documentOne = new Decision()
            {
                Localization = "lokalizacja druga",
                Localizations = new List<Localization>(),
                Content = "tresc dokumentu",
                Date = DateTime.Today,
                LegalBasis = "Podstawy prawne",
                DecisionNumber = "17/GK/DW/2007",
                SubmissionDate = DateTime.Today,

            };
            context.Decisions.Add(documentOne);
            localizationOne = new Localization()
            {
                Id = 4,
                City = "Warszawa",
                Number = "5",
                PostalCode = "01-476",
                Street = "Niepodleglosci"
            };
            context.Localizations.Add(localizationOne);
            documentOne.Localizations.Add(localizationOne);

			var post = context.Posts.Add(new Post()
			{
				AuthorId = Guid.NewGuid().ToString(),
				Content = "Lorem ipsum dolor sit amet, hinc apeirian ad pri. Rebum molestie qualisque quo at, exerci moderatius dissentias pro no. Et magna partem complectitur quo, ut quo suas nostrud. Id qui erat brute prompta, mea admodum nostrum id, ad sed elit saperet voluptatum. Eum ocurreret assueverit ad. Sit modus nullam possim ei, pro erant scripserit no.",
				CreatedAt = DateTime.Now,
				IsPublished = true,
				ModifiedAt=DateTime.Now,
				PublishedAt = DateTime.Now,
				Title = "TOP Lorem ipsum dolot sit amet",
				ParentId = -1,
			});

			context.Posts.Add(new Post()
			{
				AuthorId = Guid.NewGuid().ToString(),
				Content = "Lorem ipsum dolor sit amet, hinc apeirian ad pri. Rebum molestie qualisque quo at, exerci moderatius dissentias pro no. Et magna partem complectitur quo, ut quo suas nostrud. Id qui erat brute prompta, mea admodum nostrum id, ad sed elit saperet voluptatum. Eum ocurreret assueverit ad. Sit modus nullam possim ei, pro erant scripserit no.",
				CreatedAt = DateTime.Now,
				IsPublished = true,
				ModifiedAt = DateTime.Now,
				PublishedAt = DateTime.Now,
				Title = "CHILD Lorem ipsum dolot sit amet",
				ParentId = post.Entity.Id,
			});

			context.Posts.Add(new Post()
			{
				AuthorId = Guid.NewGuid().ToString(),
				Content = "Lorem ipsum dolor sit amet, hinc apeirian ad pri. Rebum molestie qualisque quo at, exerci moderatius dissentias pro no. Et magna partem complectitur quo, ut quo suas nostrud. Id qui erat brute prompta, mea admodum nostrum id, ad sed elit saperet voluptatum. Eum ocurreret assueverit ad. Sit modus nullam possim ei, pro erant scripserit no.",
				CreatedAt = DateTime.Now,
				IsPublished = true,
				ModifiedAt = DateTime.Now,
				PublishedAt = DateTime.Now,
				Title = "CHILD Lorem ipsum dolot sit amet",
				ParentId = post.Entity.Id,
			});

			await context.SaveChangesAsync();
        }
    }
}
