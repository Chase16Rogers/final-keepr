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
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _KS;

    public KeepsController(KeepsService kS)
    {
      _KS = kS;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
        try
        {
            return Ok(_KS.GetAll());
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
        try
        {
            return Ok(_KS.GetOne(id));
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> CreateAsync([FromBody] Keep newKeep)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            newKeep.creatorId = userInfo.id;
            return Ok(_KS.Create(newKeep));
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Keep>> EditAsync(int id, [FromBody] Keep editKeep)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            return Ok(_KS.Edit(id, editKeep, userInfo.id));
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
    
    [HttpPut("{id}/update")]
    public ActionResult<Keep> UnEdit(int id, [FromBody] Keep editKeep)
    {
        try
        {
            return Ok(_KS.UnEdit(id, editKeep));
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
            return Ok(_KS.Delete(id, userInfo.id));
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
  }
}