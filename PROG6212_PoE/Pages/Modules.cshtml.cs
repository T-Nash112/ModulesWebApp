using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG6212_PoE.Model;

namespace PROG6212_PoE.Pages
{
    public class ModulesModel : PageModel
    {
        public List<CustomLibrary> m = new List<CustomLibrary>();
        public void OnGet()
        {
            CustomLibrary md = new CustomLibrary();
            m = md.allModules();
        }
    }
}
