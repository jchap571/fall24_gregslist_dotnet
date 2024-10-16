
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

  internal void DeleteHouse(int houseId)
  {
    string sql = "DELETE FROM houses WHERE id = @houseID LIMIT 1;";
    int rowsAffected = _db.Execute(sql, new { houseId });

    if (rowsAffected == 0)
    {
      throw new Exception("No houses were deleted");
    }
    if (rowsAffected > 1)
    {
      throw new Exception("more than one house was deleted");
    }

  }

  internal void UpdateHouse(House house)
  {
    string sql = @"
        UPDATE houses
        SET
        bathrooms = @Bathrooms,
        bedrooms = @Bedrooms,
        price = @Price,
        description = @Description,
        WHERE id = @Id
        LIMIT 1;";

    int rowsAffected = _db.Execute(sql, house);
    if (rowsAffected == 0)
    {
      throw new Exception("No houses were updated");
    }
    if (rowsAffected > 1)
    {
      throw new Exception("more than one house was updated");
    }
  }
}


