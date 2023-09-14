using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ProductivityTools.Transfers.WebApi.Controllers
{
    public class CustomTokenController : Controller
    {
        [HttpGet]
        [Route("GetPython")]
        public async Task<string> GetToken()
        {
            var uid = "GetPython";
            string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
            return customToken;
        }
    }
}
