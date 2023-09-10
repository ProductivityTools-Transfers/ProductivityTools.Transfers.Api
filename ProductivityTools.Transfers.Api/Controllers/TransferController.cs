using Microsoft.AspNetCore.Mvc;

namespace ProductivityTools.Transfers.Api.Controllers
{
    public class TransferController : Controller
    {
        [HttpGet]
        [Route("echo")]
        public string echo(object name)
        {
            return $"Welcome {name.ToString()}";
        }

        [HttpPost]
        [Route("List")]
        public string List(object name)
        {
            return "fsda";
    }
    }


}
