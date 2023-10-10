﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Transfers.Database;
using ProductivityTools.Transfers.Database.Objects;
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
            return $"Welcome {name}";
        }
        public class x
        {
            string Name { get; set; }
        }

        [HttpPost]
        [Route("TransferItem")]
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
        public IEnumerable<TransferResponse> List(TransferItem source)
        {
            var r=this.TransferService.GetTransferList(source.TransferId);
            return r;
        }


        [HttpPost]
        [Route("TransferEdit")]
        public StatusCodeResult Add(Transfer transfer)
        {
            if (transfer.TransferId == null)
            {
                this.TransfersContext.Transfers.Add(transfer);
            }
            else
            {
                this.TransfersContext.Transfers.Update(transfer);
            }
            this.TransfersContext.SaveChanges();
            return Ok();
        }
    }
}
