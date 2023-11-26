using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Part1;

namespace PROG6212_PoE.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            string username = Request.Form["txtName"];
            string password = Request.Form["txtPassword"];

            Auth lg = new Auth(username, password);
            //lg.getUser(username, password);
            //bool isValid = lg.Verify(username,password);
            //lg.getUser(username,password);
            if (lg != null)
            {
                if (lg.Username.Equals(username) && lg.Password.Equals(password))
                {
                    Response.Redirect("/Home");
                }
            }
            else
            {
                Response.Redirect("/Register");
            }
        }
    }
}
