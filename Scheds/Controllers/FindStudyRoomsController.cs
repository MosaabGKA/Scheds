using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scheds.Domain.Configuration;

namespace Scheds.MVC.Controllers
{
    public class FindStudyRoomsController : Controller
    {
        private readonly FrontendSettings _frontend;
        private readonly IWebHostEnvironment _env;

        public FindStudyRoomsController(IOptions<FrontendSettings> frontend, IWebHostEnvironment env)
        {
            _frontend = frontend.Value;
            _env = env;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrWhiteSpace(_frontend.Url))
            {
                return ServeSpaIndex();
            }

            return Redirect($"{_frontend.Url.TrimEnd('/')}/find-study-rooms");
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
