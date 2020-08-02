using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RouletteApi.Models;
using StackExchange.Redis;

namespace RouletteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly IDatabase _database;
        private IConfiguration configuration { get; }
        public RouletteController(IDatabase database, IConfiguration iconfig)
        {
            _database = database;
            configuration = iconfig;
        }

        [HttpPost]
        [Route("createRoulette/")]
        public string PostCreateRoulette()
        {
            string IdRoulete = createNewID("ROU", "Rouletteposition");
            _database.StringSet(IdRoulete, "Close");

            return IdRoulete;
        }

        [HttpPut]
        [Route("StartRoulette/")]
        public string PutStartRoulette([FromBody] string IdRoulette)
        {
            return updateStateRoulette(IdRoulette);
        }

        [HttpPost("{userId}")]
        [Route("CreateBet/")]
        public void PostCreateBet(string userId, [FromBody] Bet model)
        {
            BetResult betR = new BetResult();
            Random rnd = new Random();
            string IdBet = "";
            betR.idRoulette = model.IdRoulette;
            betR.userId = userId;
            betR.Amount = model.Amount;
            betR.BetInformation = model.BetInformation;
            if (validateBet(betR) && validateIdRoulete(betR))
            {
                betR.Result = rnd.Next(1, 3) == 1 ? "Win" : "Lose";
            }
            else
            {
                betR.Result = "Error";
            }
            betR.id = createNewID("BET", "Rouletteposition");
            betR.DateTimeResult = Convert.ToDateTime(DateTime.UtcNow.ToString("s") + "Z");
            //var betResultInString = JsonConvert.SerializeObject(betR);

        }

        private string createNewID(string strCode, string keyPosition )
        {
            string lastPosition = "";
            int newPosition = 0;
            if (!_database.KeyExists(keyPosition)) {
                newPosition = 1;
            }
            else
            {
                lastPosition= _database.StringGet(keyPosition);
                newPosition = Int16.Parse(lastPosition)+1;
            }
            _database.StringGetSet(strCode, newPosition);

            return strCode + newPosition;
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

        private bool validateBet(BetResult betR)
        {
            bool result = false;
            int maxAmount = configuration.GetValue<int>("MaxAmount");
            string color1 = configuration.GetValue<string>("Color1");
            string color2 = configuration.GetValue<string>("Color2");
            int MinNum = configuration.GetValue<int>("MinNum");
            int MaxNum = configuration.GetValue<int>("MaxNum");
            if (betR.Amount < maxAmount)
            {
                result = true;

                if (betR.BetInformation.Trim() == color1 || betR.BetInformation.Trim() == color2)
                {
                    result = true;
                }
                else
                {
                    for (int i = MinNum; i <= MaxNum; i++)
                    {
                        if (betR.BetInformation.Trim() == i + "")
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }

            return result;
        }
        private bool validateIdRoulete(BetResult betR)
        {
            bool result = false;
            if (_database.KeyExists(betR.idRoulette))
            {
                if (_database.StringGet(betR.idRoulette) == "Open")
                {
                    result = true;
                }
            }

            return result;
        }
     
    }
}
