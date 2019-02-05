using System;
using System.Collections.Generic;
using Audecyzje.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Audecyzje.Infrastructure.DatabaseContext
{
    public static class AppDbContextInMemory
    {
        public static void Seed(WarsawContext context)
        {
            AddDecisions(context);
            AddPosts(context);

        }

        private static void AddPosts(WarsawContext context)
        {
            var post = new Post()
            {
                Id = 1,
                Title = "Bitwa pod Aguere",
                AuthorId = Guid.NewGuid().ToString(),
                Content = "Bitwa pod Aguere – bitwa stoczona 14 listopada 1494 r. pomiędzy Kastylijczykami (1000 ludzi) i Guanczami (ok. 5000 ludzi), jeden z epizodów podboju Wysp Kanaryjskich[1].",
                CreatedAt = DateTime.Now,
                IsPublished = true,
                ModifiedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                ParentId = -1,
            };

            

            var post2 = new Post()
            {
                Id = 2,
                AuthorId = Guid.NewGuid().ToString(),
                Title = "Tło",
                Content = "Alonso Luis Fernández de Lugo rozpoczął podbój Teneryfy 1 maja 1494 r., po kilku wcześniejszych, nieudanych próbach podporządkowania wyspy Kastylii. Teneryfa była wówczas ostatnią wyspą archipelagu Wysp Kanaryjskich, która opierała się europejskiej inwazji od czasu, gdy Lugo opanował Gran Canarię i La Palmę. Wyspa była wówczas podzielona pomiędzy 9 królestw rządzonych przez osobnych wodzów, z czego 4 zdecydowało się na współpracę z najeźdźcami[2].",

                CreatedAt = DateTime.Now,
                IsPublished = true,
                ModifiedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                ParentId = post.Id,
            };

            

            var post3 = new Post()
            {
                Id = 3,
                AuthorId = Guid.NewGuid().ToString(),
                Title = "Bitwa i następstwa",
                Content = "Lugo przeżył bitwę[3], wycofał się na Gran Canarię i powrócił na wyspę[2] z silniejszym oddziałem i stoczył z tubylcami bitwę pod Aguere, na otwartym terenie, gdzie mógł wykorzystać swoją przewagę techniczną. Europejczycy rozpoczęli bitwę od salwy artyleryjskiej, a następnie nastąpiła szarża kawalerii ubezpieczanej przez piechotę. Efektem starcia było zdecydowane zwycięstwo Kastylijczyków[3].",
                CreatedAt = DateTime.Now,
                IsPublished = true,
                ModifiedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                ParentId = post.Id,
            };

            context.Posts.Add(post);
            context.Posts.Add(post2);
            context.Posts.Add(post3);
        }

        private static void AddDecisions(WarsawContext context)
        {
            for (int i = 100; i < 110; i++)
            {
                var documentZero = new Decision()
                {
                    Id = i,
                    Localizations = new List<Localization>(),
                    Content = "tresc dokumentu",
                    Date = DateTime.Today,
                    LegalBasis = "Podstawy prawne",
                    DecisionNumber = "13/GK/DW/1007",
                    SubmissionDate = DateTime.Today,
                    Street = "ul. Marszałkowska 120",
                    Address = "ul. Marszałkowska 120",
                };

                context.Decisions.Add(documentZero);
            }

            var documentOne = new Decision()
            {
                Id = 1,
                Localizations = new List<Localization>(),
                Content = "tresc dokumentu",
                Date = DateTime.Today,
                LegalBasis = "Podstawy prawne",
                DecisionNumber = "13/GK/DW/1007",
                SubmissionDate = DateTime.Today,
                Street = "ul. Stalowa 22",
                Address = "ul. Stalowa 22",
            };
            
            context.Decisions.Add(documentOne);
            var localizationOne = new Localization()
            {
                Id = 1,
                City = "Warszawa",
                Number = "5",
                PostalCode = "01-476",
                Street = "ul. Nowogrodzka 22, Warszawa",
                DocumentId = 1
            };
            context.Localizations.Add(localizationOne);


            var localizationTwo = new Localization()
            {
                Id = 2,
                City = "Warszawa",
                Number = "6",
                PostalCode = "01-476",
                Street = "Nowogrodzka 44",
                DocumentId = 1
            };
            
            documentOne.Localizations.Add(localizationTwo);

            var localizationThree = new Localization()
            {
                Id = 3,
                City = "Warszawa",
                Number = "6",
                PostalCode = "01-476",
                Street = "Niepodleglości 12",
                DocumentId = 1
            };
            
            documentOne.Localizations.Add(localizationThree);

            var documentTwo = new Decision()
            {
                Id = 2,
                Localization = "lokalizacja druga",
                Localizations = new List<Localization>(),
                Content = "tresc dokumentu",
                Date = DateTime.Today,
                LegalBasis = "Podstawy prawne",
                DecisionNumber = "17/GK/DW/2007",
                SubmissionDate = DateTime.Today,

            };
            context.Decisions.Add(documentTwo);
            var localizationFour = new Localization()
            {
                Id = 4,
                City = "Warszawa",
                Number = "5",
                PostalCode = "01-476",
                Street = "Niepodleglosci",
                DocumentId = 2
            };
            
            documentOne.Localizations.Add(localizationFour);
        }
    }
}
