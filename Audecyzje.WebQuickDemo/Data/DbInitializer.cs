using Audecyzje.WebQuickDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Decision dec = new Decision()
                {
                    Content = "Initial decision"
                };
                context.Add(dec);
                context.SaveChanges();
            }

            if (!context.Localizations.Any())
            {
                Localization loc = new Localization()
                {
                    Latitude = 20.9945904,
                    Longitude = 52.2330803,
                    Street = "Initial Warsaw"
                };
                context.Add(loc);
                context.SaveChanges();
            }

            if (!context.Tags.Any())
            {
                Tag tag = new Tag()
                {
                    TagName = "Initial Tag"
                };
                context.Add(tag);
                context.SaveChanges();
            }
        }
    }

}

