# CustomResponse-With-Multil-anguage

<p>Develop by .Net 4.7 MVC5</p>

Summary of steps
- Create a CustomResponseView class that performs an override action that will help with responding at directory serviceLib
- CustomResponseView is inheritance from ActionFilterAttribute
- add GlobalFilters.Filters.Add(new CustomResponseView()) to Application_Start() Global.asax
- in Global.asax add method Application_BeginRequest for check local country language 
- in _layout.cshtml edit action name to 
    "@Html.ActionLink("m-Home", "Index", "Home")"  
    "@Html.ActionLink("m-About", "About", "Home")"
    "@Html.ActionLink("m-Contact", "Contact", "Home")"
- Create language file .json in Resource directory
- use custom to top of class
    [CustomResponseView]
    public class HomeController : Controller{}
