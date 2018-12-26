using EasyNote.Core.Logic.Accounts;
using EasyNote.Core.Model.Accounts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace EasyNote.Controllers
{
  public class AccountsController : Controller
  {
    private readonly IAccountsManager _accountsManager;

    public AccountsController(IAccountsManager accountsManager)
    {
      _accountsManager = accountsManager;
    }

    // POST accounts/register
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]CredentialsParams model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      await _accountsManager.CreateUserAsync(model);

      return Ok();
    }

    //POST accounts/login
    [HttpPost]
    public async Task<IActionResult> Login([FromBody]CredentialsParams credentials)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        var token = await _accountsManager.GetUserTokenAsync(credentials);
        var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject(token);
        return new OkObjectResult(deserialized);
      }
      catch (AuthenticationException)
      {
        return Unauthorized();
      }
    }
  }
}
