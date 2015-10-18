﻿using System.Collections.Generic;
using System.Linq;
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

    [HttpGet]
    [Route("UnitMeasure/getby")]
    public IEnumerable<UnitMeasure> GetUnitMeasureBy([FromUri] UnitMeasure u)
    {
      return (from x in unitMeasureRepository.Data
              where (x.Name == u.Name && x.Name != "" || x.ShortName == u.ShortName && x.ShortName != "" ||
              x.UnitMeasureID == u.UnitMeasureID && x.UnitMeasureID != 0)
              select x);
    }
  }
}