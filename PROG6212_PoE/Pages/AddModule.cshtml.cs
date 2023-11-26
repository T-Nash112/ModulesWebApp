using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG6212_PoE.Model;

namespace PROG6212_PoE.Pages
{
    public class AddModuleModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            //linking the variables to the form names
            string code = Request.Form["txtCode"];
            string name = Request.Form["txtName"];
            int credits = Convert.ToInt32(Request.Form["txtCredits"]);
            int numOfWeeks = Convert.ToInt32(Request.Form["txtNumOfWeeks"]);
            int hoursPerWeek = Convert.ToInt32(Request.Form["txtHoursPerWeek"]);
            DateTime date = Convert.ToDateTime(Request.Form["txtDate"]);
            double selfstudy = 0;


            CustomLibrary md = new CustomLibrary(code, name, credits, hoursPerWeek, numOfWeeks, selfstudy, date);
            md.addNew();
        }
    }
}
