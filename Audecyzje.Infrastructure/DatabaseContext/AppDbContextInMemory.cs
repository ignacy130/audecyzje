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
            //context.Persons.Add(new Person()
            //{
            //    Id = 1,
            //    FirstName = "Michal",
            //    Function = "Przewodniczacy",
            //    LastName = "Kowalski",
            //    Supervisor = "Prezydent m.s Warszawa"
            //});
            //context.Persons.Add(new Person()
            //{
            //    Id = 2,
            //    FirstName = "Dariusz",
            //    Function = "Zastepca",
            //    LastName = "Nowak",
            //    Supervisor = "Przewodniczący Michal Kowalski"
            //});
            await context.SaveChangesAsync();
        }
    }
}
