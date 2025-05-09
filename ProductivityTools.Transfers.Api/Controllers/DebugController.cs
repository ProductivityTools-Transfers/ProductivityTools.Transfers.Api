using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Transfers.Database;

namespace ProductivityTools.Transfers.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : Controller
    {
        private readonly TransfersContext TransfersContext;

        public DebugController(TransfersContext context)
        {
            this.TransfersContext = context;
        }

        [HttpGet]
        [Route("Date")]
        public string Date()
        {
            return DateTime.Now.ToString();
        }

        [HttpGet]
        [Route("AppName")]
        public string AppName()
        {
            return "PTTransfers";
        }

        [HttpGet]
        [Route("Hello")]
        public string Hello(string name)
        {
            return string.Concat($"Hello {name.ToString()} Current date:{DateTime.Now}");
        }

        [HttpGet]
        [Route("ServerName")]
        public string ServerName()
        {
            string server = this.TransfersContext.Database.SqlQuery<string>($"select @@SERVERNAME as value").Single();
            return server;
        }
    }
}
