using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class nameOfThePageModel : PageModel
    {
        private readonly ILogger<nameOfThePageModel> _logger;

    public nameOfThePageModel(ILogger<nameOfThePageModel> logger)
    {
        _logger = logger;
    }


    

    public void OnGet()
    {

    }
    }
}
