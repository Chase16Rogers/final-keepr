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
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _PS;
        private readonly KeepsService _KS;
        private readonly VaultsService _VS;

    public ProfilesController(ProfilesService pS, KeepsService kS, VaultsService vS)
    {
      _PS = pS;
      _KS = kS;
      _VS = vS;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
        try
        {
            return Ok(_PS.GetOne(id)); 
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
   
   [HttpGet("{id}/keeps")]
   public ActionResult<IEnumerable<Keep>> GetKeepsByProfile(string id)
   {
       try
       {
           return Ok(_KS.GetKeepsByProfile(id));
       }
       catch (System.Exception error)
       {
           return BadRequest(error.Message);
       }
   }

   [HttpGet("{id}/vaults")]
   public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByProfileAsync(string id)
   {
       try
       {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            if(userInfo != null)
            {
                return Ok(_VS.GetVaultsByProfile(id, userInfo.id));
            }
            else
            {
                return Ok(_VS.GetVaultsByProfile(id, "failing"));
            }
       }
       catch (System.Exception error)
       {
            return BadRequest(error.Message);
       }
    }
    }
}