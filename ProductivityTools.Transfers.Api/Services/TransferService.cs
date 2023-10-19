using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Transfers.Database;
using ProductivityTools.Transfers.Database.Objects;
using ProductivityTools.Transfers.WebApi.Requests;
using ProductivityTools.Transfers.WebApi.Responses;
using System.Collections.Generic;

namespace ProductivityTools.Transfers.WebApi.Services
{
    public class TransferService
    {
        TransfersContext TransfersContext;

        public TransferService(TransfersContext transfersContext)
        {
            this.TransfersContext = transfersContext;
        }

        private int GetChildTransfers(int targetTransferId)
        {
            int sum = 0;
            var transferList = this.TransfersContext.Transfers.Where(x => x.SourceId == targetTransferId).ToList();
            if (transferList.Count > 0)
            {
                sum += transferList.Count;
                foreach (var transfer in transferList)
                {
                    sum += GetChildTransfers(transfer.TargetId);
                }
            }
            return sum;
        }

        public List<TransferResponse> GetTransferList(int? sourceTransferId)
        {
            List<TransferResponse> transferList = null;
            if (sourceTransferId == null)
            {
                transferList = this.TransfersContext.Transfers
                    .Include(x => x.Source)
                    .Include(x => x.Target)
                    .Where(x => x.Source.Type == "Income")
                    .Select(x => new TransferResponse(x))
                    .ToList();
            }
            else
            {
                transferList = this.TransfersContext.Transfers
                    .Where(x => x.SourceId == sourceTransferId)
                    .Include(x => x.Source)
                    .Include(x => x.Target)
                    .Select(x => new TransferResponse(x))
                    .ToList();
            }

            foreach (var transfer in transferList)
            {
                transfer.ChildTransfers = GetChildTransfers(transfer.TargetId);
            }

            //var targetIds = transferList.Select(x => x.TargetId).ToList();
            //var drillDown = this.TransfersContext.Transfers.Where(x => targetIds.Contains(x.SourceId)).Select

            return transferList.OrderByDescending(x => x.Value).ToList(); 




            //var transferList = this.TransfersContext.Transfers
            //      .Include(x => x.Source)
            //      .Include(x => x.Target)
            //      .Where(x => x.Source.Type == "Root")
            //      .ToList();
            //var targetIds = transferList.Select(x => x.TargetId).ToList();

            //var drillDown = this.TransfersContext.Transfers.Where(x => targetIds.Contains(x.SourceId)).ToList();


        }
    }
}
