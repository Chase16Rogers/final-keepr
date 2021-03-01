using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;


namespace keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal IEnumerable<Vault> GetAll()
    {

    string sql = @"
       SELECT 
       v.* ,
       p.*
       FROM vaults v
       JOIN profiles p ON v.creatorId = p.id";
       return _db.Query<Vault, Profile, Vault>(sql, (Vault, profile)=> {Vault.creator = profile; return Vault;}, splitOn: "id");
    }

    internal Vault GetOne(int id)
    {
       string sql = @"
       SELECT 
       v.* ,
       p.*
       FROM vaults v
       JOIN profiles p ON v.creatorId = p.id
       WHERE v.id = @id
       ";
       return _db.Query<Vault, Profile, Vault>(sql, (Vault, profile)=>{Vault.creator = profile; return Vault;}, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal IEnumerable<Vault> GetVaultsByProfile(string id)
    {
       string sql = @"
       SELECT 
       v.* ,
       p.*
       FROM vaults v
       JOIN profiles p ON v.creatorId = p.id
       WHERE v.creatorId = @id
       ";
       return _db.Query<Vault, Profile, Vault>(sql, (Vault, profile)=>{Vault.creator = profile; return Vault;}, new { id }, splitOn: "id");
    }

    internal int Create(Vault newVault)
    {
       string sql = @"
       INSERT INTO vaults
       (name, creatorId, description)
       VALUES
       (@name, @creatorId, @description);
       SELECT LAST_INSERT_ID();
       ";
       return _db.ExecuteScalar<int>(sql, newVault);
    }

    internal Vault Edit(Vault editVault)
    {
       string sql = @"
       UPDATE vaults
       SET
       name = @name, description = @description, isPrivate = @isPrivate
       WHERE id = @id;
       ";
       _db.Execute(sql, editVault);
       return editVault;
    }

    internal object Delete(int id)
    {
       string sql = "DELETE FROM vaults WHERE id = @id;";
       _db.Execute(sql, new { id });
       return "deleted";
    }
  }
}