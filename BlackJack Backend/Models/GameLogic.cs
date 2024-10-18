namespace BlackJack_Backend.Models
{
    public class GameLogic
    {
        public bool IsGameOver { get; set; } = false;
        public string Winner { get; set; }

        private readonly Deck _deck;
        private readonly Player _player;
        private readonly Dealer _dealer;

        //dela ut första handen
        public void DealInitialHand()
        {
            _player.HandOfCards = [_deck.DrawCard(), _deck.DrawCard()];
            _dealer.HandOfCards = [_deck.DrawCard(false), _deck.DrawCard()];
        }

        //dela ut ett kort
        public Card DealCard()
        {
            return (_deck.DrawCard());
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

        public void CheckGameOver()
        {
            int dealerValue = CalculateHandValue(_dealer.HandOfCards);
            int playerValue = CalculateHandValue(_player.HandOfCards);

            if (playerValue > 21 || dealerValue == 21 && playerValue == 21)
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
    }
}
