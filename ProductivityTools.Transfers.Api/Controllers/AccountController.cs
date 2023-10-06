using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Transfers.Database;
using ProductivityTools.Transfers.Database.Objects;

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
        [Route("List")]
        public IEnumerable<Account> List(object x)
        {
            var accounts = this.TransfersContext.Accounts.ToList();
            return accounts;
        }


        [HttpPost]
        [Route("Add")]
        public StatusCodeResult Add(Account account)
        {
            this.TransfersContext.Accounts.Add(account);
            this.TransfersContext.SaveChanges();
            return Ok();
        }
    }
}
