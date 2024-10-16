
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Hosting;

namespace gregslist_csharp.Services;

public class HousesService
{

  public HousesService(HousesRepository housesRepository)
  {
    _housesRepository = housesRepository;
  }

  private readonly HousesRepository _housesRepository;




  internal List<House> GetAllHouses()
  {
    List<House> houses = _housesRepository.GetAllHouses();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {
    House house = _housesRepository.GetHouseById(houseId);

    if (house == null)
    {
      throw new Exception($"Invalid house Id: {houseId}");
    }
    return house;
  }

  internal House CreateHouse(House houseData)
  {
    House house = _housesRepository.CreateHouse(houseData);
    return house;
  }

  internal string DeleteHouse(int houseId, string userId)
  {
    House house = GetHouseById(houseId);
    if (house.CreatorId != userId)
    {
      throw new Exception("That ain't your hosue");

    }

    _housesRepository.DeleteHouse(houseId);

    return $"{houseId} was deleted";
  }

  internal House UpdateHouse(int houseId, string userId, House houseUpdateData)
  {
    House house = GetHouseById(houseId);

    if (house.CreatorId != userId)
    {
      throw new Exception("That's not your house, guy");
    }

    house.Bathrooms = houseUpdateData.Bathrooms ?? house.Bathrooms;
    house.Bedrooms = houseUpdateData.Bedrooms ?? house.Bedrooms;
    house.Price = houseUpdateData.Price ?? house.Price;
    house.Description = houseUpdateData.Description ?? house.Description;

    _housesRepository.UpdateHouse(house);
    return house;

  }
}

