using System.Collections.Generic;
using System.Web.Http;
using Domain.Abstract;
using Domain.Entities;

namespace API.Controllers.Entities
{
  public class UnitMeasureController : ApiController
  {
    private ICommonRepository<UnitMeasure> unitMeasureRepository;

    public UnitMeasureController(ICommonRepository<UnitMeasure> unitsMeasure)
    {
      unitMeasureRepository = unitsMeasure;
    }

    [Route("UnitMeasure/all")]
    [HttpGet]
    public IEnumerable<UnitMeasure> GetUnitsMeasure()
    {
      return unitMeasureRepository.Data;
    }
  }
}
