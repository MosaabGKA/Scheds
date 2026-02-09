using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scheds.Domain.Configuration;

namespace Scheds.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly FrontendSettings _frontend;
        private readonly IWebHostEnvironment _env;

        public AdminController(IOptions<FrontendSettings> frontend, IWebHostEnvironment env)
        {
            _frontend = frontend.Value;
            _env = env;
        }

        public IActionResult Login()
        {
            return RedirectOrServeSpa("/admin/login");
        }

        [HttpPost]
        public IActionResult Login(string password)
        {
            return RedirectOrServeSpa("/admin/login");
        }

        public IActionResult Logout()
        {
            return RedirectOrServeSpa("/admin/login");
        }

        public IActionResult Index()
        {
            return RedirectOrServeSpa("/admin");
        }

        public IActionResult Analytics()
        {
            return RedirectOrServeSpa("/admin/analytics");
        }

        public IActionResult GenerationHistory()
        {
            return RedirectOrServeSpa("/admin/generations");
        }

        public IActionResult GenerationDetails(int id)
        {
            return RedirectOrServeSpa($"/admin/generations/{id}");
        }

        private IActionResult RedirectOrServeSpa(string path)
        {
            if (string.IsNullOrWhiteSpace(_frontend.Url))
            {
                return ServeSpaIndex();
            }

            return Redirect($"{_frontend.Url.TrimEnd('/')}{path}");
        }

        private IActionResult ServeSpaIndex()
        {
            var indexPath = Path.Combine(_env.WebRootPath ?? string.Empty, "index.html");
            if (!System.IO.File.Exists(indexPath))
            {
                return NotFound();
            }

            return PhysicalFile(indexPath, "text/html");
        }
    }
}
