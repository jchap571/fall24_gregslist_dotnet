
using System.Reflection.Metadata.Ecma335;

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

}

