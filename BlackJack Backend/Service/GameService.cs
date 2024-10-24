using BlackJack_Backend.Models;
using BlackJack_Backend.Models.Dto;

namespace BlackJack_Backend.Service
{
    public class GameService
    {
        public bool IsGameOver { get; set; } = false;
        public string Winner { get; set; } = string.Empty;
        public bool IsATie { get; set; } = false;

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

        //Metod för att kolla om dealern ska dra fler kort
        public void DealerDrawCard()
        {
            int dealerCardsValue = CalculateHandValue(_game.Dealer.HandOfCards);
            if (dealerCardsValue < 17)
            {
                Card card = DrawCard();
                _game.Dealer.HandOfCards.Add(card);
            }
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
            if (currentHandValue > 21)
            {
                _game.Player.CanDrawCard = false;
            }
            CheckGameOver();
        }


        //Stand
        public void Stand()
        {
            foreach (var card in _game.Dealer.HandOfCards)
            {
                card.IsFaceUp = true;
            }
            while (CalculateHandValue(_game.Dealer.HandOfCards) < 17)
            {
                DealerDrawCard();
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
        }


        //räkna ut värdet på en hand
        public int CalculateHandValue(List<Card> handOfCards)
        {
            int totalSum = 0;
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

                    if (totalSum > 21)
                    {
                        totalSum -= 10;
                    }
                }
            }
            return totalSum;
        }



        //Räkna ut om bettet vanns eller ej
        public void EvaluateBet(int betSum)
        {
            if (Winner == "Player")
            {
                betSum *= 2;
                _game.Player.Currency += betSum;
            }
            else if (Winner == "Dealer")
            {
                _game.Player.Currency -= betSum;
            }
            else if (IsATie)
            {
                _game.Player.Currency += betSum;
            }
        }

        //kolla om spelet är slut 
        public void CheckGameOver()
        {
            int dealerValue = CalculateHandValue(_game.Dealer.HandOfCards);
            int playerValue = CalculateHandValue(_game.Dealer.HandOfCards);


            if (dealerValue == playerValue)
            {
                IsGameOver = true;
                IsATie = true;
            }
            else if (playerValue == 21 && dealerValue != 21)
            {
                IsGameOver = true;
                Winner = "Player";
            }
            else if (dealerValue == 21 && playerValue != 21)
            {
                IsGameOver = true;
                Winner = "Dealer";
            }
            else if (playerValue > 21)
            {
                IsGameOver = true;
                Winner = "Dealer";
            }
            else if (dealerValue > 21)
            {
                IsGameOver = true;
                Winner = "Player";
            }
            else
            {
                IsGameOver = false;
            }
        }

        //Starta nytt spel
        public void StartNewGame(BetRequestDto betDto)
        {
            _game.Deck.CreateDeck();
            _game.Player.HandOfCards.Clear();
            _game.Dealer.HandOfCards.Clear();
            _game.Player.PlaceBet(betDto.BetValue);
            DealInitialHand();
        }

        //Avsluta spel
        public void EndGame(BetRequestDto betDto)
        {
            CheckGameOver();
            if (IsGameOver)
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
