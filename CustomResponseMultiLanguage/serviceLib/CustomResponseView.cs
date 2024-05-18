using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CustomResponseMultiLanguage.serviceLib
{
    public class CustomResponseView : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;

            // Swap the response output with a temporary StringWriter
            var originalWriter = response.Output;
            var stringWriter = new StringWriter();
            response.Output = stringWriter;

            // Save the original writer to the HttpContext for later use
            filterContext.HttpContext.Items["originalWriter"] = originalWriter;
            filterContext.HttpContext.Items["stringWriter"] = stringWriter;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            HttpCookie cookie = filterContext.HttpContext.Request.Cookies["SelectedLanguage"];
            string cultureName = cookie != null ? cookie.Value : Thread.CurrentThread.CurrentUICulture.Name;//"en-US";

            var response = filterContext.HttpContext.Response;

            // Retrieve the original writer and the StringWriter
            var originalWriter = filterContext.HttpContext.Items["originalWriter"] as TextWriter;
            var stringWriter = filterContext.HttpContext.Items["stringWriter"] as StringWriter;

            // Get the rendered content from the StringWriter
            var renderedContent = stringWriter.ToString();

            // Modify the content

            Dictionary<string, string> langs = new serviceLanguage().LoadLngDict(cultureName);
            foreach(var lan in langs)
                renderedContent = renderedContent.Replace(lan.Key, lan.Value);

            var modifiedContent = renderedContent;

            // Write the modified content to the original writer
            originalWriter.Write(modifiedContent);

            // Restore the original writer to the response
            response.Output = originalWriter;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

            base.OnResultExecuted(filterContext);
        }
    }
}