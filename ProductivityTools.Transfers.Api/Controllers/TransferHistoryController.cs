﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Transfers.Database;
using ProductivityTools.Transfers.Database.Objects;

namespace ProductivityTools.Transfers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferHistoryController : Controller
    {
        TransfersContext TransfersContext;

        public TransferHistoryController(TransfersContext transfersContext)
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
        [Authorize]

        public IEnumerable<TransferHistory> List(x ob)
        {
            //var lastElement = this.TransfersContext.TransfersHistory.OrderBy(x => x.Date).Single();
            var list = this.TransfersContext.TransfersHistory.ToList();
            return list;
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        //I cannot find any reference for this method
        //I think I wrote it but not use with the current approach (web) this method does not make sense
        public StatusCodeResult Add(TransferHistory transfer)
        {
            var recordWithTheSameDateExists = this.TransfersContext.TransfersHistory.FirstOrDefault(x => x.Date == transfer.Date && x.Source==transfer.Source && x.Target==transfer.Target);
            if (recordWithTheSameDateExists != null)
            {
                this.TransfersContext.TransfersHistory.Remove(recordWithTheSameDateExists);
                this.TransfersContext.SaveChanges();
            }
            this.TransfersContext.Add(transfer);
            this.TransfersContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        [Route("AddSnapshot")]
        [Authorize]
        //I cannot find any reference for this method
        //I think I wrote it but not use with the current approach (web) this method does not make sense
        public StatusCodeResult AddSnapshot(object nothing)
        {
            var r=this.TransfersContext.Database.ExecuteSqlRaw("EXEC [dbo].[TodayTransferHistory]");
            return Ok();
        }
    }
}
