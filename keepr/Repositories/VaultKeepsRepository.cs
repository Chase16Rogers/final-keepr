using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;


namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep GetOne(int id)
    {
       string sql = "SELECT * FROM vaultKeeps WHERE id = @id;";
       return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal int Create(VaultKeep newVaultKeep)
    {
       string sql = @"
       INSERT INTO vaultKeeps
       (creatorId, vaultId, keepId)
       VALUES
       (@creatorId, @vaultId, @keepId);
       SELECT LAST_INSERT_ID();
       ";
       return _db.ExecuteScalar<int>(sql, newVaultKeep);
    }

    internal object Delete(int id)
    {
       string sql = "DELETE FROM vaultKeeps WHERE id = @id;";
       _db.Execute(sql, new { id });
       return "deleted";
    }
  }
}