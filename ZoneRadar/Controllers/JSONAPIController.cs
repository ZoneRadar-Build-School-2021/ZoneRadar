using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class JSONAPIController : ApiController
    {
        private readonly SpaceService _spaceService;
        public JSONAPIController()
        {
            _spaceService = new SpaceService();
        }

        [AcceptVerbs("GET")]
        public IHttpActionResult GetFilterData()
        {
            var json = _spaceService.GetFilterJSON();
            if (json == null)
            {
                return NotFound();
            }

            return Ok(json);
        }
    }
}
