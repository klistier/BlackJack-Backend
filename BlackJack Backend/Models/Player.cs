namespace BlackJack_Backend.Models
{
    public class Player(string name, int currency, List<Card> handOfCards)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public int Currency { get; set; } = currency;
        public List<Card> HandOfCards { get; set; } = handOfCards;
        public bool CanDrawCard { get; set; } = true;

        private readonly Deck _deck;
        private readonly GameLogic _gameLogic;


        //spelardrag - Splitta
        public (List<Card> firstHand, List<Card> secondHand) SplitHand()
        {
            string firstCardValue = HandOfCards[0].Value;
            string secondCardValue = HandOfCards[1].Value;

            List<Card> firstHand = new();
            List<Card> secondHand = new();

            if (firstCardValue == secondCardValue ||
               firstCardValue == "Knekt" && secondCardValue == "Knekt" ||
               firstCardValue == "Dam" && secondCardValue == "Dam" ||
               firstCardValue == "Kung" && secondCardValue == "Kung" ||
               firstCardValue == "Ess" && secondCardValue == "Ess")
            {
                firstHand.Add(HandOfCards[0]);
                secondHand.Add(HandOfCards[1]);

                if (firstCardValue == "Ess")
                {
                    firstHand.Add(_deck.DrawCard());
                    secondHand.Add(_deck.DrawCard());
                    CanDrawCard = false;
                }
            }
            return (firstHand, secondHand);
        }

        //Hit
        public List<Card> Hit()
        {
            var card = _deck.DrawCard();
            HandOfCards.Add(card);
            _gameLogic.CheckGameOver();
            return HandOfCards;
        }

        //Stand
        public void Stand()
        {

        }



    }



}
