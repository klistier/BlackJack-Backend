using BlackJack_Backend.Models;
using BlackJack_Backend.Models.Dto;

namespace BlackJack_Backend.Service
{
    public class GameService
    {


        private readonly Game _game;

        public GameService(Game game)
        {
            _game = game;
        }

        //dra ett kort
        public Card DrawCard()
        {
            var card = _game.Deck.CurrentDeck[0];
            _game.Deck.CurrentDeck.RemoveAt(0);
            return card;
        }

        //Hit
        public void Hit()
        {
            if (!_game.Player.CanDrawCard)
            {
                throw new Exception("Du kan inte dra fler kort!");
            }
            var card = DrawCard();
            _game.Player.HandOfCards.Add(card);
            int currentHandValue = CalculateHandValue(_game.Player.HandOfCards);
            _game.Player.HandValue = currentHandValue;
            if (currentHandValue > 21)
            {
                _game.Player.CanDrawCard = false;
            }
            CheckGameOver();
        }

        //Stand
        public void Stand()
        {
            int playerHandValue = _game.Player.HandValue;
            int dealerHandValue = _game.Dealer.HandValue;

            foreach (var card in _game.Dealer.HandOfCards)
            {
                card.IsFaceUp = true;
            }
            while (dealerHandValue < 17 && playerHandValue > dealerHandValue)
            {
                Card card = DrawCard();
                _game.Dealer.HandOfCards.Add(card);
                dealerHandValue = CalculateHandValue(_game.Dealer.HandOfCards);
                _game.Dealer.HandValue = dealerHandValue;
            }
            CheckGameOver();
        }

        //dela ut första handen
        public void DealInitialHand()
        {
            _game.Player.HandOfCards = [DrawCard(), DrawCard()];
            var faceDownCard = DrawCard();
            faceDownCard.IsFaceUp = false;
            _game.Dealer.HandOfCards = [faceDownCard, DrawCard()];
            _game.Player.HandValue = CalculateHandValue(_game.Player.HandOfCards);
            _game.Dealer.HandValue = CalculateHandValue(_game.Dealer.HandOfCards);
        }

        //räkna ut värdet på en hand
        public int CalculateHandValue(List<Card> handOfCards)
        {
            int totalSum = 0;
            int aceCount = 0;

            foreach (Card card in handOfCards)
            {
                if (int.TryParse(card.Value, out int cardValue))
                {
                    totalSum += cardValue;
                }
                else if (card.Value == "Knekt" || card.Value == "Dam" || card.Value == "Kung")
                {
                    totalSum += 10;
                }
                else if (card.Value == "Ess")
                {
                    totalSum += 11;
                    aceCount++;
                }
            }

            while (totalSum > 21 && aceCount > 0)
            {
                totalSum -= 10;
                aceCount--;
            }

            return totalSum;
        }



        //Räkna ut om bettet vanns eller ej
        public void EvaluateBet(int betSum)
        {
            if (_game.Winner == "Player")
            {
                betSum *= 2;
                _game.Player.Currency += betSum;
            }
            else if (_game.Winner == "Dealer")
            {
                _game.Player.Currency -= betSum;
            }
            else if (_game.IsATie)
            {
                _game.Player.Currency += betSum;
            }
        }

        //kolla om spelet är slut 
        public void CheckGameOver()
        {
            int dealerValue = CalculateHandValue(_game.Dealer.HandOfCards);
            int playerValue = CalculateHandValue(_game.Player.HandOfCards);


            if (dealerValue == 21 && playerValue == 21)
            {
                _game.IsGameOver = true;
                _game.IsATie = true;
                _game.Winner = "Tie";
            }
            else if (playerValue == 21 && dealerValue != 21 || playerValue <= 21 && dealerValue > 21)
            {
                _game.IsGameOver = true;
                _game.Winner = "Player";
            }
            else if (dealerValue == 21 && playerValue != 21 || dealerValue <= 21 && playerValue > 21)
            {
                _game.IsGameOver = true;
                _game.Winner = "Dealer";
            }
            else
            {
                _game.IsGameOver = false;
            }
        }

        //Starta nytt spel
        public void StartNewGame(BetRequestDto betDto)
        {
            _game.Deck.CreateDeck();
            _game.Player.HandOfCards.Clear();
            _game.Dealer.HandOfCards.Clear();
            _game.Player.CanDrawCard = true;
            _game.IsGameOver = false;
            _game.IsATie = false;
            _game.Winner = string.Empty;
            _game.Player.PlaceBet(betDto.BetValue);
            DealInitialHand();
        }

        //Avsluta spel
        public void EndGame(BetRequestDto betDto)
        {
            CheckGameOver();
            if (_game.IsGameOver)
            {
                EvaluateBet(betDto.BetValue);
            }
        }

        public Game GetCurrentGame()
        {
            return _game;
        }
    }
}
