using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG6212_PoE.Model;

namespace PROG6212_PoE.Pages
{
    public class ModulesChartModel : PageModel
    {
        public List<CustomLibrary> cl = new List<CustomLibrary>();
        public void OnGet()
        {
            CustomLibrary md = new CustomLibrary();
            cl = md.allModules();
        }
    }
}
