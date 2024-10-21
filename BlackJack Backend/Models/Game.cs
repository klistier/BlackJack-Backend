namespace BlackJack_Backend.Models
{
    public class Game
    {

        private readonly GameLogic _gameLogic;

        public void StartGame()
        {
            _gameLogic.DealInitialHand();
        }

    }
}
