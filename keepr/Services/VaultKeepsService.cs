using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {
        private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
        _repo = repo;
    }

    internal VaultKeep GetOne(int id)
    {
        VaultKeep found = _repo.GetOne(id);
        if (found == null)
        {
            throw new Exception("Invalid Id");
        }
        return found;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
        int id = _repo.Create(newVaultKeep);
        newVaultKeep.id = id;
        return newVaultKeep;
    }


    internal object Delete(int id, string userId)
    {
         VaultKeep found = GetOne(id);
        if (found == null)
        {
            throw new Exception("Invalid Id");
        }
        if (found.creatorId != userId)
        {
            throw new UnauthorizedAccessException("Not authorized");
        }
        return _repo.Delete(id);
    }
  }
}