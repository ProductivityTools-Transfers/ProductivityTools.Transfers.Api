using ProductivityTools.Transfers.Database.Objects;

namespace ProductivityTools.Transfers.WebApi.Responses
{
    public class TransferResponse:Transfer
    {
        public TransferResponse(Transfer transfer)
        {
            this.TransferId = transfer.TransferId;
            this.SourceId = transfer.SourceId;
            this.Source = transfer.Source;
            this.TargetId = transfer.TargetId;
            this.Target = transfer.Target;
            this.TargetTag = transfer.TargetTag;
            this.Value = transfer.Value;
            this.TransferDay = transfer.TransferDay;
            this.ValueComment = transfer.ValueComment;
            
        }

        public int ChildTransfers { get; set; }
    }
}
