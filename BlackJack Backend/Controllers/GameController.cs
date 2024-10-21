using BlackJack_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack_Backend.Controllers
{
    [ApiController]
    [Route("api/game")]

    public class GameController : ControllerBase
    {
        private readonly Deck _deck;
        private readonly Dealer _dealer;
        private readonly Player _player;
        private readonly GameLogic _gameLogic;

        [HttpPost("start-game")]
        public ActionResult StartGame([FromBody] BetRequestDto betDto)
        {
            try
            {
                _player.HandOfCards.Clear();
                _dealer.HandOfCards.Clear();
                _deck.CreateDeck();
                _player.PlaceBet(betDto.betValue);
                _gameLogic.DealInitialHand();
                var currentHands = new
                {
                    PlayerHand = _player.HandOfCards,
                    DealerHand = _dealer.HandOfCards
                };
                return Ok(currentHands);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("end-game")]
        public ActionResult EndGame([FromBody] BetRequestDto betDto)
        {
            try
            {
                _gameLogic.CheckGameOver();
                if (_gameLogic.IsGameOver)
                {
                    _player.EvaluateBet(betDto.betValue);
                    return Ok(new
                    {
                        _gameLogic.Winner,
                        _player.Currency,
                        _gameLogic.IsATie
                    });
                }
                else
                {
                    return BadRequest("Spelet är inte slut");
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }

}

