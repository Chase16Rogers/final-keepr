using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _VS;
        private readonly KeepsService _KS;

    public VaultsController(VaultsService vS, KeepsService kS)
    {
      _VS = vS;
      _KS = kS;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Vault>> Get()
    {
        try
        {
            return Ok(_VS.GetAll());
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetAsync(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            if(userInfo != null)
            {
            return Ok(_VS.GetOne(id, userInfo.id));
            } 
            else
            {
            return Ok(_VS.GetOne(id, "failing"));
            }
        }
            
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<Vault>> GetKeepsByVault(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            return Ok(_KS.GetKeepsByVault(id, userInfo));
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> CreateAsync([FromBody] Vault newVault)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            newVault.creatorId = userInfo.id;
            return Ok(_VS.Create(newVault));
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Vault>> EditAsync(int id, [FromBody] Vault editVault)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            return Ok(_VS.Edit(id, editVault, userInfo.id));
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteAsync(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            return Ok(_VS.Delete(id, userInfo.id));
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
  }
}