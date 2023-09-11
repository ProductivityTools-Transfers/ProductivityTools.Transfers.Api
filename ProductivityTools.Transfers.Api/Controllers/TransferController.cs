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
        public string echo(object name)
        {
            return $"Welcome {name.ToString()}";
        }

        [HttpPost]
        [Route("List")]
        public IEnumerable<Transfer> List(object name)
        {
            var lastElement = this.TransfersContext.Transfers.OrderBy(x => x.Date).Single();
            var list = this.TransfersContext.Transfers.Where(x => x.Name == name.ToString() && x.Date == lastElement.Date);
            return list;
        }

        [HttpPost]
        [Route("List")]
        public StatusCodeResult Add(Transfer transfer)
        {
            this.TransfersContext.Add(transfer);
            this.TransfersContext.SaveChanges();

            return Ok();
        }


    }


}
