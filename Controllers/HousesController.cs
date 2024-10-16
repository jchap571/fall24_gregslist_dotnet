using gregslist_csharp.Services;

namespace gregslist_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class HousesController : ControllerBase
{
  public HousesController(HousesService housesService)
  {
    _housesService = housesService;
  }



  private readonly HousesService _housesService;


  [HttpGet]
  public ActionResult<List<House>> GetAllHouses()
  {
    try
    {
      List<House> houses = _housesService.GetAllHouses();
      return Ok(houses);
    }
    catch (Exception error)
    {

      return BadRequest(error.Message);
    }
  }

}

