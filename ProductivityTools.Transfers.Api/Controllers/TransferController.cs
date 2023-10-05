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
        public IEnumerable<TransferHistory> List(x ob)
        {
            var lastElement = this.TransfersContext.Transfers.OrderBy(x => x.Date).Single();
            var list = this.TransfersContext.Transfers.Where(x => x.Name == x.Name.ToString() && x.Date == lastElement.Date);
            return list;
        }


        [HttpPost]
        [Route("Add")]
        [Authorize]
        public StatusCodeResult Add(TransferHistory transfer)
        {
            var recordWithTheSameDateExists = this.TransfersContext.Transfers.FirstOrDefault(x => x.Date == transfer.Date && x.Category==transfer.Category && x.Name==transfer.Name);
            if (recordWithTheSameDateExists != null)
            {
                this.TransfersContext.Transfers.Remove(recordWithTheSameDateExists);
                this.TransfersContext.SaveChanges();
            }
            this.TransfersContext.Add(transfer);
            this.TransfersContext.SaveChanges();

            return Ok();
        }


    }


}
