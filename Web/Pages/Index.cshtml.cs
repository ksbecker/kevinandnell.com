using Microsoft.AspNetCore.Mvc.RazorPages;

using Persistence;

namespace Web.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Class1.A();
    }
}
