using System;
using System.Collections.Generic;
using System.Linq;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
        private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal IEnumerable<Vault> GetAll()
    {
        return _repo.GetAll();
    }

    internal Vault GetOne(int id, string userId)
    {
        Vault found = _repo.GetOne(id);
        if (found == null)
        {
            throw new Exception("Invalid Id");
        }
        if (found.creatorId == userId)
        {
            return found;
        } 
        else if (found.isPrivate)
        {
        throw new UnauthorizedAccessException("Not authorized");
        }
        else 
        {
        Vault rtn = found;
        return rtn; 
        }

    }
    internal IEnumerable<Vault> GetVaultsByProfile(string id, string userId)
    {
      IEnumerable<Vault> foundVaults = _repo.GetVaultsByProfile(id);
      if (id == userId)
      {
          return foundVaults;
      }
      else
      {
          return foundVaults.Where(v=> v.isPrivate == false);
      }
    }

    internal Vault Create(Vault newVault)
    {
        int id = _repo.Create(newVault);
        return GetOne(id, newVault.creatorId);
    }

    

    internal Vault Edit(int id, Vault editVault, string userId)
    {
        Vault found = GetOne(id, userId);
        if (found == null)
        {
            throw new Exception("Invalid Id");
        }
        if (found.creatorId != userId)
        {
            throw new UnauthorizedAccessException("Not authorized");
        }
        found.name = editVault.name == null ? found.name : editVault.name;
        found.description = editVault.description == null ? found.description : editVault.description;
        found.isPrivate = editVault.isPrivate ? true : false;
        editVault.id = id;
        return _repo.Edit(editVault);
    }

    internal object Delete(int id, string userId)
    {
         Vault found = GetOne(id, userId);
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