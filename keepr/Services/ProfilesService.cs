using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class ProfilesService
  {
      private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    internal Profile GetOrCreateProfile(Profile userInfo)
    {
      Profile profile = _repo.GetOne(userInfo.id);
      if(profile == null){
          return _repo.Create(userInfo);
      }
      return profile;
    }

    internal Profile GetOne(string id)
    {
      Profile found = _repo.GetOne(id);
      if (found == null)
      {
        throw new Exception("Bad Id");
      }
      return found;
    }
  }
}