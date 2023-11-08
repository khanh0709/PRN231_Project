using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace CoFAB.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string message { get; set; } 
        public string backUrl { get; set; } 

        public ErrorModel()
        {
        }

        public void OnGet(string message, string backUrl)
        {
            this.message = message; 
            this.backUrl = backUrl; 
        }
    }
}