using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;


namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal IEnumerable<Keep> GetAll()
    {

    string sql = @"
       SELECT 
       k.* ,
       p.*
       FROM keeps k
       JOIN profiles p ON k.creatorId = p.id";
       return _db.Query<Keep, Profile, Keep>(sql, (keep, profile)=> {keep.creator = profile; return keep;}, splitOn: "id");
    }
    internal Keep GetOne(int id)
    {
       string sql = @"
       SELECT 
       k.*,
       p.* 
       FROM keeps k
       JOIN profiles p ON k.creatorId = p.id
       WHERE k.id = @id;";
       return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>{keep.creator = profile; return keep;}, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal IEnumerable<Keep> GetKeepsByProfile(string id)
    {
       string sql = @"
      SELECT 
       k.* ,
       p.*
       FROM keeps k
       JOIN profiles p ON k.creatorId = p.id
       WHERE k.creatorId = @id
       ";
       return _db.Query<Keep, Profile, Keep>(sql, (keep, profile)=> {keep.creator = profile; return keep;}, new { id },splitOn: "id");
    }

    internal IEnumerable<KeepVaultViewModel> GetKeepsByVault(int id)
    {
       string sql = @"
       SELECT
       k.*,
       vk.id AS vaultKeepId,
       p.*
       FROM vaultkeeps vk
       JOIN keeps k on k.id = vk.keepId
       JOIN profiles p ON k.creatorId = p.id
       WHERE vaultId = @id;
       ";
       return _db.Query<KeepVaultViewModel, Profile, KeepVaultViewModel>(sql, (keep, profile)=> {keep.creator = profile; return keep;}, new { id },splitOn: "id");
    }

    internal int Create(Keep newKeep)
    {
       string sql = @"
       INSERT INTO keeps
       (name, creatorId, description, img, views, shares, keeps)
       VALUES
       (@name, @creatorId, @description, @img, @views, @shares, @keeps);
       SELECT LAST_INSERT_ID();
       ";
       return _db.ExecuteScalar<int>(sql, newKeep);
    }

    internal Keep Edit(Keep editKeep)
    {
       string sql = @"
       UPDATE keeps
       SET
       name = @name, description = @description, img = @img, views = @views, shares = @shares, keeps = @keeps, id = @id, creatorId = @creatorId
       WHERE id = @id;
       ";
       _db.Execute(sql, editKeep);
       return editKeep;
    }

    internal Keep UnEdit(Keep editKeep)
    {
       string sql = @"
       UPDATE keeps
       SET
       views = @views, shares = @shares, keeps = @keeps, id = @id, creatorId = @creatorId
       WHERE id = @id;
       ";
       _db.Execute(sql, editKeep);
       return editKeep;
    }

    internal object Delete(int id)
    {
       string sql = "DELETE FROM keeps WHERE id = @id;";
       _db.Execute(sql, new { id });
       return "deleted";
    }
  }
}