using Microsoft.AspNetCore.Mvc;

namespace ProductivityTools.Transfers.Api.Controllers
{
    public class TransferController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
