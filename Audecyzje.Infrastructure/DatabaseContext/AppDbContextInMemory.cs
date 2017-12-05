using System;
using Audecyzje.Core.Domain;

namespace Audecyzje.Infrastructure.DatabaseContext
{
    public static class AppDbContextInMemory
    {
        public static async void Seed(AppDbContext context)
        {
            var localizationOne = new Localization()
            {
                Id = 1,
                City = "Warszawa",
                Number = "5",
                PostalCode = "01-476",
                Street = "Nowogrodzka"
            };
            context.Localizations.Add(localizationOne);
            var documentOne = new Document()
            {
                Localization = "lokalizacja",
                Content = "tresc dokumentu",
                Date = DateTime.Today,
                LegalBasis = "Podstawy prawne",
                DecisionNumber = "1234",
                SubmissionDate = DateTime.Today,
            };
            context.Documents.Add(documentOne);
            context.Persons.Add(new Person()
            {
                Id = 1,
                FirstName = "Michal",
                Function = "Przewodniczacy",
                LastName = "Kowalski",
                Supervisor = "Prezydent m.s Warszawa"
            });
            context.Persons.Add(new Person()
            {
                Id = 2,
                FirstName = "Dariusz",
                Function = "Zastepca",
                LastName = "Nowak",
                Supervisor = "Przewodniczący Michal Kowalski"
            });
            context.Decisions.Add(new Decision()
            {
                Content = "tresc ",
                Id = 1,
                IsApproved = true
            });
            await context.SaveChangesAsync();
        }
    }
}
