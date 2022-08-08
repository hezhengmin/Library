using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        public class TestDto {
            public int A { get; set; }
            public int B { get; set; }
        }

        [HttpGet("{id}")]
        public dynamic Get([FromRoute] string id, 
            [FromQuery] string query, 
            [FromBody] TestDto testDto)
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(id);
            list.Add(query);
            list.Add(testDto);

            return list;
        }


        [HttpGet("FormTest")]
        public dynamic Get([FromQuery] string query,
            [FromForm] string form)
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(query);
            list.Add(form);

            return list;
        }
    }
}
