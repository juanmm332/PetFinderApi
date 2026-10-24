using BusinessLogicLayer;
using EntitiesLayer;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class TossedCoinController : ControllerBase
    {
        private readonly ITossedCoinManager _manager;
        
        public TossedCoinController(ITossedCoinManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public IActionResult Post([FromQuery] string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                return BadRequest();
            }

            string coinValue = _manager.TossCoin(playerName);

            return Ok(new { CoinValue = coinValue });
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            List<TossedCoin> coinList = _manager.GetAllTossedCoins();
            return Ok(new { coinList });
        }
    }
}
