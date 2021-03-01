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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _VKS;

    public VaultKeepsController(VaultKeepsService vkS)
    {
      _VKS = vkS;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> CreateAsync([FromBody] VaultKeep newVaultKeep)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            newVaultKeep.creatorId = userInfo.id;
            return Ok(_VKS.Create(newVaultKeep));
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
            return Ok(_VKS.Delete(id, userInfo.id));
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
  }
}