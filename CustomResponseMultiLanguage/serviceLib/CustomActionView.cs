using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomResponseMultiLanguage.serviceLib
{
    public class CustomActionView : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is ViewResult viewResult)
            {
                // Determine the view name
                string viewName = viewResult.ViewName;

                // If ViewName is empty, use the action name as the view name
                if (string.IsNullOrEmpty(viewName))
                {
                    viewName = filterContext.ActionDescriptor.ActionName;
                }

                // Find the view
                ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(filterContext.Controller.ControllerContext, viewName, viewResult.MasterName);

                if (!viewEngineResult.View.Equals(null))
                {
                    // Capture the response output
                    var response = filterContext.HttpContext.Response;
                    var originalOutput = response.Output;

                    using (var stringWriter = new StringWriter())
                    {
                        response.Output = stringWriter;

                        // Render the view
                        var viewContext = new ViewContext(
                            filterContext.Controller.ControllerContext,
                            viewEngineResult.View,
                            viewResult.ViewData,
                            viewResult.TempData,
                            stringWriter
                        );

                        viewEngineResult.View.Render(viewContext, stringWriter);

                        // Modify the view content
                        var renderedContent = stringWriter.ToString();
                        var modifiedContent = renderedContent.Replace("ASP.NET", "เอเอสพี ดอทเน็ต");

                        // Write the modified content back to the response
                        response.Output = originalOutput;
                        response.Write(modifiedContent);

                        // Prevent further processing of the action result
                        filterContext.Result = new EmptyResult();
                    }
                }
                else
                {
                    throw new FileNotFoundException("View cannot be found", viewName);
                }
            }
        }
    }
}