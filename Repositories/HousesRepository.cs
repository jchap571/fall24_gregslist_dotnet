
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace gregslist_csharp.Repositories;

public class HousesRepository
{
  public HousesRepository(IDbConnection db)
  {
    _db = db;
  }

  private readonly IDbConnection _db;





  internal List<House> GetAllHouses()
  {
    string sql = "SELECT * FROM houses;";

    List<House> houses = _db.Query<House>(sql).ToList();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {
    string sql = @"
        SELECT
        houses.*,
        accounts.*
        FROM houses
        JOIN accounts ON houses.creatorId = accounts.id
        WHERE houses.id = @houseId;";

    House house = _db.Query<House, Account, House>(sql, (house, account) =>
    {
      house.Creator = account;
      return house;
    }, new { houseId }).FirstOrDefault();
    return house;
  }
}


