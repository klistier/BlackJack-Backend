namespace BlackJack_Backend.Models
{
    public class Dealer()
    {
        public string Name { get; set; } = "Coupier";
        public List<Card> HandOfCards { get; set; } = [];

        private readonly GameLogic _gameLogic;
        private readonly Deck _deck;

        public void DealerDrawCard()
        {
            int dealerCardsValue = _gameLogic.CalculateHandValue(HandOfCards);
            if (dealerCardsValue >= 17)
            {
                Card cardToAdd = _deck.DrawCard();
                HandOfCards.Add(cardToAdd);
            }
        }
    }


}
