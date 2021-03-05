using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
        private readonly KeepsRepository _repo;
        private readonly VaultsRepository _vRepo;

    public KeepsService(KeepsRepository repo, VaultsRepository vRepo)
    {
      _repo = repo;
      _vRepo = vRepo;
    }

    internal IEnumerable<Keep> GetAll()
    {
        return _repo.GetAll();
    }

    internal Keep GetOne(int id)
    {
        Keep found = _repo.GetOne(id);
        if (found == null)
        {
            throw new Exception("Invalid Id");
        }
        return found;
    }

    internal IEnumerable<KeepVaultViewModel> GetKeepsByVault(int id, Profile userInfo)
    {
        Vault vaultCheck = _vRepo.GetOne(id);
        if (vaultCheck.creatorId == userInfo.id)
        {
        return _repo.GetKeepsByVault(id);
        }
        if (vaultCheck.isPrivate)
        {
            throw new UnauthorizedAccessException("No");
        }
        return _repo.GetKeepsByVault(id);
    }

    internal IEnumerable<Keep> GetKeepsByProfile(string id)
    {
      return _repo.GetKeepsByProfile(id);
    }

    internal Keep Create(Keep newKeep)
    {
        int id = _repo.Create(newKeep);
        return GetOne(id);
    }

    internal Keep Edit(int id, Keep editKeep, string userId)
    {
        Keep found = GetOne(id);
        if (found == null)
        {
            throw new Exception("Invalid Id");
        }
        if (found.creatorId != userId)
        {
            throw new UnauthorizedAccessException("Not authorized");
        }
        found.name = editKeep.name == null ? found.name : editKeep.name;
        found.description = editKeep.description == null ? found.description : editKeep.description;
        // found.keeps = editKeep.keeps > 0 ? editKeep.keeps : found.keeps;
        // found.views = editKeep.views > found.views ? found.views + 1 : found.views;
        // found.shares = editKeep.shares > found.shares ? found.shares + 1 : found.shares;
        editKeep.id = id;
        editKeep.creatorId = found.creatorId;
        return _repo.Edit(found);
    }
    
        internal Keep UnEdit(int id, Keep editKeep)
    {
        Keep found = GetOne(id);
        if (found == null)
        {
            throw new Exception("Invalid Id");
        }
        found.keeps = editKeep.keeps > found.keeps ? found.keeps + 1 : found.keeps;
        found.views = editKeep.views > found.views ? found.views + 1 : found.views;
        found.shares = editKeep.shares > found.shares ? found.shares + 1 : found.shares;
        editKeep.id = id;
        editKeep.creatorId = found.creatorId;
        return _repo.UnEdit(found);
    }

    internal object Delete(int id, string userId)
    {
        Keep found = GetOne(id);
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