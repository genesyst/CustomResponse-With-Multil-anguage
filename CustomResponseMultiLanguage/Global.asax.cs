using CustomResponseMultiLanguage.serviceLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CustomResponseMultiLanguage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //custom with filter
            GlobalFilters.Filters.Add(new CustomResponseView());
        }

        protected void Application_BeginRequest()
        {
            //verify local language
            string userLanguages = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;

            if (!string.IsNullOrEmpty(userLanguages))
            {
                string preferredLanguage = userLanguages.Split(',').FirstOrDefault();
                string cultureName = GetCultureByLanguage(preferredLanguage);

                // Set the culture for the application
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            }
        }

        private string GetCultureByLanguage(string language)
        {
            // Define the mapping between languages and cultures
            var languageToCultureMap = new Dictionary<string, string>
            {
                { "en", "en-US" },
                { "th", "th-TH" },
                { "ch", "ch-CH" },
                { "lo", "lo-LA" },
                // Add more mappings as needed
            };

            if (languageToCultureMap.TryGetValue(language, out var culture))
            {
                return culture;
            }

            return "en-US"; // Default to English if the language is not in the map
        }
    }
}
