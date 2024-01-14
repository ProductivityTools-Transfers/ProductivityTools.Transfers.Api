using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Transfers.Database;
using ProductivityTools.Transfers.Database.Objects;
using ProductivityTools.Transfers.WebApi.Requests;
using System.Security.Cryptography.Xml;

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
        [Authorize]

        public Account? Item(AccountItem input)
        {
            var account = this.TransfersContext.Accounts
                .SingleOrDefault(x => x.AccountId ==input.AccountId);
            return account;
        }



        [HttpPost]
        [Route("AccountEdit")]
        public StatusCodeResult Edit(Account account)
        {
            if (account.AccountId== null)
            {
                this.TransfersContext.Accounts.Add(account);
            }
            else
            {
                this.TransfersContext.Accounts.Update(account);
            }
            this.TransfersContext.SaveChanges();
            return Ok();
        }
    }
}
