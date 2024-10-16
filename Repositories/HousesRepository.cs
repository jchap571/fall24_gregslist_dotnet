
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

  internal House CreateHouse(House houseData)
  {
    string sql = @"
       INSERT INTO
       houses(sqft, bathrooms, bedrooms, imgUrl, description, price, creatorId)
       VALUES(@Sqft, @Bathrooms, @Bedrooms, @ImgUrl, @Description, @Price, @CreatorId);
       
       SELECT
       houses.*,
       accounts.*
       FROM houses
       JOIN accounts ON houses.creatorId = accounts.id
       WHERE houses.id = LAST_INSERT_ID();";

    House house = _db.Query<House, Account, House>(sql, (house, account) =>
    {
      house.Creator = account;
      return house;
    },
    houseData).FirstOrDefault();
    return house;
  }
}


