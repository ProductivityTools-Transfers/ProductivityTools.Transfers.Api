using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Transfers.Database;
using ProductivityTools.Transfers.Database.Objects;
using ProductivityTools.Transfers.WebApi.Requests;

namespace ProductivityTools.Transfers.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        TransfersContext TransfersContext;

        public AccountController(TransfersContext transfersContext)
        {
            this.TransfersContext = transfersContext;
        }

        [HttpPost]
        [Route("AccountList")]
        public IEnumerable<Account> List(object x)
        {
            var accounts = this.TransfersContext.Accounts.ToList();
            return accounts;
        }

        [HttpPost]
        [Route("AccountItem")]
        public Account? Item(AccountItem x)
        {
            var account = this.TransfersContext.Accounts
                .SingleOrDefault(x => x.AccountId == x.AccountId);
            return account;
        }



        [HttpPost]
        [Route("AccountAdd")]
        public StatusCodeResult Add(Account account)
        {
            this.TransfersContext.Accounts.Add(account);
            this.TransfersContext.SaveChanges();
            return Ok();
        }
    }
}
