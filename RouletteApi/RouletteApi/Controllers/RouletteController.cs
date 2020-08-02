using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RouletteApi.Models;
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

        [HttpPost]
        [Route("createRoulette/")]
        public string PostCreateRoulette()
        {
            string IdRoulete = createNewIDRoullete();
            _database.StringSet(IdRoulete, "Close");

            return IdRoulete;
        }

        [HttpPut]
        [Route("StartRoulette/")]
        public string PutStartRoulette([FromBody] string IdRoulette)
        {
            return updateStateRoulette(IdRoulette);
        }

        [HttpPost("{id}")]
        public void Post(int id, [FromBody] string value)
        {
        }

        private string createNewIDRoullete()
        {
            string lastPosition = "";
            int newPosition = 0;
            if (!_database.KeyExists("Rouletteposition")) {
                newPosition = 1;
            }
            else
            {
                lastPosition= _database.StringGet("Rouletteposition");
                newPosition = Int16.Parse(lastPosition)+1;
            }
            _database.StringGetSet("Rouletteposition", newPosition);

            return "ROU" + newPosition;
        }

        private string updateStateRoulette(string IDRoulette)
        {
            string state = "Denied!";
            if (_database.KeyExists(IDRoulette))
            {
                _database.StringGetSet(IDRoulette,"Open");
                state = "Success!";
            }

            return state;
        }
    }
}
