using Audecyzje.Infrastructure.Dtos;
using Audecyzje.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audecyzje.WebQuickDemo.RegularJobs
{
    public class DecisionParser
    {
        IDecisionsService _decisionsService;
        public DecisionParser(IDecisionsService documentService)
        {
            _decisionsService = documentService;
        }

        public async void RecognizeContentFromFile()
        {
            var decision = await _decisionsService.GetFirstUnparsedDecisionNotCachedRepository();
            var file = DownloadFile(decision.SourceLink);
            DecisionDto decisionWithParsedParameters = GOTODbInitializer(file);
            string content = RunOCRInMemoryToGenerateContent(file);


        }

        private string RunOCRInMemoryToGenerateContent(object file)
        {
            throw new NotImplementedException();
        }

        private DecisionDto GOTODbInitializer(object file)
        {
            //TODO - trzeba przemyśleć jak parsować: wartości numer decyzji, nazwa ulicy i data, są zawarte w nazwie pliku, 
            //ale są już 3 algorytmy : 1.dla "przed 2016" 2.po 2016a 3.po2016b
            //Pomysł Automaty i gramatyka - ciąg znaków trzeba przepuścić żeby zdecydować który algorytm użyć -> czyli czy słowo należy do języka 2016 czy 2016b, do przegadania
            throw new NotImplementedException();
        }

        private object DownloadFile(string sourceLink)
        {
            //TODO previously was done by JDownloader
            throw new NotImplementedException();
        }
    }
}
