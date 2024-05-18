# CustomResponse-With-Multil-anguage

<p>Develop by .Net 4.7 MVC5</p>

<u>Summary of steps</u>
- Create a CustomResponseView class that performs an override action that will help with responding at directory serviceLib
- CustomResponseView is inheritance from ActionFilterAttribute
- add GlobalFilters.Filters.Add(new CustomResponseView()) to Application_Start() Global.asax
- in Global.asax add method Application_BeginRequest for check local country language 
- in _layout.cshtml edit action name to 
    <p>"@Html.ActionLink("m-Home", "Index", "Home")"  </p>
    <p>"@Html.ActionLink("m-About", "About", "Home")"</p>
    <p>"@Html.ActionLink("m-Contact", "Contact", "Home")"</p>
- Create language file .json in Resource directory
- use custom to top of class
    [CustomResponseView]
    public class HomeController : Controller{}
