using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Part1;
using PROG6212_PoE.Model;

namespace PROG6212_PoE.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            string username = Request.Form["txtName"];
            string pass = Request.Form["txtPassword"];

            Auth em = new Auth(username, pass);
            em.Register();
            Response.Redirect("/Login");          

        }
    }
}
