using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Transfers.Database;
using ProductivityTools.Transfers.Database.Objects;

namespace ProductivityTools.Transfers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : Controller
    {
        TransfersContext TransfersContext;

        public TransferController(TransfersContext transfersContext)
        {
            this.TransfersContext = transfersContext;
        }

        [HttpGet]
        [Route("echo")]
        public string echo(string name)
        {
            return $"Welcome {name}";
        }
        public class x
        {
            string Name { get; set; }
        }

        [HttpPost]
        [Route("List")]
        public IEnumerable<Transfer> List(x ob)
        {
            var lastElement = this.TransfersContext.Transfers.ToList();
            return lastElement;
        }


        [HttpPost]
        [Route("Add")]
        public StatusCodeResult Add(Transfer transfer)
        {
            this.TransfersContext.Transfers.Add(transfer);
            this.TransfersContext.SaveChanges();
            return Ok();
        }
    }
}
