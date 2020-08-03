using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RouletteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public String Get()
        {
            return "Roulette Home Api - created by: Paola Andrea Garcia Preciado";
        }
    }
}
