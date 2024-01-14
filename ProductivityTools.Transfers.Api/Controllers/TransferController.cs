using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Transfers.Database;
using ProductivityTools.Transfers.Database.Objects;
using ProductivityTools.Transfers.WebApi;
using ProductivityTools.Transfers.WebApi.Requests;
using ProductivityTools.Transfers.WebApi.Responses;
using ProductivityTools.Transfers.WebApi.Services;

namespace ProductivityTools.Transfers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : Controller
    {
        TransfersContext TransfersContext;
        TransferService TransferService;

        public TransferController(TransfersContext transfersContext, TransferService transferService)
        {
            this.TransfersContext = transfersContext;
            this.TransferService = transferService;
        }

        [HttpGet]
        [Route("echo")]
        public string echo(string name)
        {
            return $"Welcome request performed at {DateTime.Now} with param {name} on server {System.Environment.MachineName} to Application Transfers";
        }
        public class x
        {
            string Name { get; set; }
        }

        [HttpPost]
        [Route("TransferItem")]
        [Authorize]
        public Transfer Item(TransferItem transferItem)
        {
            var lastElement = this.TransfersContext.Transfers
                .Include(x => x.Source)
                .Include(x => x.Target)
                .Where(x => x.TransferId == transferItem.TransferId)
                .SingleOrDefault();
            return lastElement;
        }

        [HttpPost]
        [Route("TransferList")]
        [Authorize]
        [AuthenticatedUsers]
        public IEnumerable<TransferResponse> List(TransferItem source)
        {
            var r=this.TransferService.GetTransferList(source.TransferId);
            return r;
        }

        [HttpPost]
        [Route("TransferDelete")]
        [Authorize]
        [AuthenticatedUsers]
        public StatusCodeResult Delete(TransferDelete source)
        {
            this.TransferService.DeleteTransfer(source.TransferId);
            return Ok();
        }


        [HttpPost]
        [Route("TransferEdit")]
        [Authorize]
        public StatusCodeResult Add(Transfer transfer)
        {
            if (transfer.TransferId == null)
            {
                this.TransfersContext.Transfers.Add(transfer);
            }
            else
            {
                var update=this.TransfersContext.Transfers.Single(x=>x.TransferId== transfer.TransferId);
                update.TransferDay = transfer.TransferDay;
                update.SourceId=transfer.SourceId;  
                update.TargetId=transfer.TargetId;
                update.Value=transfer.Value;
                update.ValueComment = transfer.ValueComment;
            }
            this.TransfersContext.SaveChanges();
            return Ok();
        }
    }
}
