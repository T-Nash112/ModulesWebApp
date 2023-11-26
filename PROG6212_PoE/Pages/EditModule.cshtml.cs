using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG6212_PoE.Model;

namespace PROG6212_PoE.Pages
{
    public class EditModuleModel : PageModel
    {
        public CustomLibrary md = new CustomLibrary();
        public void OnGet()
        {
            string code = Request.Query["Code"];
            md = md.getModule(code);
        }
    }
}
