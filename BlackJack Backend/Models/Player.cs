namespace BlackJack_Backend.Models
{
    public class Player(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public int Currency { get; set; } = 1000;
        public List<Card> HandOfCards { get; set; } = [];
        //public List<Card> SplitHandOfCards { get; set; } = [];
        public bool CanDrawCard { get; set; } = true;

        private readonly Deck _deck;
        private readonly GameLogic _gameLogic;
        private readonly Dealer _dealer;


        //spelardrag - Splitta
        //public void SplitHand()
        //{
        //    string firstCardValue = HandOfCards[0].Value;
        //    string secondCardValue = HandOfCards[1].Value;

        //    List<Card> firstHand = new();
        //    List<Card> secondHand = new();

        //    if (firstCardValue == secondCardValue ||
        //       firstCardValue == "Knekt" && secondCardValue == "Knekt" ||
        //       firstCardValue == "Dam" && secondCardValue == "Dam" ||
        //       firstCardValue == "Kung" && secondCardValue == "Kung" ||
        //       firstCardValue == "Ess" && secondCardValue == "Ess")
        //    {

        //        firstHand.Add(HandOfCards[0]);
        //        secondHand.Add(HandOfCards[1]);

        //        HandOfCards = firstHand;
        //        SplitHandOfCards = secondHand;

        //        if (firstCardValue == "Ess")
        //        {
        //            firstHand.Add(_deck.DrawCard());
        //            secondHand.Add(_deck.DrawCard());
        //            CanDrawCard = false;
        //        }
        //    }            
        //}

        //Hit
        public void Hit(bool isSplitHand = false)
        {
            var card = _deck.DrawCard();
            HandOfCards.Add(card);
            _gameLogic.CheckGameOver();
        }


        //Stand
        public void Stand()
        {
            foreach (var card in _dealer.HandOfCards)
            {
                card.IsFaceUp = true;
            }
            _dealer.DealerDrawCard();
            _gameLogic.CheckGameOver();
        }

        //Satsa
        public string PlaceBet(int betSum)
        {
            if (betSum > Currency)
            {
                return "Inte tillräckligt med pengar!";
            }
            else
            {
                Currency -= betSum;
                return "Bet lagt!";
            }
        }


        //Räkna ut om bettet vanns eller ej
        public void EvaluateBet(int betSum)
        {
            if (_gameLogic.Winner == "Player")
            {
                betSum *= 2;
                Currency += betSum;
            }
            else if (_gameLogic.Winner == "Dealer")
            {
                Currency -= betSum;
            }
            else if (_gameLogic.IsATie)
            {
                Currency += betSum;
            }
        }

    }



}
