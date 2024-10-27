using BlackJack_Backend.Models;
using BlackJack_Backend.Models.Dto;
using BlackJack_Backend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Numerics;

namespace BlackJack_Backend.Controllers
{
    [ApiController]
    [Route("api/game")]

    public class GameController : ControllerBase
    {


        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("start-game")]
        public ActionResult<Game> StartGame([FromBody] BetRequestDto betDto)
        {

            try
            {
                _gameService.StartNewGame(betDto);

                return Ok(_gameService.GetCurrentGame());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("end-game")]
        public ActionResult<Game> EndGame([FromBody] BetRequestDto betDto)
        {
            try
            {
                _gameService.EndGame(betDto);
                return Ok(_gameService.GetCurrentGame());
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("hit")]
        public ActionResult<Game> PlayerHit()
        {
            try
            {
                _gameService.Hit();
                return Ok(_gameService.GetCurrentGame());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("stand")]
        public ActionResult<Game> PlayerStand()
        {
            try
            {
                _gameService.Stand();
                return Ok(_gameService.GetCurrentGame());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
