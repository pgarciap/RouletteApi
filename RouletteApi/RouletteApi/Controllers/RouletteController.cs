using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RouletteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly IDatabase _database;
        public RouletteController(IDatabase database)
        {
            _database = database;
        }


        [HttpGet ]
        public string Get([FromQuery]string key)
        {
            return _database.StringGet(key);
        }

        [HttpPost]
        public void Post([FromQuery] KeyValuePair<string,string> keyValue)
        {
            _database.StringSet(keyValue.Key ,keyValue.Value );
        }

    }
}
