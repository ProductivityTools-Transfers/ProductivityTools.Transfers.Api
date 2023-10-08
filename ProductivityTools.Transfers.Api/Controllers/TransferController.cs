using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Route("TransferList")]
        public IEnumerable<Transfer> List(x ob)
        {
            var lastElement = this.TransfersContext.Transfers
                .Include(x=>x.Source)
                .Include(x=>x.Target)
                .ToList();
            return lastElement;
        }


        [HttpPost]
        [Route("TransferAdd")]
        public StatusCodeResult Add(Transfer transfer)
        {
            this.TransfersContext.Transfers.Add(transfer);
            this.TransfersContext.SaveChanges();
            return Ok();
        }
    }
}
